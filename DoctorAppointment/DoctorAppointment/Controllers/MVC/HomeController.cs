using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Controllers.MVC
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
