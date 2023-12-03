using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.ProductService
{
    internal interface IProductService 
    {

       
        void Add(Product p);

        void Update(Product p);
        void Delete(int id);
        Task<Product?> GetById(int id);
        IQueryable<Product> GetAll();
        Task<IQueryable<Product>> GetAllByUserId(int id);
    }
}
