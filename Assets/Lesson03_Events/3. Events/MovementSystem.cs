using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using System;
using Unity.Collections;

namespace Sample3_3
{
    // https://www.youtube.com/watch?v=fkJ-7pqnRGo
    public class MovementSystem : JobComponentSystem
    {
        private DOTSEvents_NextFrame<EventComponent> dotsEvent;  // <------------

        public event EventHandler onSpeedChange;
        public struct EventComponent : IComponentData
        {
            public double ElapsedTime;
        }

        protected override void OnCreate()
        {
            dotsEvent = new DOTSEvents_NextFrame<EventComponent>(World);
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            //Debug.Log("MovementSystem working...");
            float time = Time.DeltaTime;

            double elapsedTime = Time.ElapsedTime;

            DOTSEvents_NextFrame<EventComponent>.EventTrigger eventTrigger = dotsEvent.GetEventTrigger();

            JobHandle jobHandle =
                Entities
                .ForEach((int entityInQueryIndex, ref Translation translation, ref MovementData movementData) =>
                {
                    if (movementData.position.x > 5)
                    {
                        movementData.speed.x = -movementData.speed.x;
                        eventTrigger.TriggerEvent(entityInQueryIndex,
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

            dotsEvent.CaptureEvents(jobHandle, (EventComponent basicEvent) => 
            {
                Debug.Log("Event!");
                onSpeedChange?.Invoke(this, EventArgs.Empty);
            });

            return jobHandle;
        }
    }
}
