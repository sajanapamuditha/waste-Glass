namespace WasteGlassAPI.Services;

/// Calculates great-circle distance between two GPS coordinates.
public static class HaversineService
{
    private const double R = 6371.0; // Earth radius in km

    public static double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        var dLat = ToRad(lat2 - lat1);
        var dLon = ToRad(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
              + Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2))
              * Math.Sin(dLon / 2)   * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c; // distance in km
    }

    private static double ToRad(double deg) => deg * Math.PI / 180.0;
}
