using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Application.Services;

public class CourseService
{
    private readonly IUnitOfWork _unitOfWork;
    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null) return null;
        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name
        };
    }

    public async Task<CourseDto> CreateCourseAsync(CreateCourseDto courseDto)
    {
        var course = new Course { Name = courseDto.Name };
        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.SaveChangesAsync();

        return new CourseDto { Id = course.Id, Name = course.Name};
    }

    public async Task UpdateCourseAsync(int id, CreateCourseDto courseDto)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null) return;
        course.Name = courseDto.Name;
        _unitOfWork.Courses.Update(course);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null) return;
        _unitOfWork.Courses.Remove(course);
        await _unitOfWork.SaveChangesAsync();
    }
}
