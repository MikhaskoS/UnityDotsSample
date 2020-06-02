using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Sample1_4
{
    [DisableAutoCreation]
    public class EntitySpawnerSystem : ComponentSystem
    {
        private float spawnTimer;
        private Random random;

        protected override void OnCreate()
        {
            Debug.Log("Work EntitySpawnerSystem...");
            random = new Random(56);
        }

        protected override void OnUpdate()
        {
            spawnTimer -= Time.DeltaTime;
            if (spawnTimer <= 0.0f)
            {
                spawnTimer = 0.5f;

                // Способ 2
                Entity spawnedEntity = EntityManager.Instantiate(PrefabEntities_V2.prefabEntity);
                EntityManager.SetComponentData(spawnedEntity,
                    new Translation
                    {
                        Value = new float3(random.NextFloat(-5, 5), random.NextFloat(-5, 5), 0)
                    });
            }
        }
    }
}
