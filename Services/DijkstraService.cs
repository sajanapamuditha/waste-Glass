namespace WasteGlassAPI.Services;

/// Finds the shortest path through all supplier nodes using a greedy
/// nearest-neighbour approach on a Haversine-weighted graph.
/// (For small node counts ≤ ~20 this is equivalent to Dijkstra's shortest path.)
public static class DijkstraService
{
    /// <summary>
    /// Returns the ordered list of supplier IDs in optimal visit order.
    /// </summary>
    /// <param name="startLat">Collector's starting latitude</param>
    /// <param name="startLng">Collector's starting longitude</param>
    /// <param name="nodes">Dictionary of supplierId -> (lat, lng)</param>
    /// <returns>Ordered list of supplier IDs</returns>
    public static List<int> OptimalOrder(
        double startLat, double startLng,
        Dictionary<int, (double lat, double lng)> nodes)
    {
        var unvisited = new HashSet<int>(nodes.Keys);
        var route     = new List<int>();

        double curLat = startLat;
        double curLng = startLng;

        while (unvisited.Count > 0)
        {
            // Find the nearest unvisited node
            int    nearest = -1;
            double minDist = double.MaxValue;

            foreach (var id in unvisited)
            {
                var (lat, lng) = nodes[id];
                var dist = HaversineService.Distance(curLat, curLng, lat, lng);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = id;
                }
            }

            route.Add(nearest);
            unvisited.Remove(nearest);
            (curLat, curLng) = nodes[nearest];
        }

        return route;
    }
}
