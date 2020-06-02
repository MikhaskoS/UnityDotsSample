using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


namespace Sample1_2
{
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS Samples/SpawnFromEntity/Spawner")]
    [ConverterVersion("joe", 1)]
    public class SpawnerAuthoring_FromEntity : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public GameObject Prefab;
        public int CountX;
        public int CountY;

        // Ссылки на префабы должны быть объявлены так, чтобы система преобразования знала о них заранее
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(Prefab);
        }

        // Lets you convert the editor data representation to the entity optimal runtime representation
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var spawnerData = new Spawner_FromEntity
            {
                // The referenced prefab will be converted due to DeclareReferencedPrefabs.
                // So here we simply map the game object to an entity reference to that prefab.
                Prefab = conversionSystem.GetPrimaryEntity(Prefab),
                CountX = CountX,
                CountY = CountY
            };
            dstManager.AddComponentData(entity, spawnerData);
        }
    }
}
