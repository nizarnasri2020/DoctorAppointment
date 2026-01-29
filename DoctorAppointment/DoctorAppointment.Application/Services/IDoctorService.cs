namespace DoctorAppointment.Application.Services
{
    public interface IDoctorService
    {
        int GetDoctorsCount();
        bool DoctorExists(int doctorId);
        int GetAppointmentsCountForDoctor(int doctorId);
        bool HasAppointmentsToday(int doctorId);
    }
}
