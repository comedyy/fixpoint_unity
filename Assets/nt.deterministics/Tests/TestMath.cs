using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using Nt.Deterministics;
using Random = UnityEngine.Random;

namespace Tests
{
    public class TestMath
    {
        (fp, float) GetRandom1(float min = -99.0f, float max = 99.0f)
        {
            var r = Random.Range(min,max);
            var num = fp.ConvertFrom(r);

            return (num, r);
        }

        (fp2, Unity.Mathematics.float2) GetRandom2(float min = -99.0f, float max = 99.0f)
        {
            (var n, var f) = GetRandom1(min, max);
            (var n1, var f1) = GetRandom1(min, max);

            return (new fp2(n, n1), new Unity.Mathematics.float2(f, f1));
        }

        (fp3, Unity.Mathematics.float3) GetRandom3()
        {
            (var n, var f) = GetRandom1();
            (var n1, var f1) = GetRandom1();
            (var n2, var f2) = GetRandom1();

            return (new fp3(n, n1, n2), new Unity.Mathematics.float3(f, f1, f2));
        }

        (float4, Unity.Mathematics.float4) GetRandom4()
        {
            (var n, var f) = GetRandom1();
            (var n1, var f1) = GetRandom1();
            (var n2, var f2) = GetRandom1();
            (var n3, var f3) = GetRandom1();

            return (new float4(n, n1, n2, n3), new Unity.Mathematics.float4(f, f1, f2, f3));
        }

        [Test]
        public void TestSinCos1()
        {
            TestRotation.InitLookupTable();
            // (var r1, var r2) = (number.ConvertFrom(-351.501f), -351.501f);
            (var r1, var r2) = (fp.ConvertFrom(1.5f), 1.5f);
            Assert.IsTrue(math.Approximately(math.tan(r1), Unity.Mathematics.math.tan(r2)), $"{math.tan(r1)} {Unity.Mathematics.math.tan(r2)} {r1} {r2}");
            Debug.LogError(math.tan(r1));
            Debug.LogError(Unity.Mathematics.math.tan(r2));
        }

