using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.CategoryService
{
    internal interface ICategoryService
    {
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
        Task<Category?> GetById(int id);
        IQueryable<Category> GetAll();
    }
}
