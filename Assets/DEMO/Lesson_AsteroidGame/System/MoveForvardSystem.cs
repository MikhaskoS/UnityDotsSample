using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


namespace AsteroidGame
{
    public class MoveForvardSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities
                .WithAny<AsteroidTag, ChaserTag>()
                .WithNone<PlayerTag>()
                .ForEach((ref Translation pos, in MoveData moveData, in Rotation rot) =>
            {
                float3 forfardDirection = math.forward(rot.Value);
                pos.Value += forfardDirection * moveData.speed * deltaTime;
            }).ScheduleParallel();
        }
    }
}
