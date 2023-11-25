using Telegram.Bot.Types;

namespace ProductManagerBot.Extensions;

internal static class TgUserExtensions
{
    public static Data.Entities.User ToUser(this User user)
    {
        return new Data.Entities.User
        {
            TgId = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Username = user.Username
        };
    }
}
