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
        void Add(User user);
        void Update(User user);
        Task<User?> GetById(int id);
        IQueryable<User> GetAll();


    }
}
