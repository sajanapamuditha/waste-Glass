namespace WasteGlassAPI.Models;

public class RouteResult
{
    public List<RouteStop> Stops           { get; set; } = new();
    public double          TotalDistanceKm { get; set; }
}

public class RouteStop
{
    public int    SupplierId       { get; set; }
    public int    StopOrder        { get; set; }
    public double DistanceFromPrev { get; set; }
}
