using _RegisterBot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagerBot.Data;
using ProductManagerBot.Services.AdminCheckService;
using ProductManagerBot.Services.CategoryService;
using ProductManagerBot.Services.FavoriteProductService;
using ProductManagerBot.Services.ManufactureService;
using ProductManagerBot.Services.ProductService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.APITokenService;
using ProductManagerBot.Services.UserService;
using ProductManagerBot.Services.SearchupcService;
using ProductManagerBot.Services.LookupService;
using ProductManagerBot.Services.UPCitemService;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite("Data Source=../../../medb.db");
});
services.AddTransient<IProductService, ProductService>();
services.AddTransient<ICategoryService, CategoryService>();
services.AddTransient<IFavoriteProductService, FavoriteProductService>();
services.AddTransient<IManufactureService, ManufactureService>();
services.AddTransient<IAdminCheckService, AdminCheckService>();
services.AddTransient<IUserService, UserService>();
services.AddSingleton<ISearchupcService, SearchupcService>();
services.AddSingleton<ITokenService,TokenService>();
services.AddSingleton<IUPCitemService, UPCitemService>();
services.AddSingleton<ILookupService,LookupService>();
services.AddSingleton<RegisterBot>();


using var provider = services.BuildServiceProvider(); //provider.GetService<ITokenService>();

var bot = provider.GetService<RegisterBot>();


bot?.Start();
bot?.GetStatus();
Console.ReadLine();

