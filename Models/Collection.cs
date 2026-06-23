namespace WasteGlassAPI.Models;

public class Collection
{
    public int      Id         { get; set; }
    public int      SupplierId { get; set; }
    public double   ClearKg    { get; set; }
    public double   ColouredKg { get; set; }
    public string   Condition  { get; set; } = string.Empty;
    public DateTime Timestamp  { get; set; }
    public Supplier? Supplier  { get; set; }
}
