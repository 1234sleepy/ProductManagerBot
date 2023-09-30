using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data;
using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.FavoriteProductService
{
    internal class FavoriteProductService : IFavoriteProductService
    {
        private readonly AppDbContext _appDbContext;
        public FavoriteProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async void Add(int userid, int productid)
        {
            await _appDbContext.FavoriteProducts.AddAsync(new FavoriteProduct() { UserId = userid, ProductId = productid  });
            await _appDbContext.SaveChangesAsync();
        }


        public async void Delete(int id)
        {
            _appDbContext.FavoriteProducts.Remove(new FavoriteProduct() { Id = id});
            await _appDbContext.SaveChangesAsync();
        }

        public async void Update(int id, int userid, int productid)
        {
            _appDbContext.Entry(new FavoriteProduct { Id = id, UserId = userid, ProductId = productid })
                         .State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

        }

        public IQueryable<FavoriteProduct> GetAll() => _appDbContext.FavoriteProducts;

        public async Task<IQueryable<FavoriteProduct>> GetAllByUserId(int id)
        {
            var user = await _appDbContext.Users.Include(x => x.FavoriteProducts)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return (IQueryable<FavoriteProduct>)user!.FavoriteProducts;
        }

        public async Task<FavoriteProduct?> GetById(int id)
            => await _appDbContext.FavoriteProducts.FirstOrDefaultAsync(x => x.Id == id);


    }
}
