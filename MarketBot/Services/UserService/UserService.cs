using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.UserService
{
    internal class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async void Add(int id, string name, string username, string phone, string email)
        {
            await _appDbContext.Users.AddAsync(new User() {Id = id, Name = name, Username = username, Phone = phone, Email = email});
            await _appDbContext.SaveChangesAsync();
        }
        

        public async void Delete(int id)
        {
            _appDbContext.Users.Remove(new User() { Id = id });
            await _appDbContext.SaveChangesAsync();
        }
        
        public async void Update(int id, string name, string username, string phone, string email)
        {
            _appDbContext.Entry(new User { Id = id, Name = name, Username = username, Phone = phone, Email = email })
                         .State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
        public IQueryable<User> GetAll() => _appDbContext.Users;

        public async Task<User?> GetById(int id)
            => await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);


    }
}
