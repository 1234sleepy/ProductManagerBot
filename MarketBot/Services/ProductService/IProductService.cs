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
        void Add(int id, string barcode, double weight, double calories, DateTime dateofmanufacture, DateTime dateofuse, string productcontent, int manufactureid, string name, int userid, int categoryid, Manufacture manufacture);

        void Update(int id, string barcode, double weight, double calories, DateTime dateofmanufacture, DateTime dateofuse, string productcontent, int manufactureid, string name, int userid, int categoryid, Manufacture manufacture);
        void Delete(int id);
        Task<Product?> GetById(int id);
        IQueryable<Product> GetAll();
        Task<IQueryable<Product>> GetAllByUserId(int id);
    }
}
