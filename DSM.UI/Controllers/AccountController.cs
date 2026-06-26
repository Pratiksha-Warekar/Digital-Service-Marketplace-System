using DSM.Models.Entities;
using DSM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IUserRepository _userRepository;

        public AccountController
        (
            IUserRepository userRepository
        )
        {
            _userRepository =
                userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if
            (
                string.IsNullOrWhiteSpace(user.Name) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password) ||
                string.IsNullOrWhiteSpace(user.Phone) ||
                string.IsNullOrWhiteSpace(user.Address)
            )
            {
                ViewBag.Error =
                    "Please Fill All Information";

                return View(user);
            }

            user.RegisterDate =
                DateTime.Now;

            _userRepository.Add(user);

            TempData["Success"] =
                "Registration Successful";

            return RedirectToAction("Login");
        }

       

        [HttpGet]
        public IActionResult Login()
        {
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

            var user =
                _userRepository.Login
                (
                    email,
                    password
                );

            if (user != null)
            {
                HttpContext.Session
                    .SetString
                    (
                        "UserName",
                        user.Name ?? ""
                    );

                HttpContext.Session
                    .SetInt32
                    (
                        "UserId",
                        user.UserId
                    );

                TempData["Success"] =
                    "Login Successful";

                return RedirectToAction
                (
                    "Dashboard"
                );
            }

            ViewBag.Error =
                "Invalid Email or Password";

            return View();
        }

       

        public IActionResult Dashboard()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user =
                _userRepository.GetById(userId.Value);

            ViewBag.User =
                user;

            return View();
        }


        [HttpGet]
        public IActionResult EditProfile()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user =
                _userRepository.GetById(userId.Value);

            return View(user);
        }

        

        [HttpPost]
        public IActionResult EditProfile(User user)
        {
            _userRepository.Update(user);

            TempData["Success"] =
                "Profile Updated Successfully";

            return RedirectToAction("Dashboard");
        }

        

        public IActionResult DeleteAccount()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                _userRepository.Delete(userId.Value);

                HttpContext.Session.Clear();
            }

            TempData["Success"] =
                "Account Deleted Successfully";

            return RedirectToAction("Register");
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            TempData["Success"] =
                "Logout Successful";

            return RedirectToAction("Login");
        }
    }
}