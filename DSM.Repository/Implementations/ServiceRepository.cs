using DSM.Models.Entities;
using DSM.Repository.Data;
using DSM.Repository.Interface;
using System.Linq;

namespace DSM.Repository.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Service> GetAll()
        {
            return _context.Services.ToList();
        }
        public Service? GetById(int id)
        {
            return _context.Services
                .FirstOrDefault(x => x.ServiceId == id);
        }

        public void Add(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void Update(Service service)
        {
            _context.Services.Update(service);
            _context.SaveChanges();
        }

        
        public void Delete(int id)
        {
            var data = _context.Services
                .FirstOrDefault(x => x.ServiceId == id);

            if (data != null)
            {
                _context.Services.Remove(data);
                _context.SaveChanges();
            }
        }

        
        public List<Service> GetByCategory(int categoryId)
        {
            return _context.Services
                .Where(x => x.CategoryId == categoryId)
                .ToList();
        }

        
        public string? GetAddress(int providerId)
        {
            return _context.Providers
                .Where(p => p.ProviderId == providerId)
                .Select(p => p.Address)
                .FirstOrDefault();
        }
    }
}