using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.TokenService
{
    internal class TokenService : ITokenService
    {
        public string Token => JsonSerializer.Serialize(Token);
        
    }
}
