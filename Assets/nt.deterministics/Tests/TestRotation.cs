using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;

namespace Tests
{
    public class TestRotation
    {
        public static void InitLookupTable()
        {
            Nt.Deterministics.NumberLut.Init((path)=>{
                return Resources.Load<TextAsset>(Path.Combine("NTLut", path)).bytes;
            });
        }
        // A Test behaves as an ordinary method
        [Test]
        public void TestLookRotation()
        {
            InitLookupTable();
            for(int i = 0; i < 1000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var f2 = Random.insideUnitSphere.normalized;
                Nt.Deterministics.float3 f11 = new Nt.Deterministics.float3(Nt.Deterministics.number.ConvertFrom(f1.x), Nt.Deterministics.number.ConvertFrom(f1.y), Nt.Deterministics.number.ConvertFrom(f1.z));
                Nt.Deterministics.float3 f12 = new Nt.Deterministics.float3(Nt.Deterministics.number.ConvertFrom(f2.x), Nt.Deterministics.number.ConvertFrom(f2.y), Nt.Deterministics.number.ConvertFrom(f2.z));

                var q1 = Nt.Deterministics.quaternion.LookRotation(f11, f12);
                var q2 = Unity.Mathematics.quaternion.LookRotation(f1, f2);

                Assert.IsTrue(Nt.Deterministics.math.Approximately(q1, q2), $"{q1} {q2}");
                Debug.LogError(q1 + " " + q2);
            }
        }
    }
}
