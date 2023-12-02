using Newtonsoft.Json;
using ProductManagerBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.ConfigService
{
    internal class ConfigService : IConfigService
    {
        private readonly AppConfig? _appConfig;

        public ConfigService()
        {
            string json = File.ReadAllText(Constants.APP_CONFIG);
            _appConfig = JsonConvert.DeserializeObject<AppConfig?>(json);
        }
        public string ConnectionString => _appConfig?.ConnectionString ?? string.Empty;
    }
}
