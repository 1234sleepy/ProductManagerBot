using Microsoft.Data.Sqlite;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Services.AdminCheckService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.UserService;
using System.Numerics;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace _RegisterBot
{
    internal class RegisterBot
    {
        private TelegramBotClient client;
        private readonly IAdminCheckService _adminCheck;
        private readonly IUserService user;

        public RegisterBot(ITokenService token, IAdminCheckService admincheck, IUserService user)
        {
            client = new TelegramBotClient(token.Token);
            _adminCheck = admincheck;
            this.user = user;
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

            if(type == MessageType.Text)
            {
                TextMessageHandler(update);
            }
        }

        private async void TextMessageHandler(Update update)
        {
            int id = (int)update.Message.From.Id;
            string name = update.Message.From.FirstName + " " + update.Message.From.LastName;
            string username = update.Message.From.Username;
            string phone = "";
            string email = "";

            if (update.Message.Text == "/start") {
                await client.SendTextMessageAsync(chatId: id, $"Hello {username}. Can you wait a second please. We create account for you.");
                user.Add(id, name, username,phone, email);
                await client.SendTextMessageAsync(chatId: id, $"You register is successful");
            }else {
                await client.SendTextMessageAsync(chatId: id,
                text: $"Hello {username} you successful log in");
            }

            if (update.Message.Text == "/getUsers" && _adminCheck.Check(update.Message.From.Id))
            {
                user.GetAll();
            }

        }
        #endregion
    }
}

