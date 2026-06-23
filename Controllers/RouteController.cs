using Microsoft.AspNetCore.Mvc;
using WasteGlassAPI.Services;

namespace WasteGlassAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RouteController : ControllerBase
{
    private readonly RouteService _routeService;

    public RouteController(RouteService routeService)
    {
        _routeService = routeService;
    }

    /// GET /api/route?lat=6.82&lng=79.87
    /// Returns today's supplier list in optimised stop order using Dijkstra.
    [HttpGet]
    public async Task<IActionResult> GetRoute(
        [FromQuery] double lat = 6.8218,
        [FromQuery] double lng = 79.8798)
    {
        var result = await _routeService.GetOptimalRouteAsync(lat, lng);
        return Ok(result);
    }
}
