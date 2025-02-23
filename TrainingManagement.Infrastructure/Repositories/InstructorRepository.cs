using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories;
public class InstructorRepository : Repository<Instructor>, IInstructorRepository
{
    public InstructorRepository(TrainingManagementDbContext context) : base(context)
    {
    }

    public async Task<Instructor?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(i => i.Name == name);
    }
}
