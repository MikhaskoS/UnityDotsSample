using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


namespace Sample5_3
{
    //[DisableAutoCreation]
    public class WaveSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation trans, ref MoveSpeedData  moveSpeed, ref WaveData waveData) =>
            {
                float zPosition = waveData.amplitude * math.sin((float)Time.ElapsedTime * moveSpeed.Value
                    + trans.Value.x * waveData.xOffset + trans.Value.y * waveData.yOffset);
                //float zPosition = waveData.amplitude * math.sin((float)Time.ElapsedTime * moveSpeed.Value
                //  );
                trans.Value = new float3(trans.Value.x, trans.Value.y, zPosition);
            });
        }
    }
}
