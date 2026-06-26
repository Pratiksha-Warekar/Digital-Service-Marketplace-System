using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DSM.Repository.Interface;
using DSM.Models.Entities;

namespace DSM.UI.Controllers
{
    public class AdminController : Controller
    {
        // ================= REPOSITORIES =================

        private readonly IUserRepository _userRepo;

        private readonly IProviderRepository _providerRepo;

        private readonly IServiceRepository _serviceRepo;

        // ================= CONSTRUCTOR =================

        public AdminController
        (
            IUserRepository userRepo,
            IProviderRepository providerRepo,
            IServiceRepository serviceRepo
        )
        {
            _userRepo = userRepo;

            _providerRepo = providerRepo;

            _serviceRepo = serviceRepo;
        }

        // ================= LOGIN =================

        [HttpGet]
        public IActionResult Login()
        {
            // ALREADY LOGIN
            if (HttpContext.Session.GetString("Admin") != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login
        (
            string username,
            string password
        )
        {
            if (username == "admin" &&
                password == "admin123")
            {
                // SESSION STORE

                HttpContext.Session
                    .SetString("Admin", "true");

                return RedirectToAction
                (
                    "Dashboard"
                );
            }

            ViewBag.Error =
                "Invalid Credentials";

            return View();
        }

        // ================= DASHBOARD =================

        public IActionResult Dashboard()
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction
                (
                    "Login"
                );
            }

            return View();
        }

        // ================= MANAGE USERS =================

        public IActionResult ManageUsers()
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _userRepo.GetAll();

            return View(data);
        }

        // ================= EDIT USER =================

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _userRepo.GetById(id);

            return View(data);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            _userRepo.Update(user);

            return RedirectToAction
            (
                "ManageUsers"
            );
        }

        // ================= DELETE USER =================

        public IActionResult DeleteUser(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            _userRepo.Delete(id);

            return RedirectToAction
            (
                "ManageUsers"
            );
        }

        // ================= MANAGE PROVIDERS =================

        public IActionResult ManageProviders()
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _providerRepo.GetAll();

            return View(data);
        }

        // ================= EDIT PROVIDER =================

        [HttpGet]
        public IActionResult EditProvider(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _providerRepo.GetById(id);

            return View(data);
        }

        [HttpPost]
        public IActionResult EditProvider
        (
            Provider provider
        )
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            _providerRepo.Update(provider);

            return RedirectToAction
            (
                "ManageProviders"
            );
        }

        // ================= DELETE PROVIDER =================

        public IActionResult DeleteProvider(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            _providerRepo.Delete(id);

            return RedirectToAction
            (
                "ManageProviders"
            );
        }

        // ================= MANAGE SERVICES =================

        public IActionResult ManageServices()
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _serviceRepo.GetAll();

            return View(data);
        }

        // ================= EDIT SERVICE =================

        [HttpGet]
        public IActionResult EditService(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _serviceRepo.GetById(id);

            return View(data);
        }

        [HttpPost]
        public IActionResult EditService
        (
            Service service
        )
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            _serviceRepo.Update(service);

            return RedirectToAction
            (
                "ManageServices"
            );
        }

        // ================= DELETE SERVICE =================

        public IActionResult DeleteService(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            _serviceRepo.Delete(id);

            return RedirectToAction
            (
                "ManageServices"
            );
        }

        // ================= APPROVE SERVICES =================

        public IActionResult ApproveServices()
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var data =
                _serviceRepo.GetAll();

            return View(data);
        }

        // ================= APPROVE =================

        public IActionResult Approve(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var service =
                _serviceRepo.GetById(id);

            if (service != null)
            {
                service.Status =
                    "Approved";

                _serviceRepo.Update(service);
            }

            return RedirectToAction
            (
                "ApproveServices"
            );
        }

        // ================= REJECT =================

        public IActionResult Reject(int id)
        {
            // SESSION CHECK

            if (HttpContext.Session
                .GetString("Admin") == null)
            {
                return RedirectToAction("Login");
            }

            var service =
                _serviceRepo.GetById(id);

            if (service != null)
            {
                service.Status =
                    "Rejected";

                _serviceRepo.Update(service);
            }

            return RedirectToAction
            (
                "ApproveServices"
            );
        }

        // ================= LOGOUT =================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction
            (
                "Login"
            );
        }
    }
}