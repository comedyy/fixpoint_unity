using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using Deterministics.Math;
using Random = UnityEngine.Random;
using System;
using System.Linq;

namespace Tests
{
    public class TestMath
    {
        const int TEST_COUNT = 100000;
        public static (fp, float) GetRandom1(float min = -99.0f, float max = 99.0f)
        {
            var r = Random.Range(min,max);
            var num = fp.ConvertFrom(r);

            return (num, r);
        }

        public static (fp2, Unity.Mathematics.float2) GetRandom2(float min = -99.0f, float max = 99.0f)
        {
            (var n, var f) = GetRandom1(min, max);
            (var n1, var f1) = GetRandom1(min, max);

            return (new fp2(n, n1), new Unity.Mathematics.float2(f, f1));
        }

        public static (fp3, Unity.Mathematics.float3) GetRandom3()
        {
            (var n, var f) = GetRandom1();
            (var n1, var f1) = GetRandom1();
            (var n2, var f2) = GetRandom1();

            return (new fp3(n, n1, n2), new Unity.Mathematics.float3(f, f1, f2));
        }

        public static (fp4, Unity.Mathematics.float4) GetRandom4()
        {
            (var n, var f) = GetRandom1();
            (var n1, var f1) = GetRandom1();
            (var n2, var f2) = GetRandom1();
            (var n3, var f3) = GetRandom1();

            return (new fp4(n, n1, n2, n3), new Unity.Mathematics.float4(f, f1, f2, f3));
        }

        [Test]
        public void TestFmod()
        {
            TestRotation.InitLookupTable();
            TestFunc1(fpMath.fmod, Unity.Mathematics.math.fmod, -1000, 1000, 0.001f, "fmod", 0.95f);
        }

        [Test]
        public void TestNormalized()
        {
            TestRotation.InitLookupTable();
        }

        [Test]
        public void TestWriteSqrtLut()
        {
            var one = fp.one.RawValue;
            var four = (fp.one* 4).RawValue;
            List<float> list = new List<float>();
            for(int i = (int)one; i < four; i+=4)
            {
                var current = (1.0f * i) / one;
                var value = Unity.Mathematics.math.sqrt(current);
                list.Add(value);
            }

            string s = string.Join(",", list.Select((m, i)=>{
                var value = fp.ConvertFrom(m).RawValue - one;
                if(i % 10 == 0){
                    return "\n" + value;
                }
                else
                {
                    return value.ToString();
                }
                }));
            File.WriteAllText("d://xx.txt", s);

            // var total = 1.0f * ((1 << fixlut.PRECISION) * 3) / 4;
            // Debug.LogError(total);
            // List<ushort> list = new List<ushort>();
            // for(int i = 0; i < count; i++)
            // {
            //     list.Add((ushort)(Unity.Mathematics.math.sqrt(1.0f * i / count) * taotal + 0.5f));
            // }
        }

