using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-T2HJAMS;Initial Catalog=Com;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
        public DbSet<StudentModel> Students { get; set; }
       
        public DbSet<Cours> Courses { get; set; }
        public DbSet<Cours_stds> CourseStudents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
       
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cours_stds>()
                .HasKey(cs => new { cs.StudentId, cs.CourseId });

            modelBuilder.Entity<Cours_stds>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);

            modelBuilder.Entity<Cours_stds>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);
        }
    }
}
