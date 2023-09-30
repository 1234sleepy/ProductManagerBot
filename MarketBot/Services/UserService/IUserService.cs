using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.UserService
{
    internal interface IUserService
    {
        void Add(int userid, int productid);
        void Update(int id, int userid, int productid);
        void Delete(int id);
    }
}
