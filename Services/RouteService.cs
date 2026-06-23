using WasteGlassAPI.Data;
using WasteGlassAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WasteGlassAPI.Services;

public class RouteService
{
    private readonly AppDbContext _db;

    public RouteService(AppDbContext db) => _db = db;

    public async Task<RouteResponseDto> GetOptimalRouteAsync(double startLat, double startLng)
    {
        var suppliers = await _db.Suppliers.ToListAsync();

        // Build node map
        var nodes = suppliers.ToDictionary(
            s => s.Id,
            s => (lat: s.Latitude, lng: s.Longitude));

        // Run optimisation
        var orderedIds = DijkstraService.OptimalOrder(startLat, startLng, nodes);

        // Build response DTOs with stop order and distances
        var stops      = new List<SupplierDto>();
        double prevLat = startLat;
        double prevLng = startLng;
        double total   = 0;

        for (int i = 0; i < orderedIds.Count; i++)
        {
            var id = orderedIds[i];
            var s  = suppliers.First(x => x.Id == id);
            var dist = HaversineService.Distance(prevLat, prevLng, s.Latitude, s.Longitude);
            total += dist;

            // Set status: first unvisited = "Next", rest = "Pending"
            string status = s.Status == "Collected"
                ? "Collected"
                : (i == 0 && s.Status != "Collected" ? "Next" : s.Status);

            stops.Add(new SupplierDto
            {
                Id                 = s.Id,
                Name               = s.Name,
                Address            = s.Address,
                Latitude           = s.Latitude,
                Longitude          = s.Longitude,
                ExpectedClearKg    = s.ExpectedClearKg,
                ExpectedColouredKg = s.ExpectedColouredKg,
                BarcodeRef         = s.BarcodeRef,
                Status             = status,
                StopOrder          = i + 1,
                DistanceFromPrev   = Math.Round(dist, 2),
            });

            prevLat = s.Latitude;
            prevLng = s.Longitude;
        }

        // Ensure exactly one "Next" (the first non-Collected stop)
        bool foundNext = false;
        foreach (var stop in stops)
        {
            if (stop.Status == "Collected") continue;
            if (!foundNext) { stop.Status = "Next"; foundNext = true; }
            else            { stop.Status = "Pending"; }
        }

        return new RouteResponseDto
        {
            Stops           = stops,
            TotalDistanceKm = Math.Round(total, 2),
        };
    }
}
