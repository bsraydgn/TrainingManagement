using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(TrainingManagementDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Teacher>> GetBySurnameAsync(string surname)
        {
            return await _dbSet.Where(t => t.Surname.Contains(surname)).ToListAsync();
        }
    }
}
