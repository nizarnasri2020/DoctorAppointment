using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Controllers.MVC.ViewModels
{
    public class BookAppointmentVm
    {
        public int DoctorId { get; set; }

        [Required]
        public string PatientFullName { get; set; }

        [Required, EmailAddress]
        public string PatientEmail { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }
    }

}
