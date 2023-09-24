using ProductManagerBot.Data.Entities;

namespace ProductManagerBot.Services.ManufactureService
{
    internal interface IManufactureService
    {
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);

        Task<Manufacture?> GetById(int id);
        IQueryable<Manufacture> GetAll();
        Task<IQueryable<Manufacture>> GetAllByUserId(int id);
    }
}
