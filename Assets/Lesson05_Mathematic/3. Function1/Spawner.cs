using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;


namespace Sample5_3
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject gameObjectPrefab;

        [SerializeField] int xSize = 10;
        [SerializeField] int ySize = 10;
        [Range(0.1f, 2f)]
        [SerializeField] float spacing = 1f;

        private Entity entityPrefab;
        private World defaultWorld;
        private EntityManager entityManager;


        void Start()
        {
            // setup references to World and EntityManager
            defaultWorld = World.DefaultGameObjectInjectionWorld;
            entityManager = defaultWorld.EntityManager;
            BlobAssetStore blobAsset = new BlobAssetStore();

            // generate Entity Prefab
            if (gameObjectPrefab != null)
            {
                GameObjectConversionSettings settings =
                    GameObjectConversionSettings.FromWorld(defaultWorld, blobAsset);
                entityPrefab =
                    GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

                // spawn x by y grid of Entities
                InstantiateEntityGrid(xSize, ySize, spacing);

                //InstantiateEntity(new float3(2, 2, 2));
            }

            blobAsset.Dispose();
        }

        // create a single Entity from an Entity prefab
        private void InstantiateEntity(float3 position)
        {
            if (entityManager == default)
            {
                Debug.LogWarning("InstantiateEntity WARNING: No EntityManager found!");
                return;
            }

            Entity myEntity = entityManager.Instantiate(entityPrefab);
            entityManager.SetComponentData(myEntity, new Translation
            {
                Value = position
            });
        }

        // create a grid of Entities in an x by y formation
        private void InstantiateEntityGrid(int dimX, int dimY, float spacing = 1f)
        {
            for (int i = 0; i < dimX; i++)
            {
                for (int j = 0; j < dimY; j++)
                {
                    InstantiateEntity(new float3(i * spacing, j * spacing, 0f));
                }
            }
        }

        // create a single Entity using the Conversion Workflow
        //private void ConvertToEntity(float3 position)
        //{
        //    if (entityManager == default)
        //    {
        //        Debug.LogWarning("ConvertToEntity WARNING: No EntityManager found!");
        //        return;
        //    }

        //    if (gameObjectPrefab == null)
        //    {
        //        Debug.LogWarning("ConvertToEntity WARNING: Missing GameObject Prefab");
        //        return;
        //    }

        //    GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);
        //    entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjectPrefab, settings);

        //    Entity myEntity = entityManager.Instantiate(entityPrefab);
        //    entityManager.SetComponentData(myEntity, new Translation
        //    {
        //        Value = position
        //    });
        //}
    }
}
