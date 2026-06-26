using Microsoft.AspNetCore.Mvc;
using DSM.Repository.Interface;
using DSM.Repository.Data;
using System.Linq;

namespace DSM.UI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly ApplicationDbContext _context;

        public ServiceController(
            IServiceRepository serviceRepo,
            ApplicationDbContext context)
        {
            _serviceRepo = serviceRepo;
            _context = context;
        }

        

        public IActionResult Index(string search)
        {
            var data = _serviceRepo.GetAll()
                .Where(x => x.Status == "Approved")
                .Select(x => new DSM.Models.Entities.Service
                {
                    ServiceId = x.ServiceId,
                    ServiceName = x.ServiceName,
                    Description = x.Description,
                    Price = x.Price,
                    PhotoURL = x.PhotoURL,
                    ProviderId = x.ProviderId,
                    ProviderName = x.ProviderName,
                    Status = x.Status,

                    
                    Address = _context.Providers
    .Where(p => p.Name == x.ProviderName)
    .Select(p => p.Address)
    .FirstOrDefault()
                });

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x =>
                    x.ServiceName.Contains(search));
            }

            return View(data.ToList());
        }

        

        public IActionResult Details(int id)
        {
            var data = _serviceRepo.GetById(id);

            if (data != null)
            {
                data.Address = _context.Providers
    .Where(p => p.Name == data.ProviderName)
    .Select(p => p.Address)
    .FirstOrDefault();
            }

            return View(data);
        }

      

        [HttpGet]
        public JsonResult SearchServices(string term)
        {
            var data = _serviceRepo.GetAll()
                .Where(x => x.Status == "Approved");

            if (!string.IsNullOrEmpty(term))
            {
                data = data.Where(x =>
                    x.ServiceName.Contains(term));
            }

            var result = data.Select(x => new
            {
                id = x.ServiceId,
                serviceName = x.ServiceName
            }).ToList();

            return Json(result);
        }
    }
}