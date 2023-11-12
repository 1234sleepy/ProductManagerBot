using Microsoft.Data.Sqlite;
using ProductManagerBot.Data.Entities;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace _RegisterBot
{
    internal class RegisterBot
    {
        private TelegramBotClient client;

        public RegisterBot(string token)
        {
            client = new TelegramBotClient(token);
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
            Console.WriteLine(update?.CallbackQuery?.Data);

        }

        private async void TextMessageHandler(Update update)
        {
       
        }

        #endregion
    }
}

