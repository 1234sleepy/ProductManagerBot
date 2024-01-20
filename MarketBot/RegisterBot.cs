using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Extensions;
using ProductManagerBot.Services.AdminCheckService;
using ProductManagerBot.Services.FavoriteProductService;
using ProductManagerBot.Services.LookupService;
using ProductManagerBot.Services.ProductService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.UserService;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ZXing.Windows.Compatibility;

namespace _RegisterBot
{
    internal class RegisterBot
    {
        private TelegramBotClient client;
        private readonly IAdminCheckService _adminCheck;
        private readonly IUserService _userService;
        private readonly ILookupService _lookupService;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly IProductService _productService;
        static Dictionary<String, Object> buffer = new Dictionary<String, Object>();
       
        public RegisterBot(ITokenService token,
                           IAdminCheckService admincheck,
                           IUserService user,
                           ILookupService lookupService,
                           IFavoriteProductService favoriteProductService,
                           IProductService productService)
        {
            client = new TelegramBotClient(token.Token);
            _adminCheck = admincheck;
            _userService = user;
            _lookupService = lookupService;
            _favoriteProductService = favoriteProductService;
            _productService = productService;
        }

        #region -- Public Methods --
        public async void GetStatus()
        {
            var userInfo = await client.GetMeAsync();
            Console.WriteLine($"@{userInfo.Id} Username: {userInfo.Username}");
        }

        public void Start()
        {
            client.StartReceiving(updateHandler: UpdateHandler,
                                  pollingErrorHandler: ErrorHandler);
        }
        #endregion

        #region -- Private Methods --
        private Task ErrorHandler(ITelegramBotClient bot, Exception ex, CancellationToken ct)
        {
            Console.WriteLine(ex);
            return Task.CompletedTask;
        }

        private async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken ct)
        {
            int id = (int)update!.Message!.From!.Id;
            var type = update.Message?.Type ?? MessageType.Unknown;

            if (update.Type == UpdateType.CallbackQuery)
            {
                await bot.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
                Console.WriteLine(update.CallbackQuery.Data);
                //Console.WriteLine(await _userService.GetById(int.Parse(update.CallbackQuery.Data)));
                var f = update.CallbackQuery.Data.Split(" ")[1];
                if (update.CallbackQuery.Data == "addFavProduct")
                {

                    if (await (await _favoriteProductService.GetAllByUserId(id)).AnyAsync(x => x.Id.Equals(f)))
                    {
                        _favoriteProductService.Add(id, (int)buffer[update.CallbackQuery.Data.Split(" ")[1]]);
                    }

                    if (update.CallbackQuery.Data.Contains("addProduct"))
                    {
                        if (await (await _productService.GetAllByUserId(id)).AnyAsync(x => x.Barcode.Equals(f)))
                        {
                            _productService.Add((Product)buffer[update.CallbackQuery.Data.Split(" ")[1]], id);
                        }

                    }
                }


                if (type == MessageType.Photo)
                {

                    MemoryStream ms = new MemoryStream();
                    var photoId = update?.Message?.Photo?.Last().FileId;
                    var photoInfo = await bot.GetFileAsync(photoId);
                    await bot.DownloadFileAsync(
                        photoInfo.FilePath,
                        ms);

                    var reader = new BarcodeReader();
                    var luminance = new BitmapLuminanceSource(new Bitmap(ms));
                    var result = reader.Decode(luminance);
                    if (result != null)
                    {


                        await bot.SendTextMessageAsync(
                            update.Message.From.Id,
                            text: $"{result.Text}!");

                        var ress = await _lookupService.GetProduct(result.Text);
                        buffer[result.Text] = ress;
                        var btn = InlineKeyboardButton.WithCallbackData("Favorite?", "addFavProduct" + result.Text);
                        var btn2 = InlineKeyboardButton.WithCallbackData("Product?", "addProduct " + result.Text);
                        var btna = new[] { btn, btn2 };
                        var bmenu = new InlineKeyboardMarkup(btna);
                        await client.SendTextMessageAsync(id, ress.Name, replyMarkup: bmenu);
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(
                            update.Message.From.Id,
                            text: $"oh no((");
                    }
                }

                if (type == MessageType.Text)
                {
                    await TextMessageHandler(update);
                }
            }
        }

        private async Task TextMessageHandler(Update update)
        {
            var tgUser = update!.Message!.From;
            int id = (int)update!.Message!.From!.Id;


            if (update.Message.Text == "/start")
            {
                await client.SendTextMessageAsync(chatId: id, $"Hello {tgUser!.Username}. Can you wait a second please. We create account for you.");
                _userService.Add(tgUser.ToUser());
                await client.SendTextMessageAsync(chatId: id, $"You register is successful");
            }
            else
            {
                await client.SendTextMessageAsync(chatId: id,
                text: $"Hello {tgUser!.Username} you successful log in");
            }

            if (update.Message.Text == "/getUsers" && _adminCheck.Check(update.Message.From.Id))
            {
                InlineKeyboardButton.WithCallbackData(string.Join("\n", _userService.GetAll().Select(x => x.Name)), "Some DATA");


                var us = _userService.GetAll().Select(x => InlineKeyboardButton.WithCallbackData(x.Name, x.Id.ToString())).ToArray();

                var gmenu = new InlineKeyboardMarkup(us);
                await client.SendTextMessageAsync(id, "Users",
                    replyMarkup: gmenu);
            }
            if (update.Message.Text == "/getFood")
            {

            }

        }
        #endregion
    }
}

