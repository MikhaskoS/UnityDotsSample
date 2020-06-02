using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Sample1_3
{
    [DisableAutoCreation]
    public class EntitySpawnerSystem : ComponentSystem
    {
        private float spawnTimer;
        private Random random;

        protected override void OnCreate()
        {
            Debug.Log("Spawner System create...");
            random = new Random(56);
        }

        protected override void OnUpdate()
        {
            spawnTimer -= Time.DeltaTime;
            if (spawnTimer <= 0.0f)
            {
                Debug.Log("Spawner System update...");
                spawnTimer = 0.5f;

                // Spawn (способ 1)
                Entities.ForEach((ref PrefabEntityComponent prefabEntityComponent) =>
                {
                    Entity spawnedEntity =
                    EntityManager.Instantiate(prefabEntityComponent.prefabEntity);

                    EntityManager.SetComponentData(spawnedEntity,
                        new Translation
                        {
                            Value = new float3(random.NextFloat(-5, 5), random.NextFloat(-5, 5), 0)
                        });
                });
                // Способ 1б - через синглтон
                //-----------------------------------------------------------------------------
                //PrefabEntityComponent prefabEntityComponent = GetSingleton<PrefabEntityComponent>();
                //Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabEntity);
                //EntityManager.SetComponentData(spawnedEntity,
                //    new Translation
                //    {
                //        Value = new float3(random.NextFloat(-5, 5), random.NextFloat(-5, 5), 0)
                //    });
                //-----------------------------------------------------------------------------
            }
        }
    }
}
