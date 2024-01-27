using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Data;
namespace ProductManagerBot.Services.ProductService
{
    internal class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;

    public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async void Add(Product p,long telegramId)
        {
            p.UserId = telegramId;
            await _appDbContext.Products.AddAsync(p);
            await _appDbContext.SaveChangesAsync();
        }
        

        public async void Delete(int id)
        {
            _appDbContext.Products.Remove(new Product() { Id = id });
            await _appDbContext.SaveChangesAsync();
        }

        public async void Update(Product p)
        {
            _appDbContext.Entry(p)
                         .State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
        public IQueryable<Product> GetAll() => _appDbContext.Products;

        public async Task<IQueryable<Product>> GetAllByUserId(int id)
        {
            var user = await _appDbContext.Users.Include(x => x.Products)
                                                .FirstOrDefaultAsync(x => x.Id == id);
            return user!.Products.AsQueryable();
        }

        public async Task<Product?> GetById(int id)
            => await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

    }
}
