using DoctorAppointment.Domain.Entities;

namespace DoctorAppointment.Application.Services
{
    public interface IAppointmentService
    {
        Appointment BookAppointment(
            int doctorId,
            int patientId,
            DateTime appointmentDate
        );

        bool HasConflict(int doctorId, DateTime appointmentDate);

        int CountPendingAppointments(int doctorId);
    }
}
