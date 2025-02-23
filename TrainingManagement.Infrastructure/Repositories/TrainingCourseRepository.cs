using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories
{
    public class TrainingCourseRepository : Repository<TrainingCourse>, ITrainingCourseRepository
    {
        public TrainingCourseRepository(TrainingManagementDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TrainingCourse>> GetByTrainingIdAsync(int trainingId)
        {
            return await _dbSet.Where(tc => tc.TrainingId == trainingId).ToListAsync();
        }
    }
}
