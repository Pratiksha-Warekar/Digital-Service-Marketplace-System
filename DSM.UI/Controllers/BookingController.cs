using DSM.Models.Entities;
using DSM.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DSM.UI.Controllers
{
    public class BookingController : Controller
    {

        private readonly IBookingRepository _bookingRepo;

        private readonly IServiceRepository _serviceRepo;


        public BookingController
        (
            IBookingRepository bookingRepo,
            IServiceRepository serviceRepo
        )
        {
            _bookingRepo = bookingRepo;

            _serviceRepo = serviceRepo;
        }

        

        public IActionResult CheckLogin(int id)
        {
            var user =
                HttpContext.Session.GetInt32("UserId");

            // USER NOT LOGIN

            if (user == null)
            {
                TempData["Error"] =
                    "Please Login First To Book Service";

                return RedirectToAction
                (
                    "Login",
                    "Account"
                );
            }

            // USER LOGIN

            return RedirectToAction
            (
                "Create",
                new { serviceId = id }
            );
        }

       

        [HttpGet]
        public IActionResult Create(int serviceId)
        {
            var user =
                HttpContext.Session.GetInt32("UserId");

          

            if (user == null)
            {
                return RedirectToAction
                (
                    "Login",
                    "Account"
                );
            }

            

            var service =
                _serviceRepo.GetById(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            // BOOKING MODEL

            BookingRequest booking =
                new BookingRequest();

            booking.ServiceId =
                service.ServiceId;

            booking.ServiceName =
                service.ServiceName;

            booking.ProviderName =
                service.ProviderName;

            booking.Price =
                service.Price;

            booking.PhotoURL =
                service.PhotoURL;

            booking.UserName =
                HttpContext.Session.GetString("UserName");

            return View(booking);
        }

      
        [HttpPost]
        public IActionResult Create(BookingRequest model)
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            var userName =
                HttpContext.Session.GetString("UserName");

            // LOGIN CHECK

            if (userId == null)
            {
                return RedirectToAction
                (
                    "Login",
                    "Account"
                );
            }

            // GET SERVICE DETAILS

            var service =
                _serviceRepo.GetById(model.ServiceId);

            if (service != null)
            {
                model.ServiceName =
                    service.ServiceName;

                model.ProviderName =
                    service.ProviderName;

                model.Price =
                    service.Price;

                model.PhotoURL =
                    service.PhotoURL;
            }

            // SAVE USER DATA

            model.UserId =
                userId.Value;

            model.UserName =
                userName;

            model.Status =
                "Pending";

            model.BookingDate =
                DateTime.Now;

            // SAVE BOOKING

            _bookingRepo.AddBooking(model);

            // SUCCESS MESSAGE

            TempData["Success"] =
                "Booking Successfully Confirmed";

            // REDIRECT SUCCESS PAGE

            return RedirectToAction("Success");
        }

        
        public IActionResult Success()
        {
            return View();
        }


        public IActionResult MyBookings()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            var data =
                _bookingRepo.GetAllBookings()
                .Where(x => x.UserId == userId)
                .ToList();

            return View(data);
        }

       

        public IActionResult BookingRequests()
        {
            // PROVIDER LOGIN CHECK

            var providerName =
                HttpContext.Session
                .GetString("Provider");

            if (providerName == null)
            {
                return RedirectToAction
                (
                    "Login",
                    "Provider"
                );
            }

            
            var data =
                _bookingRepo.GetAllBookings()
                .Where(x =>
                    x.ProviderName == providerName)
                .ToList();

            return View(data);
        }

        

        public IActionResult Approve(int id)
        {
            _bookingRepo.UpdateStatus
            (
                id,
                "Approved"
            );

            TempData["Success"] =
                "Booking Approved Successfully";

            return RedirectToAction
            (
                "BookingRequests"
            );
        }

       

        public IActionResult Reject(int id)
        {
            _bookingRepo.UpdateStatus
            (
                id,
                "Rejected"
            );

            TempData["Error"] =
                "Booking Rejected";

            return RedirectToAction
            (
                "BookingRequests"
            );
        }
    }
}