using System;
using System.Collections;
using System.Collections.Generic;
using FixedPoint;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class TestPerformance : MonoBehaviour
{
    #if UNITY_EDITOR
    const int CAlCOunt = 10000000;
    const int CAlCOuntQ = 1000000;
    #else
    const int CAlCOunt = 1000000000;
    const int CAlCOuntQ = 10000000;
    #endif

    // Start is called before the first frame update
    void Start()
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Restart();
        var floatRet = 0f;
        for(int i = 0; i < CAlCOunt; i++)
        {
            floatRet = floatRet + i;
        }
        var x1 = watch.ElapsedMilliseconds;

        watch.Restart();
        fp ret = 0;
        for(int i = 0; i < CAlCOunt; i++)
        {
            ret = ret + i;
        }
        var y1 = watch.ElapsedMilliseconds;
        Debug.LogError($"+ 时间： {y1} float: {x1} result: {ret}  float {floatRet}");

        watch.Restart();
        floatRet = 0f;
        for(int i = 0; i < CAlCOunt; i++)
        {
            floatRet = floatRet - i;
        }
        x1 = watch.ElapsedMilliseconds;

        watch.Restart();
        ret = 0;
        for(int i = 0; i < CAlCOunt; i++)
        {
            ret = ret -i;
        }
        y1 = watch.ElapsedMilliseconds;
        Debug.LogError($"+ 时间： {y1} float: {x1} result: {ret}  float {floatRet}");

        watch.Restart();
        floatRet = 1f;
        for(int i = 0; i < CAlCOunt; i++)
        {
            floatRet = floatRet * i;
        }
        x1 = watch.ElapsedMilliseconds;

        watch.Restart();
        ret = 1;
        for(int i = 0; i < CAlCOunt; i++)
        {
            ret = ret * i;
        }
        y1 = watch.ElapsedMilliseconds;
        Debug.LogError($"* 时间： {y1} float: {x1} result: {ret}  float {floatRet}");

        watch.Restart();
        floatRet = 100000000f;
        for(int i = 0; i < CAlCOunt; i++)
        {
            floatRet = floatRet / i;
        }
        x1 = watch.ElapsedMilliseconds;

        watch.Restart();
        ret = 100000000;
        for(int i = 1; i < CAlCOunt; i++)
        {
            ret = ret / i;
        }
        y1 = watch.ElapsedMilliseconds;
        Debug.LogError($"/ 时间： {y1} float: {x1} result: {ret}  float {floatRet}");

        CheckMathOpt(watch);
        CheckRotationOpt(watch);
        CheckBurstOpt();
    }

    private void CheckBurstOpt()
    {
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        watch.Restart();
        JobFloat3 t = new JobFloat3(){
            result = new NativeArray<float3>(1, Allocator.TempJob)
        };
        t.Run();
        t.result.Dispose();
        var xxx = watch.ElapsedMilliseconds;
        watch.Restart();
        JobFloat3 x = new JobFloat3(){
            result = new NativeArray<float3>(1, Allocator.TempJob)
        };
        x.Run();
        xxx = watch.ElapsedMilliseconds;
        watch.Restart();
        JobFp3 x1 = new JobFp3(){
            result = new NativeArray<fp3>(1, Allocator.TempJob)
        };
        x1.Run();
        var y = watch.ElapsedMilliseconds;
        Debug.LogError($"{xxx} {y} ; {x.result[0]} {x1.result[0]}");
        x.result.Dispose();
        x1.result.Dispose();


        watch.Restart();
        var x11 = new JobFloat3Cross(){
            result = new NativeArray<float3>(1, Allocator.TempJob)
        };
        x11.Run();
        xxx = watch.ElapsedMilliseconds;
        watch.Restart();
        var x12 = new JobFp3Cross(){
            result = new NativeArray<fp3>(1, Allocator.TempJob)
        };
        x12.Run();
        y = watch.ElapsedMilliseconds;
        
        Debug.LogError($"{xxx} {y} ; {x11.result[0]} {x12.result[0]}");
        x11.result.Dispose();
        x12.result.Dispose();
    }

    [BurstCompile]
    struct JobFloat3 : IJob
    {
        public NativeArray<float3> result;
        public void Execute()
        {
            quaternion q = quaternion.AxisAngle(new float3(1, 0, 0), 30);
            float3 targetDir = new float3(1, 1, 1);
            for(int i = 0; i < 100000000; i++)
            {
                targetDir = math.mul(q, targetDir);
            }

            result[0] = targetDir;
        }
    }

    [BurstCompile]
    struct JobFp3Cross : IJob
    {
        public NativeArray<fp3> result;
        public void Execute()
        {
            fp3 tDir222 = new fp3(1, 2, 3);
            fp3 targetDir222 = new fp3(1, 1, 1);
            for(int i = 0; i < 100000000; i++)
            {
                targetDir222 = fpMath.cross(tDir222, targetDir222);
            }

            result[0] = targetDir222;
        }
    }

    [BurstCompile]
    struct JobFloat3Cross : IJob
    {
        public NativeArray<float3> result;
        public void Execute()
        {
            float3 tDir111 = new float3(1, 2, 3);
            float3 targetDir111 = new float3(1, 1, 1);
            for(int i = 0; i < 100000000; i++)
            {
                targetDir111 = math.cross(tDir111 , targetDir111);
            }

            result[0] = targetDir111;
        }
    }

    [BurstCompile]
    struct JobFp3 : IJob
    {
        public NativeArray<fp3> result;
        public void Execute()
        {
            fpQuaternion q = fpQuaternion.AxisAngle(new fp3(1, 0, 0), 30);
            fp3 targetDir = new fp3(1, 1, 1);
            for(int i = 0; i < 100000000; i++)
            {
                targetDir = fpMath.mul(q, targetDir);
            }

            result[0] = targetDir;
        }
    }

    private void CheckRotationOpt(System.Diagnostics.Stopwatch watch)
    {
        int count = CAlCOuntQ;
        watch.Restart();
        quaternion q = quaternion.AxisAngle(new float3(1, 0, 0), 30);
        float3 targetDir = new float3(1, 1, 1);
        for(int i = 1; i < count; i++)
        {
            targetDir = math.mul(q, targetDir);
        }

        var x = watch.ElapsedMilliseconds;

        watch.Restart();
        fpQuaternion qq = fpQuaternion.AxisAngle(new fp3(1, 0, 0), 30);
        fp3 targetDir1 = new fp3(1, 1, 1);
        for(int i = 1; i < count; i++)
        {
            targetDir1 = fpMath.mul(qq, targetDir1);
        }
        var y = watch.ElapsedMilliseconds;

        Debug.LogError($"opt :quaternion.mul time:{y} {x}, ret {targetDir1} {targetDir}");


        watch.Restart();
        fp3 tDir222 = new fp3(1, 2, 3);
        fp3 targetDir222 = new fp3(1, 1, 1);
        for(int i = 1; i < count; i++)
        {
            targetDir222 = fpMath.cross(tDir222, targetDir222);
        }
        y = watch.ElapsedMilliseconds;

        watch.Restart();
        float3 tDir111 = new float3(1, 2, 3);
        float3 targetDir111 = new float3(1, 1, 1);
        for(int i = 1; i < count; i++)
        {
            targetDir111 = math.cross(tDir111 , targetDir111);
        }

        x = watch.ElapsedMilliseconds;
        Debug.LogError($"opt :float3.cross time:{y} {x}, ret {targetDir111} {targetDir222}");

         watch.Restart();
        fp3 tDir22 = new fp3(1, 2, 3);
        fp3 targetDir22 = new fp3(1, 2, 3);
        for(int i = 1; i < count; i++)
        {
            targetDir22 = tDir22 * targetDir22;
        }
        y = watch.ElapsedMilliseconds;

        watch.Restart();
        float3 tDir11 = new float3(1, 2, 3);
        float3 targetDir11 = new float3(1, 2, 3);
        for(int i = 1; i < count; i++)
        {
            targetDir11 = tDir11 * targetDir11;
        }

        x = watch.ElapsedMilliseconds;

        Debug.LogError($"opt :float3 mul time:{y} {x}, ret {targetDir11} {targetDir22}");

        watch.Restart();
        fp3 targetDir2 = new fp3(1, 2, 3);
        for(int i = 1; i < count; i++)
        {
            targetDir2 = i * targetDir2;
        }
        y = watch.ElapsedMilliseconds;

        watch.Restart();
        float3 targetDir1xx = new float3(1, 2, 3);
        for(int i = 1; i < count; i++)
        {
            targetDir1xx = i * targetDir1xx;
        }

        x = watch.ElapsedMilliseconds;

        Debug.LogError($"opt :float3 int * fp  time:{y} {x}, ret {targetDir1xx} {targetDir2}");

        watch.Restart();
        float3 xxxx = 1000f;
        for(int i = 1; i < count; i++)
        {
            math.sincos(xxxx, out var s, out var c);
            xxxx = s;
        }
        x = watch.ElapsedMilliseconds;

        watch.Restart();
        fp3 fPxxxxz = new fp3(1000);
        for(int i = 1; i < count; i++)
        {
            fpMath.sincos(fPxxxxz, out var s, out var c);
            fPxxxxz = s;
        }

        var z = watch.ElapsedMilliseconds;

        Debug.LogError($"opt :sincos time:{x} {z}, ret {fPxxxxz}{xxxx}");
    }

    private void CheckMathOpt(System.Diagnostics.Stopwatch watch)
    {
        CheckOpt("abs", fpMath.abs, math.abs, CAlCOuntQ, watch);
        CheckOpt("sin", fpMath.sin, math.sin, CAlCOuntQ, watch);
        CheckOpt("cos", fpMath.cos, math.cos, CAlCOuntQ, watch);
        CheckOpt("sqrt", fpMath.sqrt, math.sqrt, CAlCOuntQ, watch);
    }

    private void CheckOpt(string opt, Func<fp, fp> abs1, Func<float, float> abs2, int count, System.Diagnostics.Stopwatch watch)
    {
        watch.Restart();
        float floatRet = 0;
        for(int i = 1; i < count; i++)
        {
            floatRet = abs2(floatRet);
        }

        var x = watch.ElapsedMilliseconds;

        watch.Restart();
        fp ret = 0;
        for(int i = 1; i < count; i++)
        {
            ret = abs1(ret);
        }
        var y = watch.ElapsedMilliseconds;

        Debug.LogError($"opt :{opt} time:{y} {x}, ret {ret} {floatRet}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
