namespace DoctorAppointment.Domain.Entities
{
    public class MedicalReport
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }


        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
