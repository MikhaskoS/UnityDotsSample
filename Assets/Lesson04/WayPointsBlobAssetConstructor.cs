using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Debug = UnityEngine.Debug;

namespace Lesson4
{
    public class WayPointsBlobAssetConstructor : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            BlobAssetReference<WayPointsBlobAsset> waypointBlobAssetReference;

            using (BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp))
            {
                ref WayPointsBlobAsset wayPointsBlobAsset = ref blobBuilder.ConstructRoot<WayPointsBlobAsset>();
                // выделяем память
                BlobBuilderArray<WayPoint> waypointArray = blobBuilder.Allocate(ref wayPointsBlobAsset.waypointArray, 3);
                waypointArray[0] = new WayPoint { position = new float3(0, 0, 0) };
                waypointArray[1] = new WayPoint { position = new float3(5, 0, 0) };
                waypointArray[2] = new WayPoint { position = new float3(2.5f, 2.5f, 0) };

                // ссылка на BlobAsset
                waypointBlobAssetReference =
                blobBuilder.CreateBlobAssetReference<WayPointsBlobAsset>(Allocator.Persistent);

                Debug.Log(waypointBlobAssetReference.Value.waypointArray.Length);
                Debug.Log(waypointBlobAssetReference.Value.waypointArray[0].position);

            }
        }
    }
}
