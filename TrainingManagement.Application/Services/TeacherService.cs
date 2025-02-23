using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Application.Services;

public class TeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    public TeacherService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TeacherDto>> GetAllTeachersAsync()
    {
        var teachers = await _unitOfWork.Teachers.GetAllAsync();
        return teachers.Select(t => new TeacherDto
        {
            Id = t.Id,
            Name = t.Name,
            Surname = t.Surname
        });
    }

    public async Task<TeacherDto?> GetTeacherByIdAsync(int id)
    {
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
        if (teacher == null) return null;
        return new TeacherDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Surname = teacher.Surname
        };
    }

    public async Task<TeacherDto> CreateTeacherAsync(CreateTeacherDto teacherDto)
    {
        var teacher = new Teacher
        {
            Name = teacherDto.Name,
            Surname = teacherDto.Surname
        };
        await _unitOfWork.Teachers.AddAsync(teacher);
        await _unitOfWork.SaveChangesAsync();

        return new TeacherDto { Id = teacher.Id, Name = teacher.Name, Surname = teacher.Surname };
    }

    public async Task UpdateTeacherAsync(int id, CreateTeacherDto teacherDto)
    {
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
        if (teacher == null) return;
        teacher.Name = teacherDto.Name;
        teacher.Surname = teacherDto.Surname;
        _unitOfWork.Teachers.Update(teacher);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteTeacherAsync(int id)
    {
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
        if (teacher == null) return;
        _unitOfWork.Teachers.Remove(teacher);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task GradeStudentAsync(TeacherGradeStudentDto gradeDto)
    {
        var teacher = await _unitOfWork.Teachers.GetByIdAsync(gradeDto.TeacherId);
        if (teacher == null) return;

        var studentCourse = await _unitOfWork.StudentsCourses.GetStudentCourseAsync(gradeDto.StudentId, gradeDto.CourseId);
        if (studentCourse == null) return;

        studentCourse.Grade = gradeDto.Grade;
        _unitOfWork.StudentsCourses.Update(studentCourse);
        await _unitOfWork.SaveChangesAsync();
    }
}

