using DoctorAppointment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Infrastructure.Persistence
{
    public class MedicalDbContext : DbContext
    {
        public MedicalDbContext(DbContextOptions<MedicalDbContext> options)
        : base(options) { }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalReport> MedicalReports { get; set; }
    }
}
