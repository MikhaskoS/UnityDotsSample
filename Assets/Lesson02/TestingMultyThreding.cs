using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;


namespace Smaple2
{
    public class TestingMultyThreding : MonoBehaviour
    {
        [SerializeField] private bool useJob = false;

        void Update()
        {
            float startTime = Time.realtimeSinceStartup;

            if (!useJob)
            {
                for(int i = 0; i<10; i++)
                    ReallyToughTask();
            }
            else
            {
                // Выигрыш составит в [ количество процессоров ] раз. Это если без  [BurstCompile].
                // C  [BurstCompile] выигрыш огромный
                NativeList<JobHandle> jobHandleList = new NativeList<JobHandle>(Allocator.Temp);
                for (int i = 0; i < 10; i++)
                {
                    JobHandle jobHandle = ReallyToughTaskJob();
                    jobHandleList.Add(jobHandle);
                }
                JobHandle.CompleteAll(jobHandleList);
                jobHandleList.Dispose();
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

