using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Enums;
using DoctorAppointment.Infrastructure.Persistence;

namespace DoctorAppointment.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly MedicalDbContext _context;

        public AppointmentService(MedicalDbContext context)
        {
            _context = context;
        }

        public Appointment BookAppointment(
            int doctorId,
            int patientId,
            DateTime appointmentDate)
        {
            if (appointmentDate < DateTime.Now)
                throw new InvalidOperationException("Cannot book appointment in the past");

            if (HasConflict(doctorId, appointmentDate))
                throw new InvalidOperationException("Doctor already has an appointment at this time");

            Appointment appointment = new()
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AppointmentDate = appointmentDate,
                Status = AppointmentStatus.Pending
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return appointment;
        }

        public bool HasConflict(int doctorId, DateTime appointmentDate)
        {
            return _context.Appointments.Any(a =>
                a.DoctorId == doctorId &&
                a.AppointmentDate == appointmentDate);
        }

        public int CountPendingAppointments(int doctorId)
        {
            return _context.Appointments.Count(a =>
                a.DoctorId == doctorId &&
                a.Status == AppointmentStatus.Pending);
        }
    }
}
