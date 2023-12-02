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
        public async void Add(User user)
        {
            if (await _appDbContext.Users.AnyAsync(x => x.Id == user.Id)) {

                return;
            }
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

        }
        public async void Delete(int id)
        {
            _appDbContext.Users.Remove(new User() { Id = id });
            await _appDbContext.SaveChangesAsync();
        }
        
        public async void Update(User user)
        {
            _appDbContext.Entry(user)
                         .State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
        public IQueryable<User> GetAll() => _appDbContext.Users;

        public async Task<User?> GetById(int id)
            => await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);


    }
}
