using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.SearchupcService
{
    internal interface ISearchupcService
    {
        public Task<Product> GetProduct(String barcode);
    }
}
