using CarsService.Database.Entity;
using CarsService.Database.Views;
using Microsoft.EntityFrameworkCore;

namespace CarsService.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<TechExam> Exams { get; set; }
        public DbSet<CarDetailsView> CarDetailsView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Database=CarsApp;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDetailsView>(cd =>
            {
                cd.HasNoKey();
            });
        }
    }
}
