namespace DoctorAppointment.Controllers.MVC.ViewModels
{
    public class CreateDoctorVm
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";

        // Selected specialty from dropdown
        public string SelectedSpecialty { get; set; } = "";

        // List of options for dropdown
        public List<string> Specialties { get; set; } = new();
    }
}