        [Test]
        public void TestSinCos()
        {
            TestRotation.InitLookupTable();

            for(int i = 0; i < 100000; i++)
            {
                (var r1, var r2) = GetRandom1();
                (var r12, var r22) = GetRandom1();
                (var lerp1, var lerp2) = GetRandom1(-2.0f, 2.0f);
                (var r13, var r23) = GetRandom1();
                (var r14, var r24) = GetRandom1();
                (var r15, var r25) = GetRandom1(-1.0f, 1.0f);
                (var r16, var r26) = GetRandom1(0f, 3.0f);
                (var r17, var r27) = GetRandom1(0, 99999999f);
                (var r18, var r28) = GetRandom1(0.1f, 10f);

                fpMath.sincos(r1, out var s, out var c);
                Unity.Mathematics.math.sincos(r1, out var s1, out var c1);
                Assert.IsTrue(fpMath.Approximately(s, s1));
                Assert.IsTrue(fpMath.Approximately(c, c1));

                Assert.IsTrue(fpMath.Approximately(fpMath.modf(r1, out _), Unity.Mathematics.math.modf(r2, out _)));

                // Assert.IsTrue(math.Approximately(math.tanh(r1), Unity.Mathematics.math.tanh(r2)));
                // Assert.IsTrue(math.Approximately(math.sinh(r1), Unity.Mathematics.math.sinh(r2)));
                // Assert.IsTrue(math.Approximately(math.cosh(r1), Unity.Mathematics.math.cosh(r2)));

                // Assert.IsTrue(math.Approximately(math.tan(r1), Unity.Mathematics.math.tan(r2)), $"{math.tan(r1)} {Unity.Mathematics.math.tan(r2)} {r1} {r2}");
                // Assert.IsTrue(fpMath.Approximately(fpMath.atan(r1), Unity.Mathematics.math.atan(r2)));
                // Assert.IsTrue(fpMath.Approximately(fpMath.atan2(r1, r12), Unity.Mathematics.math.atan2(r2, r22)));

                TestFunc(fpMath.atan, Unity.Mathematics.math.atan, -100f, 100f, 0.001f, "atan");
                TestFunc(fpMath.atan2, Unity.Mathematics.math.atan2, -100f, 100f, 0.001f, "atan2");
                
                TestFunc(fpMath.cos, Unity.Mathematics.math.cos, -100f, 100f, 0.001f, "cos");
                TestFunc(fpMath.sin, Unity.Mathematics.math.sin, -100f, 100f, 0.001f, "sin");

                TestFunc(fpMath.asin, Unity.Mathematics.math.asin, -1f, 1f, 0.001f, "asin");
                TestFunc(fpMath.acos, Unity.Mathematics.math.acos, -1f, 1f, 0.001f, "acos");

                TestFunc(fpMath.max, Unity.Mathematics.math.max, -100f, 100f, 0.001f, "max");
                TestFunc(fpMath.min, Unity.Mathematics.math.min, -100f, 100f, 0.001f, "min");

                TestFunc(fpMath.mad, Unity.Mathematics.math.mad, -100f, 100f, 0.001f, "mad");
                TestFunc(fpMath.clamp, Unity.Mathematics.math.clamp, -100f, 100f, 0.001f, "clamp");
                TestFunc(fpMath.saturate, Unity.Mathematics.math.saturate, -100f, 100f, 0.001f, "saturate");
                TestFunc(fpMath.abs, Unity.Mathematics.math.abs, -100f, 100f, 0.001f, "abs");
                TestFunc(fpMath.dot, Unity.Mathematics.math.dot, -100f, 100f, 0.001f, "dot");

                TestFunc(fpMath.lerp, Unity.Mathematics.math.lerp, -100f, 100f, 0.001f, "lerp");
                TestFunc(fpMath.unlerp, Unity.Mathematics.math.unlerp, -100f, -50f, 0.001f, "unlerp");
                TestFunc(fpMath.remap, Unity.Mathematics.math.remap, -100f, 100f, 0.001f, "remap");
                TestFunc(fpMath.square, (x)=>x*x, -100f, 100f, 0.001f, "square");

                TestFunc(fpMath.floor, Unity.Mathematics.math.floor, -100f, 100f, 0.001f, "floor");
                TestFunc(fpMath.round, Unity.Mathematics.math.round, -100f, 100f, 0.001f, "round");
                TestFunc(fpMath.trunc, Unity.Mathematics.math.trunc, -100f, 100f, 0.001f, "trunc");
                TestFunc(fpMath.frac, Unity.Mathematics.math.frac, -100f, 100f, 0.001f, "frac");
                TestFunc(fpMath.rcp, Unity.Mathematics.math.rcp, -100f, 100f, 0.001f, "rcp");
                TestFunc(fpMath.sign, Unity.Mathematics.math.sign, -100f, 100f, 0.001f, "sign");

                TestFunc(fpMath.pow, Unity.Mathematics.math.pow, 0, 5, 0.001f, "pow");
                TestFunc(fpMath.exp, Unity.Mathematics.math.exp, -5, 5, 0.001f, "exp");
                TestFunc(fpMath.exp2, Unity.Mathematics.math.exp2, -5, 5, 0.001f, "exp2");
                TestFunc(fpMath.exp10, Unity.Mathematics.math.exp10, -5, 5, 0.001f, "exp10");
                TestFunc(fpMath.log, Unity.Mathematics.math.log, 0, 500, 0.001f, "log");
                TestFunc(fpMath.log2, Unity.Mathematics.math.log2, 0, 500, 0.001f, "log2");
                TestFunc(fpMath.log10, Unity.Mathematics.math.log10, 0, 500, 0.001f, "log10");

                TestFunc(fpMath.sqrt, Unity.Mathematics.math.sqrt, 0, 99999f, 0.001f, "sqrt");
                TestFunc(fpMath.rsqrt, Unity.Mathematics.math.rsqrt, 0, 99999f, 0.001f, "rsqrt");

                // Assert.IsTrue(math.Approximately(math.normalize(r1), Unity.Mathematics.math.normalize(r2)));

                // Assert.IsTrue(math.Approximately(math.smoothstep(r1, r12, lerp1), Unity.Mathematics.math.smoothstep(r2, r22, lerp2)), $"{math.smoothstep(r1, r12, lerp1)} {Unity.Mathematics.math.smoothstep(r2, r22, lerp2)}, {r1} {r12} {lerp1}");
                TestFunc(fpMath.radians, Unity.Mathematics.math.radians, 0, 99999f, 0.001f, "radians");
                TestFunc(fpMath.degrees, Unity.Mathematics.math.degrees, 0, 99999f, 0.001f, "degrees");
            }
        }

