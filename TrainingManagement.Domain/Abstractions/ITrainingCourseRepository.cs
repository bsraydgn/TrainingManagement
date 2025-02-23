using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Domain.Abstractions;
public interface ITrainingCourseRepository : IRepository<TrainingCourse>
{
    Task<IEnumerable<TrainingCourse>> GetByTrainingIdAsync(int trainingId);
}
