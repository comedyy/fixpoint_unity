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
                float3 f11 = new float3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                float3 f12 = new float3(fp.ConvertFrom(f2.x), fp.ConvertFrom(f2.y), fp.ConvertFrom(f2.z));

                var q1 = quaternion.LookRotation(f11, f12);
                var q2 = Unity.Mathematics.quaternion.LookRotation(f1, f2);

                Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");
            }
        }

        [Test]
        public void TestEuler()
        {
            InitLookupTable();
            math.Approximately(quaternion.identity, quaternion.identity);

            for(int i = 0; i < 10000; i++)
            {
                var d1 = Random.Range(-720, 720);
                var d2 = Random.Range(-720, 720);
                var d3 = Random.Range(-720, 720);

                var fix3 = new float3(d1, d2, d3);
                var float3 = new Unity.Mathematics.float3(d1, d2, d3);

                for(RotationOrder r = RotationOrder.XYZ; r < RotationOrder.ZYX; r++)
                {
                    var q1 = quaternion.Euler(fix3, r);
                    var q2 = Unity.Mathematics.quaternion.Euler(float3, (Unity.Mathematics.math.RotationOrder)r);
                    Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");
                }

                for(RotationOrder r = RotationOrder.XYZ; r < RotationOrder.ZYX; r++)
                {
                    var q1 = quaternion.Euler(d1, d2, d3, r);
                    var q2 = Unity.Mathematics.quaternion.Euler(d1, d2, d3, (Unity.Mathematics.math.RotationOrder)r);
                    Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");
                }
            }
        }

        [Test]
        public void Test    ()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                float3 f11 = new float3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));

                var d3 = Random.Range(-720, 720);

                var q1 = quaternion.AxisAngle(f11, d3);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);

                Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");
            }
        }

        [Test]
        public void TestDot()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                float3 f11 = new float3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                var f2 = Random.insideUnitSphere.normalized;
                float3 f12 = new float3(fp.ConvertFrom(f2.x), fp.ConvertFrom(f2.y), fp.ConvertFrom(f2.z));

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q1 = quaternion.AxisAngle(f11, d3);
                var q11 = quaternion.AxisAngle(f12, d4);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);
                var q22 = Unity.Mathematics.quaternion.AxisAngle(f2, d4);

                var q111 = math.dot(q11, q1);
                var q222 = Unity.Mathematics.math.dot(q22, q2);

                Assert.IsTrue(math.Approximately(q222, q111), $"{q111} {q222}");
            }
        }

        [Test]
        public void TestInverse()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                float3 f11 = new float3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q1 = quaternion.AxisAngle(f11, d3);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);

                var q111 = math.inverse(q1);
                var q222 = Unity.Mathematics.math.inverse(q2);

                Assert.IsTrue(math.Approximately(q111, q222), $"{q111} {q222}");
            }
        }
        

        [Test]
        public void TestNormalize()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                float3 f11 = new float3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q1 = quaternion.AxisAngle(f11, d3);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);

                var q111 = math.normalize(q1);
                var q222 = Unity.Mathematics.math.normalize(q2);

                Assert.IsTrue(math.Approximately(q111, q222), $"{q111} {q222}");
            }
        }

        [Test]
        public void TestRotate()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var d3 = Random.Range(-720, 720);

                var q1 = quaternion.RotateX(d3);
                var q2 = Unity.Mathematics.quaternion.RotateX(d3);
                Assert.IsTrue(math.Approximately(q1, q2), $"{q1} {q2}");

                var q11 = quaternion.RotateY(d3);
                var q21 = Unity.Mathematics.quaternion.RotateY(d3);
                Assert.IsTrue(math.Approximately(q11, q21), $"{q11} {q21}");

                var q12 = quaternion.RotateZ(d3);
                var q22 = Unity.Mathematics.quaternion.RotateZ(d3);
                Assert.IsTrue(math.Approximately(q12, q22), $"{q12} {q22}");

                var qqq1 = math.mul(q12, math.mul(q1, q11));
                var qqq2 = Unity.Mathematics.math.mul(q12, Unity.Mathematics.math.mul(q1, q11));

                var f1 = Random.insideUnitSphere.normalized;
                float3 f11 = new float3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                var qq = math.rotate(qqq1, f11);
                var qq1 = Unity.Mathematics.math.rotate(qqq2, f1);

                Assert.IsTrue(math.Approximately(qq, qq1), $"{qq} {qq1}");
            }
        }

        [Test]
        public void TestLerp()
        {
            InitLookupTable();

            var passCount = 0;
            for(int i = 0; i < 100000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var f2 = Random.insideUnitSphere.normalized;

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);
                var q22 = Unity.Mathematics.quaternion.AxisAngle(f2, d4);

                var q1 = new quaternion(fp.ConvertFrom(q2.value.x), fp.ConvertFrom(q2.value.y), fp.ConvertFrom(q2.value.z), fp.ConvertFrom(q2.value.w));
                var q11 = new quaternion(fp.ConvertFrom(q22.value.x), fp.ConvertFrom(q22.value.y), fp.ConvertFrom(q22.value.z), fp.ConvertFrom(q22.value.w));

                var d5 = Random.Range(0.0f, 1.0f);
                var q111 = math.slerp(q11, q1, fp.ConvertFrom(d5));
                var q222 = Unity.Mathematics.math.slerp(q22, q2, d5);

                if(math.Approximately(q111, q222))
                {
                    passCount++;
                }                
            }

            Assert.IsTrue(passCount > 70000, $"{passCount}");
        }

        [Test]
        public void TestGetHashCode()
        {
            InitLookupTable();
            int totalCollide = 0;

            HashSet<quaternion> _allQuaternion = new HashSet<quaternion>();
            HashSet<int> _hashCodes = new HashSet<int>();
            for(int i = 0; i < 100000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var d3 = Random.Range(-720, 720);

                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);
                var q1 = new quaternion(fp.ConvertFrom(q2.value.x), fp.ConvertFrom(q2.value.y), fp.ConvertFrom(q2.value.z), fp.ConvertFrom(q2.value.w));

                if(_allQuaternion.Contains(q1))
                {
                    i--;
                    continue;
                }

                var hashCode = q1.GetHashCode();

                if(_hashCodes.Contains(hashCode))
                {
                    totalCollide ++;
                }                
                else
                {
                    _hashCodes.Add(hashCode);
                    _allQuaternion.Add(q1);
                }
            }

            if(totalCollide > 100)
            {
                Assert.IsTrue(false, $"{totalCollide}");
            }
        }
    }
}
