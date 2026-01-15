using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly MedicalDbContext _context;
        public DoctorsController(MedicalDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_context.Doctors.Include(d => d.Specialty).ToList());
        }
    }
}
