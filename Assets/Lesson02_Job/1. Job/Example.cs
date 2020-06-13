using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        NativeArray<float> resultArray = new NativeArray<float>(1, Allocator.TempJob);

        // 1. Instantiate Job
        SimpleJob myJob = new SimpleJob
        {
            // 2. Initialize Job
            a = 5.0f,
            result = resultArray
        };
        AnotherJob secondJob = new AnotherJob
        {
            result = resultArray
        };

        // 3. Shedule Job
        JobHandle handle = myJob.Schedule();
        JobHandle secondHandle = secondJob.Schedule(handle); // !! передаем дескриптор во вторую работу

        // 4. Complete Job
        handle.Complete();  // работу следует завершить прежде, чем пользоваться результатами
        secondHandle.Complete();

        float resultingValue = resultArray[0];
        Debug.Log("result = " + resultingValue);

        resultArray.Dispose();
    }

    private struct SimpleJob : IJob
    {
        public float a;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = a;
        }
    }

    private struct AnotherJob : IJob
    {
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = result[0] + 1;
        }
    }
}
