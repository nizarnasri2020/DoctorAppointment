using DoctorAppointment.Infrastructure.Persistence;

namespace DoctorAppointment.Application.Services
{


    public class DoctorService : IDoctorService
    {
        private readonly MedicalDbContext _context;

        public DoctorService(MedicalDbContext context)
        {
            _context = context;
        }

        public int GetDoctorsCount()
        {
            return _context.Doctors.Count();
        }
        public bool DoctorExists(int doctorId)
        {
            return _context.Doctors.Any(d => d.Id == doctorId);
        }

        public int GetAppointmentsCountForDoctor(int doctorId)
        {
            return _context.Appointments.Count(a => a.DoctorId == doctorId);
        }

        public bool HasAppointmentsToday(int doctorId)
        {
            DateTime today = DateTime.Today;

            return _context.Appointments.Any(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate.Date == today);
        }
    }

}
