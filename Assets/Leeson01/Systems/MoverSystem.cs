using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


namespace MkGame
{
    public class MoverSystem : ComponentSystem
    {

        protected override void OnUpdate()
        {
            Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
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
            });
        }

    }
}