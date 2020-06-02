using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using System;
using Unity.Collections;

namespace Sample3_1
{
    // https://www.youtube.com/watch?v=fkJ-7pqnRGo
    public class MovementSystem : JobComponentSystem
    {
        public event EventHandler OnChangeSpeed;
        public struct SpeedEvent { }
        private NativeQueue<SpeedEvent> eventsQueue;

        protected override void OnCreate()
        {
            eventsQueue = new NativeQueue<SpeedEvent>(Allocator.Persistent);
        }

        protected override void OnDestroy()
        {
            eventsQueue.Dispose();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            //Debug.Log("MovementSystem working...");
            float time = Time.DeltaTime;

            NativeQueue<SpeedEvent>.ParallelWriter eventQueueParallel =
                eventsQueue.AsParallelWriter();

            JobHandle jobHandle =
                Entities
                .ForEach((ref Translation translation, ref MovementData movementData) =>
                {
                    if (movementData.position.x > 5)
                    { 
                        movementData.speed.x = -movementData.speed.x;
                        eventQueueParallel.Enqueue(new SpeedEvent { });
                    }
                    if (movementData.position.x < -5)
                    {
                        movementData.speed.x = -movementData.speed.x;
                        eventQueueParallel.Enqueue(new SpeedEvent { });
                    }
                    if (movementData.position.y > 3)
                    {
                        movementData.speed.y = -movementData.speed.y;
                        eventQueueParallel.Enqueue(new SpeedEvent { });
                    }
                    if (movementData.position.y < -3)
                    {
                        movementData.speed.y = -movementData.speed.y;
                        eventQueueParallel.Enqueue(new SpeedEvent { });
                    }

                    movementData.position =
                    new float3(movementData.position.x + movementData.speed.x * time,
                    movementData.position.y + movementData.speed.y * time, 0);

                    translation.Value = movementData.position;

                }).Schedule(inputDeps);

            // работа должна быть завершена, прежде чем мы будем читать события!
            // фактически, мы создаем точку синхронизации
            jobHandle.Complete();

            while (eventsQueue.TryDequeue(out SpeedEvent speedEvent))
            {
                Debug.Log("Event!");
                OnChangeSpeed?.Invoke(this, EventArgs.Empty);
            }

            return jobHandle;
        }
    }
}
