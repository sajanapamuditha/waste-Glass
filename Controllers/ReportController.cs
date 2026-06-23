using Microsoft.AspNetCore.Mvc;
using WasteGlassAPI.Services;

namespace WasteGlassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    /// GET /api/report  – returns full trip summary for Screen 3
    [HttpGet]
    public async Task<IActionResult> GetReport()
    {
        var report = await _reportService.GetTripReportAsync();
        return Ok(report);
    }
}
