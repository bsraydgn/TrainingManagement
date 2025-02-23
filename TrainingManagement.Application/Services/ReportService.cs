using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;

namespace TrainingManagement.Application.Services;

public class ReportService
{
    private readonly IUnitOfWork _unitOfWork;
    public ReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReportDto?> GetStudentStatusReportAsync(int courseId)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(courseId);
        if (course == null) return null;

        var training = await _unitOfWork.Trainings.GetTrainingByCourseIdAsync(courseId);
        string teacherName = training?.Teacher != null
            ? $"{training.Teacher.Name} {training.Teacher.Surname}"
            : "N/A";

        var studentCourses = await _unitOfWork.StudentsCourses.GetByCourseIdAsync(courseId);
        var passedStudents = new List<string>();
        var failedStudents = new List<string>();

        foreach (var sc in studentCourses)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(sc.StudentId);
            if (student != null)
            {
                string fullName = $"{student.Name} {student.Surname}";
                if (sc.Grade >= 3)
                    passedStudents.Add(fullName);
                else
                    failedStudents.Add(fullName);
            }
        }

        return new ReportDto
        {
            Course = course.Name,
            Teacher = teacherName,
            PassedStudents = passedStudents,
            FailedStudents = failedStudents
        };
    }
}
