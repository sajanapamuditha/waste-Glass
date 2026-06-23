using WasteGlassAPI.Models;

namespace WasteGlassAPI.Data;

/// Seeds 6 test suppliers around Moratuwa / Colombo area, Sri Lanka.
/// Each BarcodeRef matches a Code-128 barcode you generate from barcode.tec-it.com
/// encoding the supplier ID (e.g. "1", "2" ... "6").
public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Suppliers.Any()) return; // already seeded

        var suppliers = new List<Supplier>
        {
            new() { Id = 1, Name = "Moratuwa Glass Depot",       Address = "Marine Drive, Moratuwa",        Latitude = 6.7734, Longitude = 79.8826, ExpectedClearKg = 80,  ExpectedColouredKg = 40,  BarcodeRef = "1", Status = "Pending" },
            new() { Id = 2, Name = "Dehiwala Recyclers",         Address = "Galle Rd, Dehiwala",            Latitude = 6.8518, Longitude = 79.8683, ExpectedClearKg = 60,  ExpectedColouredKg = 30,  BarcodeRef = "2", Status = "Pending" },
            new() { Id = 3, Name = "Nugegoda Glass Store",       Address = "High Level Rd, Nugegoda",       Latitude = 6.8726, Longitude = 79.8891, ExpectedClearKg = 100, ExpectedColouredKg = 50,  BarcodeRef = "3", Status = "Pending" },
            new() { Id = 4, Name = "Kollupitiya Waste Centre",   Address = "Galle Face, Colombo 3",         Latitude = 6.9070, Longitude = 79.8476, ExpectedClearKg = 120, ExpectedColouredKg = 60,  BarcodeRef = "4", Status = "Pending" },
            new() { Id = 5, Name = "Pettah Glass Merchants",     Address = "Manning Market, Pettah",        Latitude = 6.9354, Longitude = 79.8489, ExpectedClearKg = 90,  ExpectedColouredKg = 45,  BarcodeRef = "5", Status = "Pending" },
            new() { Id = 6, Name = "Kelaniya Bottle Suppliers",  Address = "Kelaniya Junction, Kelaniya",   Latitude = 7.0017, Longitude = 79.9216, ExpectedClearKg = 70,  ExpectedColouredKg = 35,  BarcodeRef = "6", Status = "Pending" },
        };

        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();
    }
}
