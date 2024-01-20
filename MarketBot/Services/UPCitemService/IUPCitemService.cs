using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.UPCitemService
{
    internal interface IUPCitemService
    {
        Task<Product> GetProduct(string barcode);
    }
}
