using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Burst;
using Unity.Jobs;
using Unity.Collections;
using Nt.Deterministics;


public class TestNumberBurst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

#if UNITY_EDITOR
    // [Button("TestNumberBurst")]
    public unsafe static void TestBurst()
    {
        if (!NumberLut.IsLoaded) AutoLoadLut.LoadLut();
        number[] inputs = new number[] { number.zero, number.PIDiv2, number.PI, number.PI3Div2, number.PITimes2 };
        foreach(var input in inputs)
        {
            var sin = number.Sin(input);
            var cos = number.Cos(input);
            var tan = number.Tan(input);
            Debug.Log($"input:{input}, sin : {sin}, cos : {cos}, tan : {tan}");
        }
        Debug.Log("==================================================");
        var job = new TestNumberBurstJob()
        {
            inputs = new NativeArray<number>(inputs, Allocator.TempJob),
            sinValues = new NativeArray<number>(inputs.Length, Allocator.TempJob),
            cosValues = new NativeArray<number>(inputs.Length, Allocator.TempJob),
            tanValues = new NativeArray<number>(inputs.Length, Allocator.TempJob),
        };
        var handler = job.Schedule(inputs.Length, 8);
        handler.Complete();
        for (int k = 0; k < inputs.Length; ++k)
        {
            Debug.Log($"input:{inputs[k]}, sin : {job.sinValues[k]}, cos : {job.cosValues[k]}, tan : {job.tanValues[k]}");
        }
    }
#endif
}

[BurstCompile]
public struct TestNumberBurstJob : IJobParallelFor
{
    [Unity.Collections.ReadOnly]
    public NativeArray<number> inputs;
    [WriteOnly]
    public NativeArray<number> sinValues;
    [WriteOnly]
    public NativeArray<number> cosValues;
    [WriteOnly]
    public NativeArray<number> tanValues;
    public void Execute(int index)
    {
        sinValues[index] = number.Sin(inputs[index]);
        cosValues[index] = number.Cos(inputs[index]);
        tanValues[index] = number.Tan(inputs[index]);
    }
}
