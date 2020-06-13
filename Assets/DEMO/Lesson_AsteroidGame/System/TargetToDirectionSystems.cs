using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;


namespace AsteroidGame
{
    public class TargetToDirectionSystems : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                      .WithNone<PlayerTag>()
                      .WithAll<ChaserTag>()
                      .ForEach((ref MoveData moveData, ref Rotation rot, in Translation pos, in TargetData targetData) =>
                      {
                          ComponentDataFromEntity<Translation> allTranslation =
                          GetComponentDataFromEntity<Translation>(true);

                          if (!allTranslation.Exists(targetData.targetEntity))
                              return;

                          Translation targetPos = allTranslation[targetData.targetEntity];

                          float3 dirToTarget = targetPos.Value - pos.Value;
                          moveData.direction = dirToTarget;

                      }).Run();
        }
    }
}