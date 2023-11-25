using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.UserService
{
    internal interface IUserService
    {
        void Add(int id, string name, string username, string phone, string email);
        void Update(int id, string name, string username, string phone, string email);
        Task<User?> GetById(int id);
        IQueryable<User> GetAll();
    }
}
