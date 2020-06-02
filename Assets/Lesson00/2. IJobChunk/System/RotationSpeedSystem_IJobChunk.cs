﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


namespace Sample0_3
{
    public class RotationSpeedSystem_IJobChunk : SystemBase
    {
        EntityQuery m_Group;

        protected override void OnCreate()
        {
            // Cached access to a set of ComponentData based on a specific query
            m_Group = GetEntityQuery(typeof(Rotation), ComponentType.ReadOnly<RotationSpeed_IJobChunk>());
        }


        [BurstCompile]
        struct RotationSpeedJob : IJobChunk
        {
            public float DeltaTime;
            public ArchetypeChunkComponentType<Rotation> RotationType;
            [ReadOnly] public ArchetypeChunkComponentType<RotationSpeed_IJobChunk> RotationSpeedType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var chunkRotations = chunk.GetNativeArray(RotationType);
                var chunkRotationSpeeds = chunk.GetNativeArray(RotationSpeedType);
                for (var i = 0; i < chunk.Count; i++)
                {
                    var rotation = chunkRotations[i];
                    var rotationSpeed = chunkRotationSpeeds[i];

                    // Rotate something about its up vector at the speed given by RotationSpeed_IJobChunk.
                    chunkRotations[i] = new Rotation
                    {
                        Value = math.mul(math.normalize(rotation.Value),
                            quaternion.AxisAngle(math.up(), rotationSpeed.RadiansPerSecond * DeltaTime))
                    };
                }
            }
        }

        protected override void OnUpdate()
        {
            // Explicitly declare:
            // - Read-Write access to Rotation
            // - Read-Only access to RotationSpeed_IJobChunk
            var rotationType = GetArchetypeChunkComponentType<Rotation>();
            var rotationSpeedType = GetArchetypeChunkComponentType<RotationSpeed_IJobChunk>(true);

            var job = new RotationSpeedJob()
            {
                RotationType = rotationType,
                RotationSpeedType = rotationSpeedType,
                DeltaTime = Time.DeltaTime
            };

            Dependency = job.Schedule(m_Group, Dependency);
        }
    }
}