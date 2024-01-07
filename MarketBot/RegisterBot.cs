using ProductManagerBot.Services.AdminCheckService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.UserService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using ProductManagerBot.Extensions;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using ProductManagerBot.Services.APITokenService;
using ProductManagerBot.Services.FavoriteProductService;
using ProductManagerBot.Services.ProductService;

namespace _RegisterBot
{
    internal class RegisterBot
    {
        private TelegramBotClient client;
        private readonly IAdminCheckService _adminCheck;
        private readonly IUserService _userService;
        private readonly IProductService _product;
        //private readonly IAPITokenService apiToken;

        static HttpClient httpClient = new HttpClient();

        public RegisterBot(ITokenService token, 
                           IAdminCheckService admincheck, 
                           IUserService user,
                           IProductService product)
        {
            client = new TelegramBotClient(token.Token);
            _adminCheck = admincheck;
            _userService = user;
            _product = product;
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
                await client.SendTextMessageAsync(id,"Users",
                    replyMarkup: gmenu);
            }
            if (update.Message.Text == "/getFood")
            {
                var prod = _product.GetAll().Select(x => InlineKeyboardButton.WithCallbackData(x.Name.ToString(), x.Id.ToString())).ToArray();

                var gmenu = new InlineKeyboardMarkup(prod);
                await client.SendTextMessageAsync(id, "Product",
                    replyMarkup: gmenu);

            }

        }
        #endregion
    }
}

