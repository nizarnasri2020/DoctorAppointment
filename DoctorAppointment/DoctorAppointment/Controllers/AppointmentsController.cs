using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Enums;
using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly MedicalDbContext _context;


        public AppointmentsController(MedicalDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            appointment.Status = AppointmentStatus.Pending;
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return Ok(appointment);
        }


        [HttpGet("doctor/{doctorId}")]
        public IActionResult DoctorAppointments(int doctorId)
        {
            List<Appointment> list = _context.Appointments
            .Where(a => a.DoctorId == doctorId)
            .Include(a => a.Patient)
            .ToList();


            return Ok(list);
        }
    }
}
