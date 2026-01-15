using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointment.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly MedicalDbContext _context;


        public ReportsController(MedicalDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Create(MedicalReport report)
        {
            report.CreatedAt = DateTime.UtcNow;
            _context.MedicalReports.Add(report);
            _context.SaveChanges();
            return Ok(report);
        }
    }
}
