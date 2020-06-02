using System;
using Unity.Entities;


namespace Sample0_3
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public struct RotationSpeed_IJobChunk : IComponentData
    {
        public float RadiansPerSecond;
    }
}
