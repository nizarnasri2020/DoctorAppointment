namespace DoctorAppointment.Domain.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
