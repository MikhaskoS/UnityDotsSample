using AsteroidGame;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;


// Так можно указать, в какой конкретный момент должно произойти обновление
// в какой группе (до или после чего)
//[UpdateInGroup(typeof(InitializationSystemGroup))]
[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TargetToDirectionSystems))]
public class AssignPlayerToTargetSystem : SystemBase
{
    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        AssignPlayer();
    }

    protected override void OnUpdate()
    {
        //AssignPlayer();
    }

    private void AssignPlayer()
    {
        //EntityQuery playerQuery = GetEntityQuery(typeof(PlayerTag));
        // оптимизация
        EntityQuery playerQuery = GetEntityQuery(ComponentType.ReadOnly<PlayerTag>());//
        // общий случай
        //Entity playerEntity = playerQuery.ToEntityArray(Allocator.Temp)[0];
        // Так как объект единичный, то можно так
        Entity playerEntity = playerQuery.GetSingletonEntity();

        // добавим цель к кораблям-преследователям
        Entities
            .WithAll<ChaserTag>()
            .ForEach((ref TargetData targetData) =>
            {
                if (playerEntity != Entity.Null)
                {
                    targetData.targetEntity = playerEntity;
                }
            }).Schedule();
    }
}