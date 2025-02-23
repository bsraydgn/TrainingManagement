using Microsoft.EntityFrameworkCore;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;
using TrainingManagement.Infrastructure.Data;

namespace TrainingManagement.Infrastructure.Repositories;
public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(TrainingManagementDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Student>> GetBySurnameAsync(string surname)
    {
        return await _dbSet.Where(s => s.Surname.Contains(surname)).ToListAsync();
    }
}
