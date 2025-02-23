using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.DTOs;
using TrainingManagement.Application.Services;

namespace TrainingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingsController : ControllerBase
{
    private readonly TrainingService _trainingService;

    public TrainingsController(TrainingService trainingService)
    {
        _trainingService = trainingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trainings = await _trainingService.GetAllTrainingsAsync();
        return Ok(trainings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var training = await _trainingService.GetTrainingByIdAsync(id);
        if (training == null)
            return NotFound();
        return Ok(training);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTrainingDto createTrainingDto)
    {
        var trainingDto = await _trainingService.CreateTrainingAsync(createTrainingDto);
        return CreatedAtAction(nameof(GetById), new { id = trainingDto.Id }, trainingDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTrainingDto trainingDto)
    {
        await _trainingService.UpdateTrainingAsync(id, trainingDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _trainingService.DeleteTrainingAsync(id);
        return NoContent();
    }
}
