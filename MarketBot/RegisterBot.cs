using ProductManagerBot.Services.AdminCheckService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.UserService;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using ProductManagerBot.Extensions;


namespace _RegisterBot
{
    internal class RegisterBot
    {
        private TelegramBotClient client;
        private readonly IAdminCheckService _adminCheck;
        private readonly IUserService _userService;

        public RegisterBot(ITokenService token, IAdminCheckService admincheck, IUserService user)
        {
            client = new TelegramBotClient(token.Token);
            _adminCheck = admincheck;
            _userService = user;
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

            if (type == MessageType.Text)
            {
                await TextMessageHandler(update);
            }
        }

        private async Task TextMessageHandler(Update update)
        {
            var tgUser = update!.Message!.From;
            int id = (int)update!.Message!.From!.Id;

            update.Message.From.ToUser();
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
                _userService.GetAll();
            }

        }
        #endregion
    }
}

