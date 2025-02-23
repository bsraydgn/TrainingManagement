using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Domain.Abstractions;

public interface IStudentCourseRepository : IRepository<StudentCourse>
{
    Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(int courseId);
    Task<StudentCourse> GetStudentCourseAsync(int studentId, int courseId);
    Task<IEnumerable<StudentCourse>> GetByCourseIdsAsync(IEnumerable<int> courseIds);
}

