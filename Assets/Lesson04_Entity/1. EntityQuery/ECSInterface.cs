using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample4_1
{
    public class ECSInterface : MonoBehaviour
    {
        EntityManager entityManager;
        World world;
        public GameObject tankPrefabEntity;
        public GameObject tankPrefab;

        // Start is called before the first frame update
        void Start()
        {
            world = World.DefaultGameObjectInjectionWorld;
            entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;

            Debug.Log("All Entities: " + 
                world.GetExistingSystem<MoveSystem>().EntityManager.GetAllEntities().Length);

            CountShip();
            CountTank();
        }

        private void Update()
        {
            // так спавнятся префабы с ConvertToEntity
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
                Instantiate(tankPrefabEntity, pos, Quaternion.identity);

                CountShip();
                CountTank();
            }

            // А так обычные префабы (такие танки не будут отображаться в счетчике, 
            // т.к. у них нет TankData)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                BlobAssetStore blobAsset = new BlobAssetStore();
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10),
                    0, UnityEngine.Random.Range(-10, 10));

                var settings = GameObjectConversionSettings.FromWorld(world, blobAsset);
                var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(tankPrefab, settings);
                var instance = entityManager.Instantiate(prefab);
                var position = transform.TransformPoint(new float3(pos.x, 0, pos.z));

                entityManager.SetComponentData(instance, new Translation { Value = position });
                entityManager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });

                blobAsset.Dispose();

                CountShip();
                CountTank();
            }
        }

        public void CountShip()
        {
            EntityQuery entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepData>());
            Debug.Log("Sheep Count: " + entityQuery.CalculateEntityCount());
        }

        public void CountTank()
        {
            EntityQuery entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<TankData>());
            Debug.Log("Tank Count: " + entityQuery.CalculateEntityCount());
        }
    }
}
