namespace DoctorAppointment.Domain.Entities
{
    public abstract class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
