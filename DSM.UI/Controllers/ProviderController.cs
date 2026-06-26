using Microsoft.AspNetCore.Mvc;
using DSM.Models.Entities;
using DSM.Repository.Interface;

namespace DSM.UI.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderRepository _providerRepo;
        private readonly IServiceRepository _serviceRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProviderController
        (
            IProviderRepository providerRepo,
            IServiceRepository serviceRepo,
            ICategoryRepository categoryRepo
        )
        {
            _providerRepo = providerRepo;
            _serviceRepo = serviceRepo;
            _categoryRepo = categoryRepo;
        }

        // ================= REGISTER =================

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Categories = _categoryRepo.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Register
        (
            Provider provider,
            Service service,
            IFormFile PhotoFile
        )
        {
            if
            (
                string.IsNullOrWhiteSpace(provider.Name) ||
                string.IsNullOrWhiteSpace(provider.Email) ||
                string.IsNullOrWhiteSpace(provider.Phone) ||
                string.IsNullOrWhiteSpace(provider.Address) ||
                string.IsNullOrWhiteSpace(provider.Password) ||
                string.IsNullOrWhiteSpace(service.ServiceName) ||
                string.IsNullOrWhiteSpace(service.Description) ||
                service.Price <= 0 ||
                service.CategoryId <= 0
            )
            {
                TempData["Error"] =
                    "Please Fill All Information";

                ViewBag.Categories =
                    _categoryRepo.GetAll();

                return View();
            }

            if (PhotoFile == null)
            {
                TempData["Error"] =
                    "Please Upload Service Image";

                ViewBag.Categories =
                    _categoryRepo.GetAll();

                return View();
            }

            _providerRepo.Add(provider);

            string fileName =
                Guid.NewGuid().ToString()
                + Path.GetExtension(PhotoFile.FileName);

            string folderPath = Path.Combine
            (
                Directory.GetCurrentDirectory(),
                "wwwroot/images"
            );

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath =
                Path.Combine(folderPath, fileName);

            using (var stream =
                new FileStream(fullPath, FileMode.Create))
            {
                PhotoFile.CopyTo(stream);
            }

            service.PhotoURL =
                "/images/" + fileName;

            service.ProviderName =
                provider.Name;

            service.Address =
                provider.Address;

            service.ProviderId =
                provider.ProviderId;

            service.Status =
                "Pending";

            _serviceRepo.Add(service);

            TempData["Success"] =
                "Registration Successful";

            return RedirectToAction("Login");
        }

        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            // ALREADY LOGIN AS PROVIDER
            if (HttpContext.Session.GetString("Provider") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login
        (
            string email,
            string password
        )
        {
            if
            (
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password)
            )
            {
                ViewBag.Error =
                    "Please Enter Email and Password";

                return View();
            }

            var data = _providerRepo.GetAll()
                .FirstOrDefault(x =>
                    x.Email == email &&
                    x.Password == password);

            if (data != null)
            {
                HttpContext.Session
                    .SetString("Provider", data.Name);

                HttpContext.Session
                    .SetInt32("ProviderId", data.ProviderId);

                TempData["Success"] =
                    "Login Successful";

                return RedirectToAction("Dashboard");
            }

            ViewBag.Error =
                "Invalid Email or Password";

            return View();
        }

        // ================= DASHBOARD =================

        public IActionResult Dashboard()
        {
            var provider =
                HttpContext.Session.GetString("Provider");

            if (provider == null)
            {
                return RedirectToAction("Login");
            }

            var providerData =
                _providerRepo.GetAll()
                .FirstOrDefault(x =>
                    x.Name == provider);

            var totalServices =
                _serviceRepo.GetAll()
                .Where(x =>
                    x.ProviderName == provider)
                .Count();

            var pendingServices =
                _serviceRepo.GetAll()
                .Where(x =>
                    x.ProviderName == provider &&
                    x.Status == "Pending")
                .Count();

            var approvedServices =
                _serviceRepo.GetAll()
                .Where(x =>
                    x.ProviderName == provider &&
                    x.Status == "Approved")
                .Count();

            ViewBag.TotalServices =
                totalServices;

            ViewBag.PendingServices =
                pendingServices;

            ViewBag.ApprovedServices =
                approvedServices;

            ViewBag.ProviderName =
                provider;

            ViewBag.Provider =
                providerData;

            return View();
        }

        // ================= ADD SERVICE =================

        [HttpGet]
        public IActionResult AddService()
        {
            var provider =
                HttpContext.Session.GetString("Provider");

            if (provider == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Categories =
                _categoryRepo.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult AddService
        (
            Service service,
            IFormFile PhotoFile
        )
        {
            var provider =
                HttpContext.Session.GetString("Provider");

            if (provider == null)
            {
                return RedirectToAction("Login");
            }

            if
            (
                string.IsNullOrWhiteSpace(service.ServiceName) ||
                string.IsNullOrWhiteSpace(service.Description) ||
                service.Price <= 0 ||
                service.CategoryId <= 0
            )
            {
                TempData["Error"] =
                    "Please Fill All Service Information";

                ViewBag.Categories =
                    _categoryRepo.GetAll();

                return View();
            }

            if (PhotoFile == null)
            {
                TempData["Error"] =
                    "Please Upload Service Image";

                ViewBag.Categories =
                    _categoryRepo.GetAll();

                return View();
            }

            string fileName =
                Guid.NewGuid().ToString()
                + Path.GetExtension(PhotoFile.FileName);

            string folderPath = Path.Combine
            (
                Directory.GetCurrentDirectory(),
                "wwwroot/images"
            );

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath =
                Path.Combine(folderPath, fileName);

            using (var stream =
                new FileStream(fullPath, FileMode.Create))
            {
                PhotoFile.CopyTo(stream);
            }

            service.PhotoURL =
                "/images/" + fileName;

            var providerName =
                HttpContext.Session.GetString("Provider");

            var providerData =
                _providerRepo.GetAll()
                .FirstOrDefault(x =>
                    x.Name == providerName);

            service.ProviderName =
                providerName;

            service.Address =
                providerData?.Address;

            service.ProviderId =
                providerData.ProviderId;

            service.Status =
                "Pending";

            _serviceRepo.Add(service);

            TempData["Success"] =
                "Service Added Successfully";

            return RedirectToAction("ManageServices");
        }

        // ================= MANAGE SERVICES =================

        public IActionResult ManageServices()
        {
            var providerName =
                HttpContext.Session.GetString("Provider");

            if (providerName == null)
            {
                return RedirectToAction("Login");
            }

            var data = _serviceRepo.GetAll()
                .Where(x =>
                    x.ProviderName == providerName)
                .ToList();

            return View(data);
        }

        // ================= EDIT SERVICE =================

        [HttpGet]
        public IActionResult EditService(int id)
        {
            var provider =
                HttpContext.Session.GetString("Provider");

            if (provider == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Categories =
                _categoryRepo.GetAll();

            var data =
                _serviceRepo.GetById(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public IActionResult EditService
        (
            Service service,
            IFormFile? PhotoFile
        )
        {
            var provider =
                HttpContext.Session.GetString("Provider");

            if (provider == null)
            {
                return RedirectToAction("Login");
            }

            var oldData =
                _serviceRepo.GetById(service.ServiceId);

            if (oldData == null)
            {
                return NotFound();
            }

            if (PhotoFile != null)
            {
                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(PhotoFile.FileName);

                string folderPath = Path.Combine
                (
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images"
                );

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fullPath =
                    Path.Combine(folderPath, fileName);

                using (var stream =
                    new FileStream(fullPath, FileMode.Create))
                {
                    PhotoFile.CopyTo(stream);
                }

                service.PhotoURL =
                    "/images/" + fileName;
            }
            else
            {
                service.PhotoURL =
                    oldData.PhotoURL;
            }

            service.ProviderName =
                oldData.ProviderName;

            service.ProviderId =
                oldData.ProviderId;

            service.Address =
                oldData.Address;

            service.Status =
                "Pending";

            _serviceRepo.Update(service);

            TempData["Success"] =
                "Service Updated Successfully";

            return RedirectToAction("ManageServices");
        }

        // ================= DELETE SERVICE =================

        public IActionResult DeleteService(int id)
        {
            var provider =
                HttpContext.Session.GetString("Provider");

            if (provider == null)
            {
                return RedirectToAction("Login");
            }

            _serviceRepo.Delete(id);

            TempData["Success"] =
                "Service Deleted Successfully";

            return RedirectToAction("ManageServices");
        }

        // ================= EDIT PROFILE =================

        [HttpGet]
        public IActionResult EditProfile()
        {
            var providerName =
                HttpContext.Session.GetString("Provider");

            if (providerName == null)
            {
                return RedirectToAction("Login");
            }

            var provider =
                _providerRepo.GetAll()
                .FirstOrDefault(x =>
                    x.Name == providerName);

            return View(provider);
        }

        [HttpPost]
        public IActionResult EditProfile(Provider provider)
        {
            if
            (
                string.IsNullOrWhiteSpace(provider.Name) ||
                string.IsNullOrWhiteSpace(provider.Email) ||
                string.IsNullOrWhiteSpace(provider.Phone) ||
                string.IsNullOrWhiteSpace(provider.Address) ||
                string.IsNullOrWhiteSpace(provider.Password)
            )
            {
                ViewBag.Error = "Please Fill All Information";
                return View(provider);
            }

            
            var oldProvider =
                _providerRepo.GetById(provider.ProviderId);

            if (oldProvider == null)
            {
                return NotFound();
            }

           
            oldProvider.Name = provider.Name;
            oldProvider.Email = provider.Email;
            oldProvider.Phone = provider.Phone;
            oldProvider.Address = provider.Address;
            oldProvider.Password = provider.Password;

            _providerRepo.Update(oldProvider);

            
            var services = _serviceRepo.GetAll()
                .Where(x => x.ProviderId == provider.ProviderId)
                .ToList();

            foreach (var item in services)
            {
                item.ProviderName = provider.Name;
                item.Address = provider.Address;

                _serviceRepo.Update(item);
            }

            HttpContext.Session
                .SetString("Provider", provider.Name);

            TempData["Success"] =
                "Profile Updated Successfully";

            return RedirectToAction("Dashboard");
        }

        // ================= LOGOUT =================

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Provider");
            HttpContext.Session.Remove("ProviderId");

            TempData["Success"] =
                "Logout Successful";

            return RedirectToAction("Login");
        }
    }
}