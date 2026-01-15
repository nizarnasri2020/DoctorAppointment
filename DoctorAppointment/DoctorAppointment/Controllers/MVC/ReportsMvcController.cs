using DoctorAppointment.Controllers.MVC.ViewModels;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Controllers.MVC
{
    public class ReportsMvcController : Controller
    {
        private readonly MedicalDbContext _context;

        public ReportsMvcController(MedicalDbContext context)
        {
            _context = context;
        }

        // GET: /ReportsMvc/Index?doctorId=1
        public IActionResult Index(int doctorId)
        {
            // Get all reports for this doctor
            List<MedicalReport> reports = _context.MedicalReports
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Patient)
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Doctor)
                .Where(r => r.Appointment.DoctorId == doctorId)
                .ToList();

            // Pass appointments to the view for creating a new report
            List<Appointment> appointments = _context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToList();

            ViewBag.DoctorId = doctorId;
            ViewBag.Appointments = appointments;

            return View(reports);
        }


        // GET: Show form to create report
        public IActionResult Create(int appointmentId)
        {
            CreateReportVm vm = new()
            {
                AppointmentId = appointmentId
            };
            return View(vm);
        }


        // POST: Save report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateReportVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            MedicalReport report = new()
            {
                AppointmentId = vm.AppointmentId,
                Diagnosis = vm.Diagnosis,
                Prescription = vm.Prescription,
                CreatedAt = DateTime.UtcNow
            };

            _context.MedicalReports.Add(report);
            _context.SaveChanges();

            // Redirect to doctor's report list
            int doctorId = _context.Appointments
                .Where(a => a.Id == vm.AppointmentId)
                .Select(a => a.DoctorId)
                .FirstOrDefault();

            return RedirectToAction(nameof(Index), new { doctorId });
        }
    }
}
