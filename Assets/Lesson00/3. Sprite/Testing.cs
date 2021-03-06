﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;


// https://unitycodemonkey.com/video.php?v=ILfUuBLfzGI
// В этом примере сущность создается напрямую, однако обычно это делается
// с помощью преобразования Convert To Entity (встроенный скрипт) либо посредством SubScene
namespace Sample0_3
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private Mesh mesh = null;
        [SerializeField] private Material material = null;

        void Start()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // Создать сущность напрямую
            //Entity entity =  entityManager.CreateEntity(typeof(LevelComponent));

            // Архетип для сущности
            EntityArchetype entityArchetype = entityManager.CreateArchetype(
                typeof(LevelComponent),
                typeof(Translation),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(RenderBounds),
                typeof(MoveSpeedComponent)
                );
            //Entity entity = entityManager.CreateEntity(entityArchetype);

            NativeArray<Entity> entityArray = new NativeArray<Entity>(100, Allocator.Temp);
            entityManager.CreateEntity(entityArchetype, entityArray);

            for (int i = 0; i < entityArray.Length; i++)
            {
                Entity entity = entityArray[i];

                entityManager.SetComponentData(entity,
                    new LevelComponent
                    {
                        level = UnityEngine.Random.Range(10, 20)
                    });

                entityManager.SetComponentData(entity,
                    new Translation
                    {
                        Value = new float3(UnityEngine.Random.Range(-5, 5f), UnityEngine.Random.Range(-5, 5f), 0)
                    });

                entityManager.SetComponentData(entity,
                    new MoveSpeedComponent
                    {
                        moveSpeed = UnityEngine.Random.Range(1f, 2f)
                    });

                entityManager.SetSharedComponentData(entity,
                   new RenderMesh
                   {
                       mesh = mesh,
                       material = material
                   });
            }

            entityArray.Dispose();
        }

    }
}
