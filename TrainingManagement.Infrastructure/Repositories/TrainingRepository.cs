using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories
{
    public class TrainingRepository : Repository<Training>, ITrainingRepository
    {
        public TrainingRepository(TrainingManagementDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Training>> GetAllWithCoursesAsync()
        {
            return await _dbSet
                .Include(t => t.TrainingCourses)
                .ToListAsync();
        }

        public async Task<Training?> GetWithCoursesAsync(int id)
        {
            return await _dbSet.Include(t => t.TrainingCourses).FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task<Training?> GetTrainingByCourseIdAsync(int courseId)
        {
            return await _dbSet
                .Include(t => t.Teacher)
                .Include(t => t.TrainingCourses)
                    .ThenInclude(tc => tc.Course)
                .FirstOrDefaultAsync(t => t.TrainingCourses.Any(tc => tc.CourseId == courseId));
        }
    }
}   
