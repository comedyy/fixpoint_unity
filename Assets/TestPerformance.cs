using System;
using System.Collections;
using System.Collections.Generic;
using Mathematics.FixedPoint;
using Unity.Mathematics;
using UnityEngine;

public class TestPerformance : MonoBehaviour
{
    #if UNITY_EDITOR
    const int CAlCOunt = 10000000;
    const int CAlCOuntQ = 1000000;
    #else
    const int CAlCOunt = 1000000000;
    const int CAlCOuntQ = 100000000;
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
            targetDir222 = fpMath.cross(new fp3(i, i, i), targetDir222);
        }
        y = watch.ElapsedMilliseconds;

        watch.Restart();
        float3 tDir111 = new float3(1, 2, 3);
        float3 targetDir111 = new float3(1, 1, 1);
        for(int i = 1; i < count; i++)
        {
            targetDir111 = math.cross(new float3(i, i, i) , targetDir111);
        }

        x = watch.ElapsedMilliseconds;
        Debug.LogError($"opt :float3.cross time:{y} {x}, ret {targetDir111} {targetDir222}");

        watch.Restart();
        fp3 tDir22 = new fp3(1, 2, 3);
        fp3 targetDir22 = new fp3(1, 2, 3);
        for(int i = 1; i < count; i++)
        {
            targetDir22 = new fp3(i, i, i) * targetDir22;

            if(targetDir22.x > int.MaxValue)
            {
                targetDir22 = new fp3(1, 2, 3);
            }
        }
        y = watch.ElapsedMilliseconds;

        watch.Restart();
        float3 tDir11 = new float3(1, 2, 3);
        float3 targetDir11 = new float3(1, 2, 3);
        for(int i = 1; i < count; i++)
        {
            targetDir11 = new float3(i, i, i) * targetDir11;

            if(targetDir11.x > int.MaxValue)
            {
                targetDir11 = new float3(1, 2, 3);
            }
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

        Debug.LogError($"opt :float3 mul1 time:{y} {x}, ret {targetDir1xx} {targetDir2}");
    }

    private void CheckMathOpt(System.Diagnostics.Stopwatch watch)
    {
        CheckOpt("abs", fpMath.abs, math.abs, CAlCOunt, watch);
        CheckOpt("sin", fpMath.sin, math.sin, CAlCOunt, watch);
        CheckOpt("cos", fpMath.cos, math.cos, CAlCOunt, watch);
        CheckOpt("sqrt", fpMath.sqrt, math.sqrt, CAlCOunt, watch);
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
