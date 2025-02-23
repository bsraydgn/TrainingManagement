using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainingManagement.Application.Services;
using TrainingManagement.Domain.Abstractions;
using TrainingManagement.Infrastructure.Data;
using TrainingManagement.Infrastructure.Repositories;

namespace TrainingManagement.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<InstructorService>();
        services.AddScoped<TeacherService>();
        services.AddScoped<StudentService>();
        services.AddScoped<CourseService>();
        services.AddScoped<TrainingService>();
        services.AddScoped<StudentCourseService>();
        services.AddScoped<ReportService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<TrainingManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ITrainingRepository, TrainingRepository>();
        services.AddScoped<ITrainingCourseRepository, TrainingCourseRepository>();
        services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();

        return services;
    }
}