        [Test]
        public void TestSinCos()
        {
            TestRotation.InitLookupTable();

            for(int i = 0; i < 1; i++)
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

                math.sincos(r1, out var s, out var c);
                Unity.Mathematics.math.sincos(r2, out var s1, out var c1);

                // Assert.IsTrue(math.Approximately(math.tan(r1), Unity.Mathematics.math.tan(r2)), $"{math.tan(r1)} {Unity.Mathematics.math.tan(r2)} {r1} {r2}");
                // Assert.IsTrue(math.Approximately(math.tanh(r1), Unity.Mathematics.math.tanh(r2)));
                Assert.IsTrue(math.Approximately(math.atan(r1), Unity.Mathematics.math.atan(r2)));
                Assert.IsTrue(math.Approximately(math.atan2(r1, r12), Unity.Mathematics.math.atan2(r2, r22)));
                // Assert.IsTrue(math.Approximately(math.cosh(r1), Unity.Mathematics.math.cosh(r2)));
                Assert.IsTrue(math.Approximately(math.cos(r1), Unity.Mathematics.math.cos(r2)));
                Assert.IsTrue(math.Approximately(math.acos(r15), Unity.Mathematics.math.acos(r25)), $"{math.acos(r15)} {Unity.Mathematics.math.acos(r25)} {r15} {r25}");
                // Assert.IsTrue(math.Approximately(math.sinh(r1), Unity.Mathematics.math.sinh(r2)));
                Assert.IsTrue(math.Approximately(math.sin(r1), Unity.Mathematics.math.sin(r2)));
                Assert.IsTrue(math.Approximately(math.asin(r15), Unity.Mathematics.math.asin(r25)));

                Assert.IsTrue(math.Approximately(s, s1));
                Assert.IsTrue(math.Approximately(c, c1));

                Assert.IsTrue(math.Approximately(math.max(r1, r12), Unity.Mathematics.math.max(r2, r22)));
                Assert.IsTrue(math.Approximately(math.min(r1, r12), Unity.Mathematics.math.min(r2, r22)));

                Assert.IsTrue(math.Approximately(math.lerp(r1, r12, lerp1), Unity.Mathematics.math.lerp(r2, r22, lerp2)), $"{math.lerp(r1, r12, lerp1)} {Unity.Mathematics.math.lerp(r2, r22, lerp2)} {r1} {r12} {lerp1}");
                Assert.IsTrue(math.Approximately(math.unlerp(r1, r12, lerp1), Unity.Mathematics.math.unlerp(r2, r22, lerp2)));
                Assert.IsTrue(math.Approximately(math.remap(r1, r12, r13, r14, lerp1), Unity.Mathematics.math.remap(r2, r22, r23, r24, lerp2)));
                Assert.IsTrue(math.Approximately(math.mad(r1, r12, lerp1), Unity.Mathematics.math.mad(r2, r22, lerp2)));
                Assert.IsTrue(math.Approximately(math.clamp(r1, r12, lerp1), Unity.Mathematics.math.clamp(r2, r22, lerp2)));
                Assert.IsTrue(math.Approximately(math.saturate(r1), Unity.Mathematics.math.saturate(r2)));
                Assert.IsTrue(math.Approximately(math.abs(r1), Unity.Mathematics.math.abs(r2)));
                Assert.IsTrue(math.Approximately(math.dot(r1, r12), Unity.Mathematics.math.dot(r2, r22)));


                Assert.IsTrue(math.Approximately(math.floor(r1), Unity.Mathematics.math.floor(r2)));
                Assert.IsTrue(math.Approximately(math.ceil(r1), Unity.Mathematics.math.ceil(r2)));
                Assert.IsTrue(math.Approximately(math.round(r1), Unity.Mathematics.math.round(r2)));
                Assert.IsTrue(math.Approximately(math.trunc(r1), Unity.Mathematics.math.trunc(r2)));
                Assert.IsTrue(math.Approximately(math.frac(r1), Unity.Mathematics.math.frac(r2)));
                Assert.IsTrue(math.Approximately(math.rcp(r18), Unity.Mathematics.math.rcp(r28)), $"{math.rcp(r1)} {Unity.Mathematics.math.rcp(r28)} {r18}");
                Assert.IsTrue(math.Approximately(math.sign(r1), Unity.Mathematics.math.sign(r2)));
                // Assert.IsTrue(math.Approximately(math.pow(r1, r16), Unity.Mathematics.math.pow(r2, r26)), $"{math.pow(r1, r16)} {Unity.Mathematics.math.pow(r2, r26)}, {r1} {r16}");
                // Assert.IsTrue(math.Approximately(math.exp(r1), Unity.Mathematics.math.exp(r2)));
                // Assert.IsTrue(math.Approximately(math.exp2(r1), Unity.Mathematics.math.exp2(r2)));
                // Assert.IsTrue(math.Approximately(math.exp10(r1), Unity.Mathematics.math.exp10(r2)));
                // Assert.IsTrue(math.Approximately(math.log(r1), Unity.Mathematics.math.log(r2)));
                // Assert.IsTrue(math.Approximately(math.log2(r1), Unity.Mathematics.math.log2(r2)));
                // Assert.IsTrue(math.Approximately(math.log10(r1), Unity.Mathematics.math.log10(r2)));
                Assert.IsTrue(math.Approximately(math.fmod(r1, r12), Unity.Mathematics.math.fmod(r2, r22)), $"{math.fmod(r1, r12)} {Unity.Mathematics.math.fmod(r2, r22)} {r1} {r12}");
                Assert.IsTrue(math.Approximately(math.modf(r1, out _), Unity.Mathematics.math.modf(r2, out _)));
                Assert.IsTrue(math.Approximately(math.sqrt(r17), Unity.Mathematics.math.sqrt(r27)));
                Assert.IsTrue(math.Approximately(math.rsqrt(r17), Unity.Mathematics.math.rsqrt(r27)));
                // Assert.IsTrue(math.Approximately(math.normalize(r1), Unity.Mathematics.math.normalize(r2)));


                // Assert.IsTrue(math.Approximately(math.smoothstep(r1, r12, lerp1), Unity.Mathematics.math.smoothstep(r2, r22, lerp2)), $"{math.smoothstep(r1, r12, lerp1)} {Unity.Mathematics.math.smoothstep(r2, r22, lerp2)}, {r1} {r12} {lerp1}");

                Assert.IsTrue(math.Approximately(math.radians(r1), Unity.Mathematics.math.radians(r2)));
                Assert.IsTrue(math.Approximately(math.degrees(r1), Unity.Mathematics.math.degrees(r2)));
  

                // normalize
                // length
                // lengthsq
                // distance
                // distancesq
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

        [Test]
        public void TestSinCos2()
        {
            TestRotation.InitLookupTable();

            for(int i = 0; i < 1; i++)
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

                math.sincos(r1, out var s, out var c);
                Unity.Mathematics.math.sincos(r2, out var s1, out var c1);

                // Assert.IsTrue(math.Approximately(math.tan(r1), Unity.Mathematics.math.tan(r2)), $"{math.tan(r1)} {Unity.Mathematics.math.tan(r2)} {r1} {r2}");
                // Assert.IsTrue(math.Approximately(math.tanh(r1), Unity.Mathematics.math.tanh(r2)));
                Assert.IsTrue(math.Approximately(math.atan(r1), Unity.Mathematics.math.atan(r2)));
                Assert.IsTrue(math.Approximately(math.atan2(r1, r12), Unity.Mathematics.math.atan2(r2, r22)));
                // Assert.IsTrue(math.Approximately(math.cosh(r1), Unity.Mathematics.math.cosh(r2)));
                Assert.IsTrue(math.Approximately(math.cos(r1), Unity.Mathematics.math.cos(r2)));
                Assert.IsTrue(math.Approximately(math.acos(r15), Unity.Mathematics.math.acos(r25)), $"{math.acos(r15)} {Unity.Mathematics.math.acos(r25)} {r15} {r25}");
                // Assert.IsTrue(math.Approximately(math.sinh(r1), Unity.Mathematics.math.sinh(r2)));
                Assert.IsTrue(math.Approximately(math.sin(r1), Unity.Mathematics.math.sin(r2)));
                Assert.IsTrue(math.Approximately(math.asin(r15), Unity.Mathematics.math.asin(r25)));

                Assert.IsTrue(math.Approximately(s, s1));
                Assert.IsTrue(math.Approximately(c, c1));

                Assert.IsTrue(math.Approximately(math.max(r1, r12), Unity.Mathematics.math.max(r2, r22)));
                Assert.IsTrue(math.Approximately(math.min(r1, r12), Unity.Mathematics.math.min(r2, r22)));

                Assert.IsTrue(math.Approximately(math.lerp(r1, r12, lerp1), Unity.Mathematics.math.lerp(r2, r22, lerp2)), $"{math.lerp(r1, r12, lerp1)} {Unity.Mathematics.math.lerp(r2, r22, lerp2)} {r1} {r12} {lerp1}");
                Assert.IsTrue(math.Approximately(math.unlerp(r1, r12, lerp1), Unity.Mathematics.math.unlerp(r2, r22, lerp2)));
                Assert.IsTrue(math.Approximately(math.remap(r1, r12, r13, r14, lerp1), Unity.Mathematics.math.remap(r2, r22, r23, r24, lerp2)));
                Assert.IsTrue(math.Approximately(math.mad(r1, r12, lerp1), Unity.Mathematics.math.mad(r2, r22, lerp2)));
                Assert.IsTrue(math.Approximately(math.clamp(r1, r12, lerp1), Unity.Mathematics.math.clamp(r2, r22, lerp2)));
                Assert.IsTrue(math.Approximately(math.saturate(r1), Unity.Mathematics.math.saturate(r2)));
                Assert.IsTrue(math.Approximately(math.abs(r1), Unity.Mathematics.math.abs(r2)));
                Assert.IsTrue(math.Approximately(math.dot(r1, r12), Unity.Mathematics.math.dot(r2, r22)));


                Assert.IsTrue(math.Approximately(math.floor(r1), Unity.Mathematics.math.floor(r2)));
                Assert.IsTrue(math.Approximately(math.ceil(r1), Unity.Mathematics.math.ceil(r2)));
                Assert.IsTrue(math.Approximately(math.round(r1), Unity.Mathematics.math.round(r2)));
                Assert.IsTrue(math.Approximately(math.trunc(r1), Unity.Mathematics.math.trunc(r2)));
                Assert.IsTrue(math.Approximately(math.frac(r1), Unity.Mathematics.math.frac(r2)));
                Assert.IsTrue(math.Approximately(math.rcp(r18), Unity.Mathematics.math.rcp(r28)), $"{math.rcp(r1)} {Unity.Mathematics.math.rcp(r28)} {r18}");
                Assert.IsTrue(math.Approximately(math.sign(r1), Unity.Mathematics.math.sign(r2)));
                // Assert.IsTrue(math.Approximately(math.pow(r1, r16), Unity.Mathematics.math.pow(r2, r26)), $"{math.pow(r1, r16)} {Unity.Mathematics.math.pow(r2, r26)}, {r1} {r16}");
                // Assert.IsTrue(math.Approximately(math.exp(r1), Unity.Mathematics.math.exp(r2)));
                // Assert.IsTrue(math.Approximately(math.exp2(r1), Unity.Mathematics.math.exp2(r2)));
                // Assert.IsTrue(math.Approximately(math.exp10(r1), Unity.Mathematics.math.exp10(r2)));
                // Assert.IsTrue(math.Approximately(math.log(r1), Unity.Mathematics.math.log(r2)));
                // Assert.IsTrue(math.Approximately(math.log2(r1), Unity.Mathematics.math.log2(r2)));
                // Assert.IsTrue(math.Approximately(math.log10(r1), Unity.Mathematics.math.log10(r2)));
                Assert.IsTrue(math.Approximately(math.fmod(r1, r12), Unity.Mathematics.math.fmod(r2, r22)), $"{math.fmod(r1, r12)} {Unity.Mathematics.math.fmod(r2, r22)} {r1} {r12}");
                Assert.IsTrue(math.Approximately(math.modf(r1, out _), Unity.Mathematics.math.modf(r2, out _)));
                Assert.IsTrue(math.Approximately(math.sqrt(r17), Unity.Mathematics.math.sqrt(r27)));
                Assert.IsTrue(math.Approximately(math.rsqrt(r17), Unity.Mathematics.math.rsqrt(r27)));
                // Assert.IsTrue(math.Approximately(math.normalize(r1), Unity.Mathematics.math.normalize(r2)));


                // Assert.IsTrue(math.Approximately(math.smoothstep(r1, r12, lerp1), Unity.Mathematics.math.smoothstep(r2, r22, lerp2)), $"{math.smoothstep(r1, r12, lerp1)} {Unity.Mathematics.math.smoothstep(r2, r22, lerp2)}, {r1} {r12} {lerp1}");

                Assert.IsTrue(math.Approximately(math.radians(r1), Unity.Mathematics.math.radians(r2)));
                Assert.IsTrue(math.Approximately(math.degrees(r1), Unity.Mathematics.math.degrees(r2)));
  
                Assert.IsTrue(math.Approximately(math.normalize(r1), Unity.Mathematics.math.normalize(r2)));
                Assert.IsTrue(math.Approximately(math.length(r1), Unity.Mathematics.math.length(r2)));
                Assert.IsTrue(math.Approximately(math.lengthsq(r1), Unity.Mathematics.math.lengthsq(r2)));
                Assert.IsTrue(math.Approximately(math.distance(r1, r12), Unity.Mathematics.math.distance(r2, r22)));
                Assert.IsTrue(math.Approximately(math.distancesq(r1, r12), Unity.Mathematics.math.distancesq(r2, r22)));

                Assert.IsTrue(math.Approximately(math.cmin(r1), Unity.Mathematics.math.cmin(r2)));
                Assert.IsTrue(math.Approximately(math.cmax(r1), Unity.Mathematics.math.cmax(r2)));
                Assert.IsTrue(math.Approximately(math.csum(r1), Unity.Mathematics.math.csum(r2)));
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
    }
}
