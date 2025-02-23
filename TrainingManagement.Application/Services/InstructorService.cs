using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;

namespace TrainingManagement.Application.Services;

public class InstructorService
{
    private readonly IUnitOfWork _unitOfWork;
    public InstructorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
    {
        var instructors = await _unitOfWork.Instructors.GetAllAsync();
        return instructors.Select(i => new InstructorDto
        {
            Id = i.Id,
            Name = i.Name
        });
    }

    public async Task<InstructorDto?> GetInstructorByIdAsync(int id)
    {
        var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
        if (instructor == null) return null;
        return new InstructorDto
        {
            Id = instructor.Id,
            Name = instructor.Name
        };
    }
}
