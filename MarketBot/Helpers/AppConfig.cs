using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Helpers
{
    internal class AppConfig
    {
        public string? TelegramToken { get; set; }
        public long[]? Admins { get; set; }
        public string? ConnectionString { get; set; }
    }
}
