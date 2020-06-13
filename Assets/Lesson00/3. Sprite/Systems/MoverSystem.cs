using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Diagnostics;

namespace Sample0_3
{
    //[DisableAutoCreation]  // отключить для демонстрации
    //public class MoverSystem : ComponentSystem
    //{
    //    protected override void OnUpdate()
    //    {
    //        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
    //        {
    //            translation.Value.y += moveSpeedComponent.moveSpeed * Time.DeltaTime;
    //            if (translation.Value.y > 4f)
    //            {
    //                moveSpeedComponent.moveSpeed = -math.abs(moveSpeedComponent.moveSpeed);
    //            }
    //            if (translation.Value.y < -4f)
    //            {
    //                moveSpeedComponent.moveSpeed = +math.abs(moveSpeedComponent.moveSpeed);
    //            }
    //        });
    //    }

    //}
    public class MoverSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithoutBurst()
                .WithName("MoverSystem")
                .ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
                {
                    translation.Value.y += moveSpeedComponent.moveSpeed * Time.DeltaTime;
                    if (translation.Value.y > 4f)
                    {
                        moveSpeedComponent.moveSpeed = -math.abs(moveSpeedComponent.moveSpeed);
                    }
                    if (translation.Value.y < -4f)
                    {
                        moveSpeedComponent.moveSpeed = +math.abs(moveSpeedComponent.moveSpeed);
                    }
                }).Run();
        }

    }
}