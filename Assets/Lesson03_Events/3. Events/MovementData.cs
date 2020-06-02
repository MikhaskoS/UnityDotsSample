using Unity.Entities;
using Unity.Mathematics;


namespace Sample3_3
{
    [GenerateAuthoringComponent]
    public struct MovementData : IComponentData
    {
        public float3 speed;
        public float3 position;
    }
}
