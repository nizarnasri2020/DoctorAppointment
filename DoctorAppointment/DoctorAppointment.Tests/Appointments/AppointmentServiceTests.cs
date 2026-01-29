using DoctorAppointment.Application.Services;
using DoctorAppointment.Domain.Entities;
using DoctorAppointment.Domain.Enums;
using Xunit;

namespace DoctorAppointment.Tests.Appointments
{
    public class AppointmentServiceTests
    {
        [Fact]
        public void BookAppointment_Creates_Appointment()
        {
            // Arrange
            Infrastructure.Persistence.MedicalDbContext context = TestDbContextFactory.Create();

            context.Doctors.Add(new Doctor { Id = 1, FullName = "Dr Wiem", Email = "wiem@test.com", SpecialtyId = 1 });
            context.Patients.Add(new Patient { Id = 1, FullName = "Patient Ahmed", Email = "ahmed@test.com" });
            context.SaveChanges();

            AppointmentService service = new(context);

            // Act
            Appointment appointment = service.BookAppointment(
                doctorId: 1,
                patientId: 1,
                appointmentDate: DateTime.Now.AddHours(1)
            );

            // Assert
            Assert.NotNull(appointment);
            Assert.Equal(AppointmentStatus.Pending, appointment.Status);
        }

        [Fact]
        public void BookAppointment_Throws_When_Date_In_Past()
        {
            Infrastructure.Persistence.MedicalDbContext context = TestDbContextFactory.Create();
            AppointmentService service = new(context);


            Assert.Throws<InvalidOperationException>(() =>
                service.BookAppointment(
                    doctorId: 1,
                    patientId: 1,
                    appointmentDate: DateTime.Now.AddDays(-1)
                )
            );
        }

        [Fact]
        public void BookAppointment_Throws_When_Doctor_Is_Double_Booked()
        {
            Infrastructure.Persistence.MedicalDbContext context = TestDbContextFactory.Create();

            context.Appointments.Add(new Appointment
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Today.AddHours(10),
                Status = AppointmentStatus.Pending
            });
            context.SaveChanges();

            AppointmentService service = new(context);

            Assert.Throws<InvalidOperationException>(() =>
                service.BookAppointment(
                    doctorId: 1,
                    patientId: 2,
                    appointmentDate: DateTime.Today.AddHours(10)
                )
            );
        }

        [Fact]
        public void HasConflict_Returns_True_When_Appointment_Exists()
        {
            Infrastructure.Persistence.MedicalDbContext context = TestDbContextFactory.Create();

            context.Appointments.Add(new Appointment
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Today
            });
            context.SaveChanges();

            AppointmentService service = new(context);

            bool conflict = service.HasConflict(1, DateTime.Today);

            Assert.True(conflict);
        }

        [Fact]
        public void CountPendingAppointments_Returns_Correct_Count()
        {
            Infrastructure.Persistence.MedicalDbContext context = TestDbContextFactory.Create();

            context.Appointments.AddRange(
                new Appointment { DoctorId = 1, Status = AppointmentStatus.Pending },
                new Appointment { DoctorId = 1, Status = AppointmentStatus.Pending },
                new Appointment { DoctorId = 1, Status = AppointmentStatus.Accepted }
            );
            context.SaveChanges();

            AppointmentService service = new(context);

            int count = service.CountPendingAppointments(1);

            Assert.Equal(2, count);
        }
    }
}
