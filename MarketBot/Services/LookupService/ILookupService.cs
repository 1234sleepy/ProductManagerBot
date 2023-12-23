using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.LookupService
{
    internal interface ILookupService
    {
        Task<Product> GetProduct(string barcode);
    }
}
