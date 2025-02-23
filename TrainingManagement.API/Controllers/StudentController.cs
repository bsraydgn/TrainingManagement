using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.DTOs;
using TrainingManagement.Application.Services;

namespace TrainingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentCourseService _studentCourseService;
    private readonly StudentService _studentService;

    public StudentController(StudentCourseService studentCourseService, StudentService studentService)
    {
        _studentCourseService = studentCourseService;
        _studentService = studentService;
    }

    [HttpPost("start-course")]
    public async Task<IActionResult> StartCourse([FromBody] StudentStartCourseDto startCourseDto)
    {
        await _studentCourseService.StartCourseAsync(startCourseDto);
        return NoContent();
    }

    [HttpPost("complete-course")]
    public async Task<IActionResult> CompleteCourse([FromBody] StudentCompleteCourseDto completeCourseDto)
    {
        await _studentCourseService.CompleteCourseAsync(completeCourseDto);
        return NoContent();
    }
}