using DSM.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DSM.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
