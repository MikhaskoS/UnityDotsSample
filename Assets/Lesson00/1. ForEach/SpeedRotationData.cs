
using Unity.Entities;

namespace Sample0_1
{
    [GenerateAuthoringComponent]
    public struct SpeedRotationData : IComponentData
    {
        public float RadiansPerSecond;
    }
}
