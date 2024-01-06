using ProductManagerBot.Extensions;
using ProductManagerBot.Services.AdminCheckService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.UserService;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ZXing;

namespace _RegisterBot
{
    internal class RegisterBot
    {
        private TelegramBotClient client;
        private readonly IAdminCheckService _adminCheck;
        private readonly IUserService _userService;
       
        public RegisterBot(ITokenService token,
                           IAdminCheckService admincheck,
                           IUserService user)
        {
            client = new TelegramBotClient(token.Token);
            _adminCheck = admincheck;
            _userService = user;
            //apiToken = apitoken;
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
            var type = update.Message?.Type ?? MessageType.Unknown;

            if (update.Type == UpdateType.CallbackQuery)
            {
                await bot.AnswerCallbackQueryAsync(update.CallbackQuery.Id);
                Console.WriteLine(update.CallbackQuery.Data);
                Console.WriteLine(await _userService.GetById(int.Parse(update.CallbackQuery.Data)));

            }
            if(type == MessageType.Photo)
            {

                MemoryStream ms = new MemoryStream();
                var photoId = update?.Message?.Photo?.Last().FileId;
                var photoInfo = await bot.GetFileAsync(photoId);
                await bot.DownloadFileAsync(
                    photoInfo.FilePath,
                    ms);

                var bmp = new Bitmap(ms);
                BarcodeReader barcodeReader = new();
                var luminance = new BitmapLuminanceSource(bmp);
                var result = barcodeReader.Decode(luminance);
                if(result != null)
                {
                    await bot.SendTextMessageAsync(
                        update.Message.From.Id,
                        text: $"{result.Text}!");
                    //Todo: btn add
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

