using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Constants;
using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Application.Services;

public class StudentCourseService
{
    private readonly IUnitOfWork _unitOfWork;
    public StudentCourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task StartCourseAsync(StudentStartCourseDto startDto)
    {
        var studentCourse = await _unitOfWork.StudentsCourses.GetStudentCourseAsync(startDto.StudentId, startDto.CourseId);
        if (studentCourse == null)
        {
            studentCourse = new StudentCourse
            {
                StudentId = startDto.StudentId,
                CourseId = startDto.CourseId,
                Status = CourseStatusValues.InProgress,
            };
            await _unitOfWork.StudentsCourses.AddAsync(studentCourse);
        }
        else
        {
            studentCourse.Status = CourseStatusValues.InProgress;
            _unitOfWork.StudentsCourses.Update(studentCourse);
        }
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CompleteCourseAsync(StudentCompleteCourseDto completeDto)
    {
        var studentCourse = await _unitOfWork.StudentsCourses.GetStudentCourseAsync(completeDto.StudentId, completeDto.CourseId);
        if (studentCourse == null) return;
        studentCourse.Status = CourseStatusValues.Completed;
        _unitOfWork.StudentsCourses.Update(studentCourse);
        await _unitOfWork.SaveChangesAsync();
    }
}
