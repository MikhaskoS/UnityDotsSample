using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


namespace Sample0_3
{
    // Контроль преобразования объекта в сущность. Прикрепляется к объекту в сцене.
    [RequiresEntityConversion]
    [AddComponentMenu("DOTS Samples/IJobChunk/Rotation Speed")]
    [ConverterVersion("joe", 1)]
    public class RotationSpeedAuthoring_IJobChunk : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float DegreesPerSecond = 360.0F;

        // The MonoBehaviour data is converted to ComponentData on the entity.
        // We are specifically transforming from a good editor representation of the data (Represented in degrees)
        // To a good runtime representation (Represented in radians)
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new RotationSpeed_IJobChunk { RadiansPerSecond = math.radians(DegreesPerSecond) };
            dstManager.AddComponentData(entity, data);
        }
    }
}
