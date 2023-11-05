using _RegisterBot;
using Microsoft.Extensions.DependencyInjection;
using ProductManagerBot.Data;
using ProductManagerBot.Services.CategoryService;
using ProductManagerBot.Services.FavoriteProductService;
using ProductManagerBot.Services.ManufactureService;
using ProductManagerBot.Services.ProductService;
using ProductManagerBot.Services.TokenService;
using ProductManagerBot.Services.UserService;

string token = "";

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>();
services.AddTransient<IProductService, ProductService>();
services.AddTransient<ICategoryService, CategoryService>();
services.AddTransient<IFavoriteProductService, FavoriteProductService>();
services.AddTransient<IManufactureService, ManufactureService>();
services.AddTransient<IUserService, UserService>();
services.AddTransient<ITokenService, TokenService>();

using var provider = services.BuildServiceProvider();

RegisterBot bot = new RegisterBot(token);

bot.Start();
bot.GetStatus();
Console.ReadLine();

