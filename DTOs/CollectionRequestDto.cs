namespace WasteGlassAPI.DTOs;

public class CollectionRequestDto
{
    public int      SupplierId { get; set; }
    public double   ClearKg    { get; set; }
    public double   ColouredKg { get; set; }
    public string   Condition  { get; set; } = string.Empty;
    public DateTime Timestamp  { get; set; }
}
