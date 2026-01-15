using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly MedicalDbContext _context;

        public AdminController(MedicalDbContext context)
        {
            _context = context;
        }

        // GET: List all doctors
        [HttpGet("doctors")]
        public IActionResult GetDoctors()
        {
            List<Doctor> doctors = _context.Doctors.Include(d => d.Specialty).ToList();
            return Ok(doctors);
        }

        // POST: Add a new doctor
        [HttpPost("doctors")]
        public IActionResult AddDoctor([FromBody] Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return Ok(doctor);
        }

        // DELETE: Remove a doctor
        [HttpDelete("doctors/{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            Doctor? doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT: Update doctor details
        [HttpPut("doctors/{id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] Doctor updatedDoctor)
        {
            Doctor? doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();

            doctor.FullName = updatedDoctor.FullName;
            doctor.Email = updatedDoctor.Email;
            doctor.SpecialtyId = updatedDoctor.SpecialtyId;

            _context.SaveChanges();
            return Ok(doctor);
        }
    }

}
