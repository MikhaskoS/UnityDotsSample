using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


namespace Sample0_4
{
    public class PlayerMovementSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Translation pos, in MoveData moveData) =>
            {
                float3 normalizedDir = math.normalizesafe(moveData.direction);
                pos.Value += normalizedDir * moveData.speed * deltaTime;

            }).Run();
        }
    }
}
