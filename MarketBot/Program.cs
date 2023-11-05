using _RegisterBot;
using Microsoft.Extensions.DependencyInjection;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Services.CategoryService;
using ProductManagerBot.Services.FavoriteProductService;
using ProductManagerBot.Services.ManufactureService;
using ProductManagerBot.Services.ProductService;
using ProductManagerBot.Services.UserService;
using Telegram.Bot.Types;

string token = "";

var services = new ServiceCollection();

services.AddSingleton< IProductService, ProductService >();
services.AddSingleton< ICategoryService, CategoryService >();
services.AddSingleton< IFavoriteProductService, FavoriteProductService >();
services.AddSingleton< IManufactureService, ManufactureService>();
services.AddSingleton< IUserService, UserService>();

using var provider = services.BuildServiceProvider();

RegisterBot bot = new RegisterBot(token);

bot.Start();
bot.GetStatus();
Console.ReadLine();

