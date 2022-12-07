using CarsService.Database.Entity;
using CarsService.Database.Views;
using Microsoft.EntityFrameworkCore;

namespace CarsService.Database
{
    public class DatabaseContext: DbContext
    {
        #region Properties

        public DbSet<Car>? Cars { get; set; }
        public DbSet<TechExam>? Exams { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<CarDetailsView>? CarDetailsView { get; set; }

        #endregion

        #region Override

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\CarsApp;Database=CarsApp;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDetailsView>(cd =>
            {
                cd.HasNoKey();
            });
        }

        #endregion
    }
}
