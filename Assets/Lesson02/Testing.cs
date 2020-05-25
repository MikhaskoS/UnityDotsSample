using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

// https://www.youtube.com/watch?v=C56bbgtPr_w&list=PLzDRvYVwl53s40yP5RQXitbT--IRcHqba&index=2
namespace Sample1
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private bool useJob = false;

        void Update()
        {
            float startTime = Time.realtimeSinceStartup;

            if (!useJob)
            {
                ReallyToughTask();
            }
            else
            {
                JobHandle jobHandle = ReallyToughTaskJob();
                jobHandle.Complete();
            }

            Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
        }

        private void ReallyToughTask()
        {
            float value = 0;
            for (int i = 0; i < 50000; i++)
                value = math.exp10(math.sqrt(value));
        }

        private JobHandle ReallyToughTaskJob()
        {
            ReallyToughJob job = new ReallyToughJob();
            return job.Schedule();
        }
    }

    // [BurstCompile]  -  выигрыш почти 100 раз
    [BurstCompile]
    public struct ReallyToughJob : IJob
    {
        public void Execute()
        {
            float value = 0;
            for (int i = 0; i < 50000; i++)
                value = math.exp10(math.sqrt(value));
        }
    }
}

