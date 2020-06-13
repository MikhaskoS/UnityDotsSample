using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;


namespace Sample4_1
{
    [DisableAutoCreation]
    public class MoveSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {

            var jobHandle = Entities
                   .WithName("MoveSystem")
                   .ForEach((ref Translation position, ref Rotation rotation) =>
                   {

                   })
                   .Schedule(inputDeps);

            return jobHandle;
        }
    }
}
