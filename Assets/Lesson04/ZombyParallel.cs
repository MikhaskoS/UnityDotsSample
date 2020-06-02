using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Sample4
{
    public class ZombyParallel : MonoBehaviour
    {
        [SerializeField] Transform pfZombie = null;
        public List<Zombie> zombieList;

        public class Zombie
        {
            public Transform transform;
            public float moveY;
        }

        void Start()
        {
            zombieList = new List<Zombie>();
            for (int i = 0; i < 10000; i++)
            {
                Transform zombieTransform =
                    Instantiate(pfZombie,
                    new Vector3(Random.Range(-3f, 3f), Random.Range(-4f, 4f)),
                    Quaternion.identity);
                zombieList.Add(new Zombie { transform = zombieTransform, moveY = Random.Range(1f, 2f) });
            }
        }

        void Update()
        {
            float startTime = Time.realtimeSinceStartup;

            NativeArray<float3> positionArray = new NativeArray<float3>(zombieList.Count, Allocator.TempJob);
            NativeArray<float> moveYArray = new NativeArray<float>(zombieList.Count, Allocator.TempJob);

            for (int i = 0; i < zombieList.Count; i++)
            {
                positionArray[i] = zombieList[i].transform.position;
                moveYArray[i] = zombieList[i].moveY;
            }

            ReallyParallelJob reallyParallelJob = new ReallyParallelJob
            {
                deltaTime = Time.deltaTime,
                positionArray = positionArray,
                moveYArray = moveYArray
            };

            JobHandle jobHandle = reallyParallelJob.Schedule(zombieList.Count, 100);
            jobHandle.Complete();

            for (int i = 0; i < zombieList.Count; i++)
            {
                zombieList[i].transform.position = positionArray[i];
                zombieList[i].moveY = moveYArray[i];
            }

            positionArray.Dispose();
            moveYArray.Dispose();

            Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
        }
    }

    [BurstCompile]
    public struct ReallyParallelJob : IJobParallelFor
    {
        public NativeArray<float3> positionArray;
        public NativeArray<float> moveYArray;
        [ReadOnly] public float deltaTime;

        public void Execute(int index)
        {
            positionArray[index] += new float3(0, moveYArray[index] * deltaTime, 0f);
            if (positionArray[index].y > 5f)
                moveYArray[index] = -math.abs(moveYArray[index]);
            if (positionArray[index].y < -5f)
                moveYArray[index] = math.abs(moveYArray[index]);
        }
    }
}

