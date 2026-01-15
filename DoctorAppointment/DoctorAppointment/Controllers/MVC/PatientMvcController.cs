using DoctorAppointment.Controllers.MVC.ViewModels;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Enums;
using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers.MVC
{
    public class PatientMvcController : Controller
    {
        private readonly MedicalDbContext _context;

        public PatientMvcController(MedicalDbContext context)
        {
            _context = context;
        }

        public IActionResult Doctors()
        {
            List<Doctor> doctors = _context.Doctors.Include(d => d.Specialty).ToList();
            return View(doctors);
        }

        // Matches /PatientMvc/Book/18
        public IActionResult Book(int id)
        {
            // id = doctorId
            BookAppointmentVm vm = new()
            {
                DoctorId = id
                // No PatientId needed
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Book(BookAppointmentVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // 1️⃣ Find existing patient by email
            Patient? patient = _context.Patients
                .FirstOrDefault(p => p.Email == vm.PatientEmail);

            // 2️⃣ Create patient if not exists
            if (patient == null)
            {
                patient = new Patient
                {
                    FullName = vm.PatientFullName,
                    Email = vm.PatientEmail
                };

                _context.Patients.Add(patient);
                _context.SaveChanges(); // patient.Id generated here
            }

            // 3️⃣ Create appointment
            Appointment appointment = new()
            {
                DoctorId = vm.DoctorId,
                PatientId = patient.Id,
                AppointmentDate = vm.AppointmentDate,
                Status = AppointmentStatus.Pending
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Doctors));
        }


        public IActionResult MyAppointments(int patientId)
        {
            List<Appointment> appointments = _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ToList();
            return View(appointments);
        }
        private int GetLoggedInPatientId()
        {
            return 1; // replace later with real auth
        }
    }

}
