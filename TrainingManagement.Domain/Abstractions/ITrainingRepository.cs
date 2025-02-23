using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Domain.Abstractions;
public interface ITrainingRepository : IRepository<Training>
{
    Task<IEnumerable<Training>> GetAllWithCoursesAsync();

    Task<Training?> GetWithCoursesAsync(int id);

    Task<Training?> GetTrainingByCourseIdAsync(int courseId);
}

