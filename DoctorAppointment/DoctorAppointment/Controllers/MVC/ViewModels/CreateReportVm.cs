namespace DoctorAppointment.Controllers.MVC.ViewModels
{
    public class CreateReportVm
    {
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; } = "";
        public string Prescription { get; set; } = "";
    }
}
