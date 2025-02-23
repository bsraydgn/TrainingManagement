using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories;
public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(TrainingManagementDbContext context) : base(context)
    {
    }

    public async Task<Course?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }
}
