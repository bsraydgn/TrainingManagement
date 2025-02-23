using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.DTOs;
using TrainingManagement.Application.Services;

namespace TrainingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly CourseService _courseService;

    public CoursesController(CourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound();
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseDto createCourseDto)
    {
        var courseDto = await _courseService.CreateCourseAsync(createCourseDto);
        return CreatedAtAction(nameof(GetById), new { id = courseDto.Id }, courseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateCourseDto courseDto)
    {
        await _courseService.UpdateCourseAsync(id, courseDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.DeleteCourseAsync(id);
        return NoContent();
    }
}