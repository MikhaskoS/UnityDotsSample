using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


[AlwaysSynchronizeSystem]
public class PaddleMovementSystem : JobComponentSystem
{
	// Запуск в основном потоке
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float deltaTime = Time.DeltaTime;
		float yBound = GameManager.main.yBound;

		Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>
		{
			trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
		}).Run();

		return default;
	}

	// Для более сложных систем, если нужно запускать процессы в разных потоках
	//protected override JobHandle OnUpdate(JobHandle inputDeps)
	//{
	//	float deltaTime = Time.DeltaTime;
	//	float yBound = GameManager.main.yBound;

	//	JobHandle myJob = Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>
	//	{
	//		trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
	//	}).Schedule(inputDeps);

	//	return myJob;
	//}
}