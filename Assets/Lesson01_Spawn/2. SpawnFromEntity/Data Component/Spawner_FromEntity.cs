using Unity.Entities;


namespace Sample1_2
{
    // ReSharper disable once InconsistentNaming
    public struct Spawner_FromEntity : IComponentData
    {
        public int CountX;
        public int CountY;
        public Entity Prefab;
    }
}
