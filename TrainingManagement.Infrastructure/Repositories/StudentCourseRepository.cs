using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories;
public class StudentCourseRepository : Repository<StudentCourse>, IStudentCourseRepository
{
    public StudentCourseRepository(TrainingManagementDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<StudentCourse>> GetByCourseIdAsync(int courseId)
    {
        return await _dbSet.Where(sc => sc.CourseId == courseId).ToListAsync();
    }

    public async Task<StudentCourse?> GetStudentCourseAsync(int studentId, int courseId)
    {
        return await _dbSet.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
    }

    public async Task<IEnumerable<StudentCourse>> GetByCourseIdsAsync(IEnumerable<int> courseIds)
    {
        return await _dbSet.Where(sc => courseIds.Contains(sc.CourseId)).ToListAsync();
    }
}
