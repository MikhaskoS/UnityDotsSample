using Unity.Entities;


namespace Sample1_3
{
    [GenerateAuthoringComponent]
    public struct PrefabEntityComponent : IComponentData
    {
        public Entity prefabEntity;
    }
}
