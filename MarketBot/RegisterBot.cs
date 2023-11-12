using Microsoft.Data.Sqlite;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Services.TokenService;
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

        public RegisterBot(ITokenService token)
        {
            client = new TelegramBotClient(token.Token);
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

            var user = update.Message.From;
            if (!isUserRegister())
            {
                string connectionString = "Data Source=mydb.db";
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    string sqlInsert = "INSERT INTO Users (Name, Age,isAdmin,Password) " +
                                       "VALUES (@name, @age, @isadmin, @password)";
                    var insertCom = new SqliteCommand(sqlInsert, connection);
                    insertCom.Parameters.Add(new SqliteParameter("@name", name));
                    insertCom.Parameters.Add(new SqliteParameter("@age", age));
                    insertCom.Parameters.Add(new SqliteParameter("@isadmin", isAdmin));
                    insertCom.Parameters.Add(new SqliteParameter("@password", password));
                    insertCom.ExecuteNonQuery();
                }
            }
            if (new string[] { "hello", "hi", "/start" }.Contains(textToLower))
            {

                await client.SendTextMessageAsync(chatId: user.Id, text: $"Hi {user.FirstName}.");
            }
            else if ("/getusers".Contains(textToLower) && isUserAdmin())
            {
                await client.SendTextMessageAsync(chatId: user.Id, text: $"Hi {user.FirstName}.");
            }
        }
        private bool isUserRegister()
        {
            return false;
        }
        private bool isUserAdmin()
        {
            return false;
        }
        #endregion
    }
}

