using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using System;
using Unity.Collections;

namespace Sample3_2
{
    // https://www.youtube.com/watch?v=fkJ-7pqnRGo
    public class MovementSystem : JobComponentSystem
    {
        public event EventHandler onSpeedChange;
        public struct EventComponent : IComponentData
        {
            public double ElapsedTime;
        }
        private EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBufferSystem;

        protected override void OnCreate()
        {
            endSimulationEntityCommandBufferSystem =
                World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            //Debug.Log("MovementSystem working...");
            float time = Time.DeltaTime;

            EntityCommandBuffer entityCommandBuffer =
                endSimulationEntityCommandBufferSystem.CreateCommandBuffer();
            EntityCommandBuffer.Concurrent entityCommandBufferConcurent = entityCommandBuffer.ToConcurrent();
            EntityArchetype eventEntityArchetype = EntityManager.CreateArchetype(typeof(EventComponent));

            double elapsedTime = Time.ElapsedTime;

            JobHandle jobHandle =
                Entities
                .ForEach((int entityInQueryIndex, ref Translation translation, ref MovementData movementData) =>
                {
                    if (movementData.position.x > 5)
                    {
                        movementData.speed.x = -movementData.speed.x;
                        Entity eventEntity =
                        entityCommandBufferConcurent.CreateEntity(entityInQueryIndex, eventEntityArchetype);
                        entityCommandBufferConcurent.SetComponent(entityInQueryIndex, eventEntity,
                            new EventComponent { ElapsedTime = elapsedTime });
                    }
                    if (movementData.position.x < -5)
                    {
                        movementData.speed.x = -movementData.speed.x;
                    }
                    if (movementData.position.y > 3)
                    {
                        movementData.speed.y = -movementData.speed.y;
                    }
                    if (movementData.position.y < -3)
                    {
                        movementData.speed.y = -movementData.speed.y;
                    }

                    movementData.position =
                    new float3(movementData.position.x + movementData.speed.x * time,
                    movementData.position.y + movementData.speed.y * time, 0);

                    translation.Value = movementData.position;

                }).Schedule(inputDeps);

            // работа должна быть завершена, прежде чем мы будем читать события!
            // фактически, мы создаем точку синхронизации
            //jobHandle.Complete();

            endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);
            EntityCommandBuffer captureEventEntityCommandBuffer = 
                endSimulationEntityCommandBufferSystem.CreateCommandBuffer();

            Entities
                .WithoutBurst()
                .ForEach((Entity entity, ref EventComponent eventComponent) =>
                {
                    Debug.Log(eventComponent.ElapsedTime + " ### " + elapsedTime);
                    onSpeedChange?.Invoke(this, EventArgs.Empty);
                    captureEventEntityCommandBuffer.DestroyEntity(entity);
                }).Run();

            return jobHandle;
        }
    }
}
