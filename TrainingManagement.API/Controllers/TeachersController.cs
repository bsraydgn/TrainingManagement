using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.DTOs;
using TrainingManagement.Application.Services;

namespace TrainingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController : ControllerBase
{
    private readonly TeacherService _teacherService;

    public TeachersController(TeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        if (teacher == null)
            return NotFound();
        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeacherDto createTeacherDto)
    {
        var teacherDto = await _teacherService.CreateTeacherAsync(createTeacherDto);
        return CreatedAtAction(nameof(GetById), new { id = teacherDto.Id }, teacherDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTeacherDto teacherDto)
    {
        await _teacherService.UpdateTeacherAsync(id, teacherDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteTeacherAsync(id);
        return NoContent();
    }

    [HttpPost("grade-student")]
    public async Task<IActionResult> GradeStudent([FromBody] TeacherGradeStudentDto gradeDto)
    {
        await _teacherService.GradeStudentAsync(gradeDto);
        return NoContent();
    }
}
