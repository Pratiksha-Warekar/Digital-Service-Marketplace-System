using DSM.Models.Entities;

namespace DSM.Repository.Interface
{
    public interface IServiceRepository
    {
        List<Service> GetAll();

        Service? GetById(int id);

        string? GetAddress(int providerId);   
        void Add(Service service);

        void Update(Service service);

        void Delete(int id);

        List<Service> GetByCategory(int categoryId);
    }
}