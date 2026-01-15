namespace DoctorAppointment.Domain.Entities
{
    public class Patient : User
    {
        public ICollection<Appointment> Appointments { get; set; }
    }
}
