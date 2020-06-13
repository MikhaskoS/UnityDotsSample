using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace AsteroidGame
{
    public class SpinnerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities
                .WithAll<SpinnerTag>()   // для всех объектов с тегом SpinnerTag
                .WithNone<PlayerTag>()   // не для объектов с тегом PlayerTag
                .ForEach((ref Rotation rot, in MoveData moveData) =>   // здесь тоже, в своем роде фильтр. Мы можем вписать in SpinnerTag
            {
                quaternion normalizeRot = math.normalize(rot.Value);
                quaternion angleToRotate = quaternion.AxisAngle(math.up(), moveData.turnSpeed * deltaTime);

                rot.Value = math.mul(normalizeRot, angleToRotate);
            }).ScheduleParallel();
        }
    }
}
