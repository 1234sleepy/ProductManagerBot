using ProductManagerBot.Data.Entities;
using ProductManagerBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductManagerBot.Services.CategoryService
{
    internal class CategoryService : ICategoryService
    {
        private readonly AppDbContext _appDbContext;
        public CategoryService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async void Add(string name)
        {
            await _appDbContext.Categories.AddAsync(new Category() { Name = name });
            await _appDbContext.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            _appDbContext.Categories.Remove(new Category() { Id = id });
            await _appDbContext.SaveChangesAsync();
        }

        public async void Update(int id, string name)
        {

            _appDbContext.Entry(new Category { Id = id, Name = name })
                         .State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

        }
        public IQueryable<Category> GetAll() => _appDbContext.Categories;

        public async Task<Category?> GetById(int id)
            => await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }
}
