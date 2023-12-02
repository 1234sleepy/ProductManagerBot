using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.ConfigService
{
    internal interface IConfigService
    {
        public string ConnectionString { get; }
    }
}