        [Test]
        public void TestMisc()
        {
            var f1 = fp.CreateByDivisor(-11, 10);
            Assert.IsTrue(fpMath.Approximately(f1, -1.1f), $"{f1}");
            Assert.IsTrue(fpMath.Approximately(fp.CreateByDivisor(-1232131312312123, 10000), -1232131312312123 / 10000f));



            Assert.IsTrue(fpMath.Approximately(fp.Parse("-0.111"), -0.111f));
            Assert.IsTrue(fpMath.Approximately(fp.Parse("0.111"), 0.111f));

            Assert.IsTrue(fpMath.Approximately(fp.Parse("-1.111"), -1.111f));
            Assert.IsTrue(fpMath.Approximately(fp.Parse("1.111"), 1.111f));


            Assert.IsTrue(fpMath.Approximately(fp.Parse("-1111111111.1223"), -1111111111.1223f), $"{fp.Parse("-1111111111.1223")}");
            Assert.IsTrue(fpMath.Approximately(fp.Parse("1111111111.1223"), 1111111111.1223f));
        }

        [Test]
        public void TestSinCos2()
        {
            TestRotation.InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                (var r1, var r2) = GetRandom2();
                (var r12, var r22) = GetRandom2();
                (var lerp1, var lerp2) = GetRandom2(-2.0f, 2.0f);
                (var r13, var r23) = GetRandom2();
                (var r14, var r24) = GetRandom2();
                (var r15, var r25) = GetRandom2(-1.0f, 1.0f);
                (var r16, var r26) = GetRandom2(0f, 3.0f);
                (var r17, var r27) = GetRandom2(0, 99999999f);
                (var r18, var r28) = GetRandom2(0.1f, 10f);

                fpMath.sincos(r1, out var s, out var c);
                Unity.Mathematics.math.sincos(r2, out var s1, out var c1);
                Assert.IsTrue(fpMath.Approximately(s, s1));
                Assert.IsTrue(fpMath.Approximately(c, c1));

                // Assert.IsTrue(math.Approximately(math.tan(r1), Unity.Mathematics.math.tan(r2)), $"{math.tan(r1)} {Unity.Mathematics.math.tan(r2)} {r1} {r2}");
                // Assert.IsTrue(math.Approximately(math.tanh(r1), Unity.Mathematics.math.tanh(r2)));
                // Assert.IsTrue(math.Approximately(math.cosh(r1), Unity.Mathematics.math.cosh(r2)));
                // Assert.IsTrue(math.Approximately(math.sinh(r1), Unity.Mathematics.math.sinh(r2)));

                Assert.IsTrue(fpMath.Approximately(fpMath.atan(r1), Unity.Mathematics.math.atan(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.atan2(r1, r12), Unity.Mathematics.math.atan2(r2, r22)));
                Assert.IsTrue(fpMath.Approximately(fpMath.cos(r1), Unity.Mathematics.math.cos(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.acos(r15), Unity.Mathematics.math.acos(r25)), $"{fpMath.acos(r15)} {Unity.Mathematics.math.acos(r25)} {r15} {r25}");
                Assert.IsTrue(fpMath.Approximately(fpMath.sin(r1), Unity.Mathematics.math.sin(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.asin(r15), Unity.Mathematics.math.asin(r25)));
  
                Assert.IsTrue(fpMath.Approximately(fpMath.max(r1, r12), Unity.Mathematics.math.max(r2, r22)));
                Assert.IsTrue(fpMath.Approximately(fpMath.min(r1, r12), Unity.Mathematics.math.min(r2, r22)));

                Assert.IsTrue(fpMath.Approximately(fpMath.lerp(r1, r12, lerp1), Unity.Mathematics.math.lerp(r2, r22, lerp2)), $"{fpMath.lerp(r1, r12, lerp1)} {Unity.Mathematics.math.lerp(r2, r22, lerp2)} {r1} {r12} {lerp1}");
                Assert.IsTrue(fpMath.Approximately(fpMath.unlerp(r1, r12, lerp1), Unity.Mathematics.math.unlerp(r2, r22, lerp2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.remap(r1, r12, r13, r14, lerp1), Unity.Mathematics.math.remap(r2, r22, r23, r24, lerp2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.mad(r1, r12, lerp1), Unity.Mathematics.math.mad(r2, r22, lerp2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.clamp(r1, r12, lerp1), Unity.Mathematics.math.clamp(r2, r22, lerp2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.saturate(r1), Unity.Mathematics.math.saturate(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.abs(r1), Unity.Mathematics.math.abs(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.dot(r1, r12), Unity.Mathematics.math.dot(r2, r22)));


                Assert.IsTrue(fpMath.Approximately(fpMath.floor(r1), Unity.Mathematics.math.floor(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.ceil(r1), Unity.Mathematics.math.ceil(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.round(r1), Unity.Mathematics.math.round(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.trunc(r1), Unity.Mathematics.math.trunc(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.frac(r1), Unity.Mathematics.math.frac(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.rcp(r18), Unity.Mathematics.math.rcp(r28)), $"{fpMath.rcp(r1)} {Unity.Mathematics.math.rcp(r28)} {r18}");
                Assert.IsTrue(fpMath.Approximately(fpMath.sign(r1), Unity.Mathematics.math.sign(r2)));
                // Assert.IsTrue(math.Approximately(math.pow(r1, r16), Unity.Mathematics.math.pow(r2, r26)), $"{math.pow(r1, r16)} {Unity.Mathematics.math.pow(r2, r26)}, {r1} {r16}");
                // Assert.IsTrue(math.Approximately(math.exp(r1), Unity.Mathematics.math.exp(r2)));
                // Assert.IsTrue(math.Approximately(math.exp2(r1), Unity.Mathematics.math.exp2(r2)));
                // Assert.IsTrue(math.Approximately(math.exp10(r1), Unity.Mathematics.math.exp10(r2)));
                // Assert.IsTrue(math.Approximately(math.log(r1), Unity.Mathematics.math.log(r2)));
                // Assert.IsTrue(math.Approximately(math.log2(r1), Unity.Mathematics.math.log2(r2)));
                // Assert.IsTrue(math.Approximately(math.log10(r1), Unity.Mathematics.math.log10(r2)));
                // Assert.IsTrue(fpMath.Approximately(fpMath.fmod(r1, r12), Unity.Mathematics.math.fmod(r2, r22)), $"{fpMath.fmod(r1, r12)} {Unity.Mathematics.math.fmod(r2, r22)} {r1} {r12}");
                Assert.IsTrue(fpMath.Approximately(fpMath.modf(r1, out _), Unity.Mathematics.math.modf(r2, out _)));
                Assert.IsTrue(fpMath.Approximately(fpMath.sqrt(r17), Unity.Mathematics.math.sqrt(r27)));
                Assert.IsTrue(fpMath.Approximately(fpMath.rsqrt(r17), Unity.Mathematics.math.rsqrt(r27)));
                // Assert.IsTrue(math.Approximately(math.normalize(r1), Unity.Mathematics.math.normalize(r2)));

                // Assert.IsTrue(math.Approximately(math.smoothstep(r1, r12, lerp1), Unity.Mathematics.math.smoothstep(r2, r22, lerp2)), $"{math.smoothstep(r1, r12, lerp1)} {Unity.Mathematics.math.smoothstep(r2, r22, lerp2)}, {r1} {r12} {lerp1}");

                Assert.IsTrue(fpMath.Approximately(fpMath.radians(r1), Unity.Mathematics.math.radians(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.degrees(r1), Unity.Mathematics.math.degrees(r2)));
  
                Assert.IsTrue(fpMath.Approximately(fpMath.normalize(r1), Unity.Mathematics.math.normalize(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.length(r1), Unity.Mathematics.math.length(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.lengthsq(r1), Unity.Mathematics.math.lengthsq(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.distance(r1, r12), Unity.Mathematics.math.distance(r2, r22)));
                Assert.IsTrue(fpMath.Approximately(fpMath.distancesq(r1, r12), Unity.Mathematics.math.distancesq(r2, r22)));

                Assert.IsTrue(fpMath.Approximately(fpMath.cmin(r1), Unity.Mathematics.math.cmin(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.cmax(r1), Unity.Mathematics.math.cmax(r2)));
                Assert.IsTrue(fpMath.Approximately(fpMath.csum(r1), Unity.Mathematics.math.csum(r2)));

                Vector3 x = new fp3(1, 1, 1);
                Quaternion q = new fpQuaternion(0, 0, 0, 1);
                // cross

                // any
                // anyNaN
                // all
                // select
                // step
                // reflect
                // refract
                // project
                // faceforward

                // cmin
                // cmax
                // csum
                // compress
                // angle
            }
        }
        
        void TestFunc(Func<fp, fp> f1, Func<float, float> f2, float start, float end, float toleranceRate, string message)
        {
            var t = fp.ConvertFrom(Random.Range(start, end));

            var result1 = f1(t);
            var result2 = f2(t);
            Assert.IsTrue(fpMath.Approximately(result1, result2, toleranceRate), $"{message}:{t} result:{result1} {result2}");
        }

        void TestFunc1(Func<fp, fp, fp> f1, Func<float, float, float> f2, float start, float end, float toleranceRate, string message, float passRate)
        {
            int count = 0;
            for(int i = 0; i < TEST_COUNT; i++)
            {
                var t = fp.ConvertFrom(Random.Range(start, end));
                var t1 = fp.ConvertFrom(Random.Range(start, end));

                var result1 = f1(t, t1);
                var result2 = f2(t, t1);

                var ret = fpMath.Approximately(result1, result2, toleranceRate);
                if(ret)
                {
                    count++;
                }
            }
          
            var rate = count / TEST_COUNT;
            Assert.IsTrue(rate > passRate, $"{message} :{rate}");
        }

        void TestFunc(Func<fp, fp, fp> f1, Func<float, float, float> f2, float start, float end, float toleranceRate, string message)
        {
            var t = fp.ConvertFrom(Random.Range(start, end));
            var t1 = fp.ConvertFrom(Random.Range(start, end));

            var result1 = f1(t, t1);
            var result2 = f2(t, t1);

            var ret = fpMath.Approximately(result1, result2, toleranceRate);
            Assert.IsTrue(ret, $"{message}:{t} {t1} result:{result1} {result2}");
        }

        void TestFunc(Func<fp, fp, fp, fp> f1, Func<float, float, float, float> f2, float start, float end, float toleranceRate, string message)
        {
            var t = fp.ConvertFrom(Random.Range(start, end));
            var t1 = fp.ConvertFrom(Random.Range(start, end));
            var t2 = fp.ConvertFrom(Random.Range(start, end));

            var result1 = f1(t, t1, t2);
            var result2 = f2(t, t1, t2);
            Assert.IsTrue(fpMath.Approximately(result1, result2, toleranceRate), $"{message}:{t} {t1} {t2} result:{result1} {result2}");
        }

        void TestFunc(Func<fp, fp, fp, fp, fp, fp> f1, Func<float, float, float, float, float, float> f2, float start, float end, float toleranceRate, string message)
        {
            var t = fp.ConvertFrom(Random.Range(start, end));
            var t1 = fp.ConvertFrom(Random.Range(start, end));
            var t2 = fp.ConvertFrom(Random.Range(start, end));
            var t3 = fp.ConvertFrom(Random.Range(start, end));
            var t4 = fp.ConvertFrom(Random.Range(start, end));

            var result1 = f1(t, t1, t2, t3, t4);
            var result2 = f2(t, t1, t2, t3, t4);
            Assert.IsTrue(fpMath.Approximately(result1, result2, toleranceRate), $"{message}:{t} {t1} {t2} {t3} {t4} result:{result1} {result2}");
        }
    }
}
