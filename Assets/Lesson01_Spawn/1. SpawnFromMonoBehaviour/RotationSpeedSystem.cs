using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


namespace Sample1_0
{
    // Эта система обновляет все объекты в сцене как с помощью компонента Rotation Speed_For Each, 
    // так и с помощью компонента Rotation component.
    public class RotationSpeedSystem : SystemBase
    {
        // OnUpdate запускается в главном потоке
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            // Schedule job to rotate around up vector
            Entities
                .WithName("RotationSpeedSystem_ForEach")
                .ForEach((ref Rotation rotation, in RotationSpeed rotationSpeed) =>
                {
                    rotation.Value = math.mul(
                        math.normalize(rotation.Value),
                        quaternion.AxisAngle(math.up(), rotationSpeed.RadiansPerSecond * deltaTime));
                })
                .ScheduleParallel();
        }
    }
}
