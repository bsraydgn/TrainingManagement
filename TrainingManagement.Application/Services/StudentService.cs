using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;

namespace TrainingManagement.Application.Services;
public class StudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.Students.GetAllAsync();
        return students.Select(s => new StudentDto { Id = s.Id, Name = s.Name, Surname = s.Surname });
    }
}
