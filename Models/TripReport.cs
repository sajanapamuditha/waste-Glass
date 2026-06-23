namespace WasteGlassAPI.Models;

public class TripReportModel
{
    public List<ReportSupplierItem> Suppliers           { get; set; } = new();
    public double                   TotalClearKg        { get; set; }
    public double                   TotalColouredKg     { get; set; }
    public double                   TotalKg             { get; set; }
    public double                   RouteDistanceKm     { get; set; }
    public int                      TripDurationMinutes { get; set; }
}

public class ReportSupplierItem
{
    public int    SupplierId          { get; set; }
    public string SupplierName        { get; set; } = string.Empty;
    public double ExpectedClearKg     { get; set; }
    public double ExpectedColouredKg  { get; set; }
    public double CollectedClearKg    { get; set; }
    public double CollectedColouredKg { get; set; }
    public string Condition           { get; set; } = string.Empty;
    public bool   HasShortfall        { get; set; }
}
