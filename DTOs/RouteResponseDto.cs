namespace WasteGlassAPI.DTOs;

public class RouteResponseDto
{
    public List<SupplierDto> Stops           { get; set; } = new();
    public double            TotalDistanceKm { get; set; }
}
