using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TrainingManagementDbContext _context;

    public IInstructorRepository Instructors { get; }
    public ITeacherRepository Teachers { get; }
    public IStudentRepository Students { get; }
    public ICourseRepository Courses { get; }
    public ITrainingRepository Trainings { get; }
    public IStudentCourseRepository StudentsCourses { get; }

    public ITrainingCourseRepository TrainingCourses { get; }

    public UnitOfWork(TrainingManagementDbContext context,
                      IInstructorRepository instructorRepository,
                      ITeacherRepository teacherRepository,
                      IStudentRepository studentRepository,
                      ICourseRepository courseRepository,
                      ITrainingRepository trainingRepository,
                      ITrainingCourseRepository trainingCourseRepository,
                      IStudentCourseRepository studentCourseRepository)
    {
        _context = context;
        Instructors = instructorRepository;
        Teachers = teacherRepository;
        Students = studentRepository;
        Courses = courseRepository;
        Trainings = trainingRepository;
        TrainingCourses = trainingCourseRepository;
        StudentsCourses = studentCourseRepository;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
