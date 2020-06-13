using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;


namespace AsteroidGame
{
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class PikupOnTriggerSystem : JobComponentSystem
    {
        private BuildPhysicsWorld buildPhysicsWorld;
        private StepPhysicsWorld stepPhysicsWorld;

        public EndSimulationEntityCommandBufferSystem commandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        // [BurstCompile] - отключить, если используются методы UnityEngine
        struct PikupOnTriggerSystemJob : ITriggerEventsJob
        {
            [ReadOnly] public ComponentDataFromEntity<PickupTag> allPickups;
            [ReadOnly] public ComponentDataFromEntity<PlayerTag> allPlayers;

            public EntityCommandBuffer entityCommandBuffer;

            public void Execute(TriggerEvent triggerEvent)
            {
                Entity entityA = triggerEvent.Entities.EntityA;
                Entity entityB = triggerEvent.Entities.EntityB;

                if (allPickups.Exists(entityA) && allPickups.Exists(entityB))
                {
                    return;
                }
                if (allPickups.Exists(entityA) && allPlayers.Exists(entityB))
                {
                    entityCommandBuffer.DestroyEntity(entityA);
                    //UnityEngine.Debug.Log("Pickup A: " + entityA + " collided with Player B" + entityB);
                }
                else if (allPlayers.Exists(entityA) && allPickups.Exists(entityB))
                {
                    entityCommandBuffer.DestroyEntity(entityB);
                    //UnityEngine.Debug.Log("Player A: " + entityA + " collided with Pickup B" + entityB);
                }
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new PikupOnTriggerSystemJob();
            job.allPickups = GetComponentDataFromEntity<PickupTag>(true);
            job.allPlayers = GetComponentDataFromEntity<PlayerTag>(true);
            job.entityCommandBuffer = commandBufferSystem.CreateCommandBuffer();


            JobHandle jobHandle =
                job.Schedule(
                    stepPhysicsWorld.Simulation,
                    ref buildPhysicsWorld.PhysicsWorld,
                    inputDependencies);

            //jobHandle.Complete();
            commandBufferSystem.AddJobHandleForProducer(jobHandle);
            return jobHandle;
        }
    }
}