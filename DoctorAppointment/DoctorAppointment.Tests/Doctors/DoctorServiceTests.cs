using DoctorAppointment.Application.Services;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Infrastructure.Persistence;
using Xunit;

namespace DoctorAppointment.Tests.Doctors
{
    public class DoctorServiceTests
    {
        [Fact]
        public void GetDoctorsCount_Returns_Correct_Count()
        {
            // Arrange
            MedicalDbContext context = TestDbContextFactory.Create();
            context.Doctors.AddRange(
                new Doctor { FullName = "Dr wiem", Email = "wiem@test.com", SpecialtyId = 1 },
                new Doctor { FullName = "Dr lamia", Email = "lamia@test.com", SpecialtyId = 1 }
            );
            context.SaveChanges();

            DoctorService service = new(context);

            // Act
            int count = service.GetDoctorsCount();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void DoctorExists_Returns_True_When_Doctor_Exists()
        {
            // Arrange
            MedicalDbContext context = TestDbContextFactory.Create();
            context.Doctors.Add(new Doctor
            {
                Id = 1,
                FullName = "Dr wiem",
                Email = "wiem@test.com",
                SpecialtyId = 1
            });
            context.SaveChanges();

            DoctorService service = new(context);

            // Act
            bool exists = service.DoctorExists(1);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void DoctorExists_Returns_False_When_Doctor_Does_Not_Exist()
        {
            // Arrange
            MedicalDbContext context = TestDbContextFactory.Create();
            DoctorService service = new(context);

            // Act
            bool exists = service.DoctorExists(99);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public void GetAppointmentsCountForDoctor_Returns_Correct_Count()
        {
            // Arrange
            MedicalDbContext context = TestDbContextFactory.Create();

            context.Appointments.AddRange(
                new Appointment { DoctorId = 1, PatientId = 1, AppointmentDate = DateTime.Today },
                new Appointment { DoctorId = 1, PatientId = 2, AppointmentDate = DateTime.Today },
                new Appointment { DoctorId = 2, PatientId = 3, AppointmentDate = DateTime.Today }
            );
            context.SaveChanges();

            DoctorService service = new(context);

            // Act
            int count = service.GetAppointmentsCountForDoctor(1);

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void HasAppointmentsToday_Returns_True_When_Appointment_Exists()
        {
            // Arrange
            MedicalDbContext context = TestDbContextFactory.Create();

            context.Appointments.Add(new Appointment
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Today
            });
            context.SaveChanges();

            DoctorService service = new(context);

            // Act
            bool hasAppointments = service.HasAppointmentsToday(1);

            // Assert
            Assert.True(hasAppointments);
        }

        [Fact]
        public void HasAppointmentsToday_Returns_False_When_No_Today_Appointments()
        {
            // Arrange
            MedicalDbContext context = TestDbContextFactory.Create();

            context.Appointments.Add(new Appointment
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Today.AddDays(-1)
            });
            context.SaveChanges();

            DoctorService service = new(context);

            // Act
            bool hasAppointments = service.HasAppointmentsToday(1);

            // Assert
            Assert.False(hasAppointments);
        }
    }
}
