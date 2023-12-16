using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data;
using ProductManagerBot.Data.Entities;
using System.Collections;

namespace ProductManagerBot.Services.ManufactureService
{
    internal class ManufactureService : IManufactureService
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

        public IQueryable<Manufacture> GetAll() => _appDbContext.Manufactures;

        public async Task<IQueryable<Manufacture>> GetAllByUserId(int id)
        {
            var user = await _appDbContext.Users.Include(x => x.Products)
                                                    .ThenInclude(y => y.Manufacture)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return user?.Products.Select(x => x!.Manufacture ?? new Manufacture())
                                 .Distinct()
                                 .AsQueryable()
                                 ??
                                 Enumerable.Empty<Manufacture>().AsQueryable();
        }

        public async Task<Manufacture?> GetById(int id)
            => await _appDbContext.Manufactures.FirstOrDefaultAsync(x => x.Id == id);

        public async void Update(int id, string name)
        {
            //var manufacture = await _appDbContext.Manufactures.FirstOrDefaultAsync(x => x.Id == id);
            //if(manufacture is null)
            //{
            //    return;
            //}
            //manufacture.Name = name;
            //await _appDbContext.SaveChangesAsync();

            _appDbContext.Entry(new Manufacture { Id = id, Name = name })
                         .State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

        }
    }
}
