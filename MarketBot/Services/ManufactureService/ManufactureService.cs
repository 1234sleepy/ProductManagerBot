using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data;
using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.ManufactureService
{
    internal class ManufactureService:IManufatureService
    {
        private readonly AppDbContext _appDbContext;

        public ManufactureService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async void Add(string name)
        {
            await _appDbContext.Manufactures.AddAsync(new Manufacture() { Name = name });
            await _appDbContext.SaveChangesAsync();
        }
        public async void Delete(int id)
        {
             _appDbContext.Manufactures.Remove(new Manufacture() { Id = id });
             await _appDbContext.SaveChangesAsync();
        }

        public void Update(int id, string name)
        {
        }

        public Manufacture GetById(int id)
        {
            return null;
        }

        public IQueryable<Manufacture> GetAll()=>_appDbContext.Manufactures;

        public async Task<IQueryable<Manufacture>> GetAllByUserId(int id)
        {
            var user = await _appDbContext.Users.Include(x => x.Products)
                                                    .ThenInclude(y=>y.Manufacture)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            return user?.Products?.Select(x => x!.Manufacture)?.Distinct()?.AsQueryable<Manufacture>() ?? Enumerable.Empty<Manufacture>().AsQueryable();

        }
    }
}
