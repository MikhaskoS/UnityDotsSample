using UnityEngine;
using Unity.Entities;


namespace Sample1_4
{
    public class PrefabEntities_V2 : MonoBehaviour, IConvertGameObjectToEntity
    {
        public static Entity prefabEntity;
        public GameObject prefabGameObject;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            using (BlobAssetStore blobAssetStore = new BlobAssetStore())
            {
                Entity prefEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefabGameObject,
                    GameObjectConversionSettings.FromWorld(dstManager.World, blobAssetStore));
                PrefabEntities_V2.prefabEntity = prefEntity;
            }
        }
    }
}
