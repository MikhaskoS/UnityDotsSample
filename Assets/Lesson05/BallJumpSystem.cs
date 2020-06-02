using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;

public class BallJumpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PhysicsVelocity physicVelosity) =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                physicVelosity.Linear.y = 5.0f;
            }
        });
    }
}
