using Unity.Entities;

namespace Sample5_3
{
    [GenerateAuthoringComponent]
    public struct WaveData : IComponentData
    {
        public float amplitude;
        public float xOffset;
        public float yOffset;
    }
}
