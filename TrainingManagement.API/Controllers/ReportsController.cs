using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.Services;

namespace TrainingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportsController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("student-status")]
    public async Task<IActionResult> GetStudentStatus([FromQuery] int courseId)
    {
        var report = await _reportService.GetStudentStatusReportAsync(courseId);
        if (report == null)
            return NotFound();
        return Ok(report);
    }
}
