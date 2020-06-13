using Unity.Entities;
using Unity.Mathematics;


namespace Sample0_4
{
    [GenerateAuthoringComponent]
    public struct MoveData : IComponentData
    {
        public float3 direction;
        public float speed;
        public float turnSpeed;
    }
}
