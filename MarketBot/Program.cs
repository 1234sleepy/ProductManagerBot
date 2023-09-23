using _RegisterBot;
using Microsoft.EntityFrameworkCore;
using ProductManagerBot.Data.Entities;

string token = "";

RegisterBot bot = new RegisterBot(token);

bot.Start();
bot.GetStatus();
Console.ReadLine();

internal class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<FavoriteProduct> FavoriteProducts => Set<FavoriteProduct>();
    public DbSet<Manufacture> Manufactures => Set<Manufacture>();
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = medb.db");
    }
}