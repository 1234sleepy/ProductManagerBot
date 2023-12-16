using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductManagerBot.Helpers;

namespace ProductManagerBot.Services.TokenService
{
    internal class TokenService : ITokenService
    {
        public TokenService() {
            string json = File.ReadAllText(Constants.APP_CONFIG);
            var config = JsonConvert.DeserializeObject<AppConfig>(json);

            Token = config!.TelegramToken;
        }
        public string Token { get; }
    }
}
