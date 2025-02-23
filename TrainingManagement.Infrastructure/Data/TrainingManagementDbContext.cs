using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Infrastructure.Data;

public class TrainingManagementDbContext(DbContextOptions<TrainingManagementDbContext> options) : DbContext(options)
{

    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<TrainingCourse> TrainingCourses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Instructor>()
            .HasData(
                new Instructor { Id = 1, Name = "Büşra Ortayol" },
                new Instructor { Id = 2, Name = "Barış Aydoğan" }
            );

        modelBuilder.Entity<Student>()
            .HasData(
                new Student { Id = 1, Name = "Ayşe", Surname = "Yılmaz" },
                new Student { Id = 2, Name = "Mustafa", Surname = "Ortayol" },
                new Student { Id = 3, Name = "Fatma", Surname = "Akgül" }
            );

        modelBuilder.Entity<Teacher>()
            .HasData(
                new Teacher { Id = 1, Name = "Serap", Surname = "Uslu" }
            );

        modelBuilder.Entity<Course>()
            .HasData(
                new Course { Id = 1, Name = "Matematik" }
            );

        modelBuilder.Entity<Training>()
            .HasOne(t => t.Teacher)
            .WithMany(t => t.Trainings)
            .HasForeignKey(t => t.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Training>()
            .HasOne(t => t.Instructor)
            .WithMany()
            .HasForeignKey(t => t.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TrainingCourse>()
            .HasOne(tc => tc.Training)
            .WithMany(t => t.TrainingCourses)
            .HasForeignKey(tc => tc.TrainingId);

        modelBuilder.Entity<TrainingCourse>()
            .HasOne(tc => tc.Course)
            .WithMany()
            .HasForeignKey(tc => tc.CourseId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany()
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany()
            .HasForeignKey(sc => sc.CourseId);
    }
}
