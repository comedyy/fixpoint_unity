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
        fp[] inputs = new fp[] { fp.zero, fp.PIDiv2, fp.PI, fp.PI3Div2, fp.PITimes2 };
        foreach(var input in inputs)
        {
            var sin = fp.Sin(input);
            var cos = fp.Cos(input);
            var tan = fp.Tan(input);
            Debug.Log($"input:{input}, sin : {sin}, cos : {cos}, tan : {tan}");
        }
        Debug.Log("==================================================");
        var job = new TestNumberBurstJob()
        {
            inputs = new NativeArray<fp>(inputs, Allocator.TempJob),
            sinValues = new NativeArray<fp>(inputs.Length, Allocator.TempJob),
            cosValues = new NativeArray<fp>(inputs.Length, Allocator.TempJob),
            tanValues = new NativeArray<fp>(inputs.Length, Allocator.TempJob),
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
    public NativeArray<fp> inputs;
    [WriteOnly]
    public NativeArray<fp> sinValues;
    [WriteOnly]
    public NativeArray<fp> cosValues;
    [WriteOnly]
    public NativeArray<fp> tanValues;
    public void Execute(int index)
    {
        sinValues[index] = fp.Sin(inputs[index]);
        cosValues[index] = fp.Cos(inputs[index]);
        tanValues[index] = fp.Tan(inputs[index]);
    }
}
