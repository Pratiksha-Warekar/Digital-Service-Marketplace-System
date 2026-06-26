using DSM.Models.Entities;

namespace DSM.Repository.Interface
{
    public interface IProviderRepository
    {
        List<Provider> GetAll();

        Provider GetById(int id);

        void Add(Provider provider);

        void Update(Provider provider);

        void Delete(int id);
    }
}