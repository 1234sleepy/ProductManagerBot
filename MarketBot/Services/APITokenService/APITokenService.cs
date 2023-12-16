using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductManagerBot.Helpers;
using ProductManagerBot.Services.TokenService;

namespace ProductManagerBot.Services.APITokenService
{
    internal class APITokenService : IAPITokenService
    {
        public APITokenService()
        {
            string json = File.ReadAllText(Constants.APP_CONFIG);
            var config = JsonConvert.DeserializeObject<AppConfig>(json);

            Token = config!.ApiToken;
        }
        public string Token { get; }
    }
}
