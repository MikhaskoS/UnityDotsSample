using Unity.Entities;


namespace Sample0_3
{
    [DisableAutoCreation]  // отключить для демонстрации
    public class LevelUpSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref LevelComponent levelComponent) =>
            {
                levelComponent.level += 1.0f * Time.DeltaTime;
            });
        }
    }
}
