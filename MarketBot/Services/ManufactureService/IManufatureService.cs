using ProductManagerBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBot.Services.ManufactureService
{
    internal interface IManufatureService
    {
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);

        Manufacture GetById(int id);
        IQueryable<Manufacture> GetAll();
        Task<IQueryable<Manufacture>> GetAllByUserId(int id);
    }
}
