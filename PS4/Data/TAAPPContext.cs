using PS4.Models;
using Microsoft.EntityFrameworkCore;

namespace PS4.Data
{
    public class TAAPPContext : DbContext
    {
        public TAAPPContext(DbContextOptions<TAAPPContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Application>().ToTable("Application");
            modelBuilder.Entity<Applicant>().ToTable("Applicant");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        }

        public DbSet<PS4.Models.TimeSchedule> TimeSchedule { get; set; }
    }
}
