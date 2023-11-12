using Newtonsoft.Json;
using ProductManagerBot.Helpers;

namespace ProductManagerBot.Services.AdminCheckService
{
    internal class AdminCheckService : IAdminCheckService
    {

        private AppConfig config;
        public AdminCheckService()
        {
            string json = File.ReadAllText(Constants.APP_CONFIG);
            config = JsonConvert.DeserializeObject<AppConfig>(json);
        }
        public bool Check(long userid)
        {
            if (config.Admins.Contains(userid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
