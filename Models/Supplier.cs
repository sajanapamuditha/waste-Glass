namespace WasteGlassAPI.Models;

public class Supplier
{
    public int    Id                 { get; set; }
    public string Name               { get; set; } = string.Empty;
    public string Address            { get; set; } = string.Empty;
    public double Latitude           { get; set; }
    public double Longitude          { get; set; }
    public double ExpectedClearKg    { get; set; }
    public double ExpectedColouredKg { get; set; }
    public string BarcodeRef         { get; set; } = string.Empty;
    public string Status             { get; set; } = "Pending";

    public ICollection<Collection> Collections { get; set; } = new List<Collection>();
}
