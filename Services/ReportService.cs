using WasteGlassAPI.Data;
using WasteGlassAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WasteGlassAPI.Services;

public class ReportService
{
    private readonly AppDbContext _db;

    public ReportService(AppDbContext db) => _db = db;

    public async Task<TripReportDto> GetTripReportAsync()
    {
        var suppliers   = await _db.Suppliers.ToListAsync();
        var collections = await _db.Collections
            .OrderByDescending(c => c.Timestamp)
            .ToListAsync();

        var reportItems = new List<ReportSupplierDto>();
        double totalClear = 0, totalColoured = 0;

        foreach (var s in suppliers)
        {
            // Latest collection record for this supplier
            var col = collections.FirstOrDefault(c => c.SupplierId == s.Id);
            double collClear     = col?.ClearKg    ?? 0;
            double collColoured  = col?.ColouredKg ?? 0;
            string condition     = col?.Condition  ?? "N/A";

            bool shortfall = collClear + collColoured < s.ExpectedClearKg + s.ExpectedColouredKg;

            reportItems.Add(new ReportSupplierDto
            {
                SupplierId          = s.Id,
                SupplierName        = s.Name,
                ExpectedClearKg     = s.ExpectedClearKg,
                ExpectedColouredKg  = s.ExpectedColouredKg,
                CollectedClearKg    = collClear,
                CollectedColouredKg = collColoured,
                Condition           = condition,
                HasShortfall        = shortfall,
            });

            totalClear    += collClear;
            totalColoured += collColoured;
        }

        // Route total distance (recalculate)
        double routeDist  = 0;
        double prevLat    = 6.8218, prevLng = 79.8798;
        foreach (var s in suppliers)
        {
            routeDist += HaversineService.Distance(prevLat, prevLng, s.Latitude, s.Longitude);
            prevLat = s.Latitude;
            prevLng = s.Longitude;
        }

        // Trip duration: from first collection to latest
        int durationMins = 0;
        if (collections.Count > 0)
        {
            var first = collections.Min(c => c.Timestamp);
            var last  = collections.Max(c => c.Timestamp);
            durationMins = (int)(last - first).TotalMinutes;
        }

        return new TripReportDto
        {
            Suppliers           = reportItems,
            TotalClearKg        = Math.Round(totalClear,    2),
            TotalColouredKg     = Math.Round(totalColoured, 2),
            TotalKg             = Math.Round(totalClear + totalColoured, 2),
            RouteDistanceKm     = Math.Round(routeDist, 2),
            TripDurationMinutes = durationMins,
        };
    }
}
