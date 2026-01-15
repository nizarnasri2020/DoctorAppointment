namespace DoctorAppointment.Domain.Entities
{
    public class Doctor : User
    {
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
