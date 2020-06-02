using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;


namespace Sample1_5
{
    public class PrefabEntities_V3 : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public static Entity prefabEntity;
        public GameObject prefabGameObject;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            Entity prefEntity = conversionSystem.GetPrimaryEntity(prefabGameObject);
            PrefabEntities_V3.prefabEntity = prefEntity;
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(prefabGameObject);
        }
    }

    //public class EntityPrefabConversionSystem : GameObjectConversionSystem
    //{
    //    // вызывается только один раз!
    //    protected override void OnUpdate()
    //    {
    //        Debug.Log("Conversion...");
    //        // что-то делаем
    //    }
    //}
}
