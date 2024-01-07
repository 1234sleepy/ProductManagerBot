using ProductManagerBot.Data;
using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Helpers
{
    static class Seed
    {
        public static void SeedData(AppDbContext db)
        {
            if (db.Users.Any())
            {
                return;
            }

            var user1 = new User { TgId = 1728238356, Name = "User1" };
            var user2 = new User { TgId = 1050318011, Name = "User2" };

            user1.Products.AddRange(new Product[] { new Product{ Name = "First Product" }, new Product { Name = "Third Product" } });
            user2.Products.AddRange(new Product[] { new Product{ Name = "Second Product" }, new Product { Name = "Fourth Product" } });
            
            db.Users.AddRange(user1, user2);
            db.SaveChanges();

        }
    }
}
