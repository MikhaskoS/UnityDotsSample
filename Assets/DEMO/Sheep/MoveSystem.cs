using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;

[DisableAutoCreation]
public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
                //position.Value += 0.1f * math.forward(rotation.Value);
                //if (position.Value.z > 50)
                //    position.Value.z = -50;
                position.Value.y -= 0.1f;
                if (position.Value.y < 0)
                    position.Value.y = 100;
            })
                        .Schedule(inputDeps);

        return jobHandle;
    }
}
