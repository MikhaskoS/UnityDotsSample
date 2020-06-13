using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;


namespace Sample4_3
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Mesh unitMesh = null;
        [SerializeField] private Material unitMaterial = null;
        // Start is called before the first frame update
        void Start()
        {
            MakeEntity(); 
        }

        // Ручное построение Entity
        private void MakeEntity()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityArchetype archetype = entityManager.CreateArchetype(
                typeof(Translation),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(RenderBounds),
                typeof(LocalToWorld),
                typeof(NonUniformScale)
                );

            Entity myEntity = entityManager.CreateEntity(archetype);
            entityManager.AddComponentData(myEntity, new Translation { Value = new float3(0, 0, 0) });
            entityManager.AddSharedComponentData(myEntity, new RenderMesh
            {
                mesh = unitMesh,
                material = unitMaterial
            }) ;
            entityManager.AddComponentData(myEntity, new NonUniformScale { Value = new float3(5, 5, 5) });
        }
    }
}
