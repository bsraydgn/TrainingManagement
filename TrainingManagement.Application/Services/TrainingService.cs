using TrainingManagement.Application.DTOs;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Domain.Constants;
using TrainingManagement.Domain.Entities;

namespace TrainingManagement.Application.Services;

public class TrainingService
{
    private readonly IUnitOfWork _unitOfWork;
    public TrainingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TrainingDto>> GetAllTrainingsAsync()
    {
        var trainings = await _unitOfWork.Trainings.GetAllWithCoursesAsync();
        var trainingDtos = new List<TrainingDto>();

        foreach (var training in trainings)
        {
            var courseIds = training.TrainingCourses?.Select(tc => tc.CourseId).ToList() ?? new List<int>();

            var studentCourses = await _unitOfWork.StudentsCourses.GetByCourseIdsAsync(courseIds);
            var studentIds = studentCourses.Select(sc => sc.StudentId).Distinct().ToList();

            trainingDtos.Add(new TrainingDto
            {
                Id = training.Id,
                Name = training.Name,
                TeacherId = training.TeacherId,
                InstructorId = training.InstructorId,
                CourseIds = courseIds,
                StudentIds = studentIds
            });
        }

        return trainingDtos;
    }

    public async Task<TrainingDto?> GetTrainingByIdAsync(int id)
    {
        var training = await _unitOfWork.Trainings.GetWithCoursesAsync(id);
        if (training == null)
            return null;

        var courseIds = training.TrainingCourses?.Select(tc => tc.CourseId).ToList() ?? new List<int>();

        var studentCourses = await _unitOfWork.StudentsCourses.GetByCourseIdsAsync(courseIds);
        var studentIds = studentCourses.Select(sc => sc.StudentId).Distinct().ToList();

        return new TrainingDto
        {
            Id = training.Id,
            Name = training.Name,
            TeacherId = training.TeacherId,
            InstructorId = training.InstructorId,
            CourseIds = courseIds,
            StudentIds = studentIds
        };
    }

    public async Task<TrainingDto> CreateTrainingAsync(CreateTrainingDto trainingDto)
    {
        var training = new Training
        {
            Name = trainingDto.Name,
            TeacherId = trainingDto.TeacherId,
            InstructorId = trainingDto.InstructorId
        };

        await _unitOfWork.Trainings.AddAsync(training);
        await _unitOfWork.SaveChangesAsync();

        if (trainingDto.CourseIds != null)
        {
            foreach (var courseId in trainingDto.CourseIds)
            {
                var trainingCourse = new TrainingCourse
                {
                    TrainingId = training.Id,
                    CourseId = courseId
                };
                await _unitOfWork.TrainingCourses.AddAsync(trainingCourse);
            }
        }

        if (trainingDto.StudentIds != null && trainingDto.CourseIds != null)
        {
            foreach (var studentId in trainingDto.StudentIds)
            {
                foreach (var courseId in trainingDto.CourseIds)
                {
                    var studentCourse = new StudentCourse
                    {
                        StudentId = studentId,
                        CourseId = courseId,
                        Status = CourseStatusValues.NotStarted,
                    };
                    await _unitOfWork.StudentsCourses.AddAsync(studentCourse);
                }
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return new TrainingDto
        {
            Id = training.Id,
            Name = training.Name,
            InstructorId = training.InstructorId,
            TeacherId = training.TeacherId,
            CourseIds = trainingDto.CourseIds,
            StudentIds = trainingDto.StudentIds
        };
    }

    public async Task UpdateTrainingAsync(int id, CreateTrainingDto trainingDto)
    {
        var training = await _unitOfWork.Trainings.GetWithCoursesAsync(id);
        if (training == null)
            return;

        training.Name = trainingDto.Name;
        training.TeacherId = trainingDto.TeacherId;
        training.InstructorId = trainingDto.InstructorId;
        _unitOfWork.Trainings.Update(training);
        await _unitOfWork.SaveChangesAsync();

        if (training.TrainingCourses != null)
        {
            var existingTrainingCourses = training.TrainingCourses.ToList();
            foreach (var tc in existingTrainingCourses)
            {
                _unitOfWork.TrainingCourses.Remove(tc);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        if (trainingDto.CourseIds != null)
        {
            foreach (var courseId in trainingDto.CourseIds)
            {
                var newTrainingCourse = new TrainingCourse
                {
                    TrainingId = training.Id,
                    CourseId = courseId
                };
                await _unitOfWork.TrainingCourses.AddAsync(newTrainingCourse);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        if (trainingDto.CourseIds != null && trainingDto.StudentIds != null)
        {
            var existingStudentCourses = await _unitOfWork.StudentsCourses.GetByCourseIdsAsync(trainingDto.CourseIds);

            foreach (var sc in existingStudentCourses)
            {
                _unitOfWork.StudentsCourses.Remove(sc);
            }
            await _unitOfWork.SaveChangesAsync();

            foreach (var studentId in trainingDto.StudentIds)
            {
                foreach (var courseId in trainingDto.CourseIds)
                {
                    var newStudentCourse = new StudentCourse
                    {
                        StudentId = studentId,
                        CourseId = courseId,
                        Status = CourseStatusValues.NotStarted
                    };
                    await _unitOfWork.StudentsCourses.AddAsync(newStudentCourse);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task DeleteTrainingAsync(int id)
    {
        var training = await _unitOfWork.Trainings.GetWithCoursesAsync(id);
        if (training == null) return;

        var courseIds = training.TrainingCourses?.Select(tc => tc.CourseId).Distinct().ToList() ?? new List<int>();

        var studentCourses = await _unitOfWork.StudentsCourses.GetByCourseIdsAsync(courseIds);

        foreach (var sc in studentCourses)
        {
            _unitOfWork.StudentsCourses.Remove(sc);
        }

        _unitOfWork.Trainings.Remove(training);
        await _unitOfWork.SaveChangesAsync();
    }
}
