using Unity.Entities;


namespace Sample5_3
{
    [GenerateAuthoringComponent]
    public struct MoveSpeedData : IComponentData
    {
        public float Value;
    }
}
