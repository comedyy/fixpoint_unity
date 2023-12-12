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
    public class TestRotation
    {
        public static void InitLookupTable()
        {
            NumberLut.Init((path)=>{
                return Resources.Load<TextAsset>(Path.Combine("NTLut", path)).bytes;
            });
        }
        // A Test behaves as an ordinary method
        [Test]
        public void TestLookRotation()
        {
            InitLookupTable();
            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var f2 = Random.insideUnitSphere.normalized;
                float3 f11 = new float3(number.ConvertFrom(f1.x), number.ConvertFrom(f1.y), number.ConvertFrom(f1.z));
                float3 f12 = new float3(number.ConvertFrom(f2.x), number.ConvertFrom(f2.y), number.ConvertFrom(f2.z));

                var q1 = quaternion.LookRotation(f11, f12);
                var q2 = Unity.Mathematics.quaternion.LookRotation(f1, f2);

                Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");
            }
        }

        [Test]
        public void TestNormal()
        {
            InitLookupTable();
            math.Approximately(quaternion.identity, quaternion.identity);

            for(int i = 0; i < 1000; i++)
            {
                var d1 = Random.Range(0, 720);
                var d2 = Random.Range(0, 720);
                var d3 = Random.Range(0, 720);

                var fix3 = new float3(d1, d2, d3);
                var float3 = new Unity.Mathematics.float3(d1, d2, d3);

                var q1 = quaternion.Euler(fix3);
                var q2 = Unity.Mathematics.quaternion.Euler(float3);

                Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");
            }
        }
    }
}
