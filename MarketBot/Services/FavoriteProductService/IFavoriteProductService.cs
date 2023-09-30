using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.FavoriteProductService
{
    internal interface IFavoriteProductService
    {
        void Add(int userid, int productid);
        void Update(int id, int userid, int productid);
        void Delete(int id);

        Task<FavoriteProduct?> GetById(int id);
        IQueryable<FavoriteProduct> GetAll();
        Task<IQueryable<FavoriteProduct>> GetAllByUserId(int id);
    }
}
