using Unity.Entities;
using Unity.Mathematics;


namespace Lesson4
{
    public struct WayPoint
    {
        public float3 position;
    }

    public struct WayPointsBlobAsset
    {
        public BlobArray<WayPoint> waypointArray;
    }
}
