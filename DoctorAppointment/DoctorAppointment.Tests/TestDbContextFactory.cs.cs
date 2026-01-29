using DoctorAppointment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointment.Tests
{
    public static class TestDbContextFactory
    {
        public static MedicalDbContext Create()
        {
            DbContextOptions<MedicalDbContext> options = new DbContextOptionsBuilder<MedicalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MedicalDbContext(options);
        }
    }
}
