using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.Services;

namespace TrainingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorsController : ControllerBase
{
    private readonly InstructorService _instructorService;

    public InstructorsController(InstructorService instructorService)
    {
        _instructorService = instructorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var instructors = await _instructorService.GetAllInstructorsAsync();
        return Ok(instructors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var instructor = await _instructorService.GetInstructorByIdAsync(id);
        if (instructor == null)
            return NotFound();
        return Ok(instructor);
    }
}
