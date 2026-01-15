using DoctorAppointment.Controllers.MVC.ViewModels;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers.MVC
{
    public class AdminMvcController : Controller
    {
        private readonly MedicalDbContext _context;

        public AdminMvcController(MedicalDbContext context)
        {
            _context = context;
        }

        // List all doctors
        public IActionResult Doctors()
        {
            List<Doctor> doctors = _context.Doctors.Include(d => d.Specialty).ToList();
            return View(doctors);
        }

        // GET: Show form
        public IActionResult CreateDoctor()
        {
            CreateDoctorVm vm = new()
            {
                Specialties = DefaultSpecialties
            };
            return View(vm);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateDoctorDebug()
        //{
        //    Dictionary<string, string> values = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
        //    return Json(values);
        //}

        // POST: Save doctor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDoctor(CreateDoctorVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Specialties = DefaultSpecialties;
                return View(vm);
            }

            // Check if specialty exists
            Specialty? specialty = _context.Specialties
                .FirstOrDefault(s => s.Name == vm.SelectedSpecialty);

            if (specialty == null)
            {
                specialty = new Specialty
                {
                    Name = vm.SelectedSpecialty
                };
                _context.Specialties.Add(specialty);
                _context.SaveChanges();
            }

            Doctor doctor = new()
            {
                FullName = vm.FullName,
                Email = vm.Email,
                SpecialtyId = specialty.Id
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Doctors));
        }

        // Delete doctor
        public IActionResult DeleteDoctor(int id)
        {
            Doctor? doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Doctors));
        }

        // Default specialties
        private static readonly List<string> DefaultSpecialties = new()
        {
            "Cardiology",
            "Dermatology",
            "Neurology",
            "Pediatrics",
            "Orthopedics",
            "General Medicine"
        };
    }

}
