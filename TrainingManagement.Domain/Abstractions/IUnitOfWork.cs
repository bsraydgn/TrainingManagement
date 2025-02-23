namespace TrainingManagement.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IInstructorRepository Instructors { get; }
    ITeacherRepository Teachers { get; }
    IStudentRepository Students { get; }
    ICourseRepository Courses { get; }
    ITrainingRepository Trainings { get; }
    IStudentCourseRepository StudentsCourses { get; }

    ITrainingCourseRepository TrainingCourses { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync();
}
