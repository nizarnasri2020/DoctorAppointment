using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoctorAppointment.Infrastructure.Persistence
{


    public class MedicalDbContextFactory
      : IDesignTimeDbContextFactory<MedicalDbContext>
    {
        public MedicalDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MedicalDbContext> optionsBuilder = new();

            string dbPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                "medical.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new MedicalDbContext(optionsBuilder.Options);
        }
    }


}
