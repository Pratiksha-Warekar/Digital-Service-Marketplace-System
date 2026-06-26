using DSM.Models.Entities;
using DSM.Repository.Data;
using DSM.Repository.Interface;

namespace DSM.Repository.Implementations
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Provider> GetAll()
        {
            return _context.Providers.ToList();
        }

        public Provider GetById(int id)
        {
            return _context.Providers
                .FirstOrDefault(x => x.ProviderId == id);
        }

        public void Add(Provider provider)
        {
            _context.Providers.Add(provider);

            _context.SaveChanges();
        }

        public void Update(Provider provider)
        {
            _context.Providers.Update(provider);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _context.Providers
                .FirstOrDefault(x => x.ProviderId == id);

            if (data != null)
            {
                _context.Providers.Remove(data);

                _context.SaveChanges();
            }
        }
    }
}