using ProductManagerBot.Data.Entities;
using ProductManagerBot.DTOs.UpcItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Extensions
{
    internal static class UpcProductExtensions
    {
        public static Product ToProduct(this Item item)
        {

            return new Product
            {
                Name = item.title
            };
        }
    }
}
