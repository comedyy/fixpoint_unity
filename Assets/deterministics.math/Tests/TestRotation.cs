using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using FixedPoint;
using Random = UnityEngine.Random;
using System;

namespace Tests
{
    public class TestRotation
    {
        static bool _isLoaded = false;
        public static void InitLookupTable()
        {
        }
        // A Test behaves as an ordinary method
        [Test]
        public void TestLookRotation()
        {
            InitLookupTable();
            for(int i = 0; i < 100000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var f2 = Random.insideUnitSphere.normalized;

                if(Unity.Mathematics.math.dot(f1, f2) > 0.95f || Unity.Mathematics.math.dot(f1, f2) < 0.05f) 
                {
                    i--;
                    continue;
                }

                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                fp3 f12 = new fp3(fp.ConvertFrom(f2.x), fp.ConvertFrom(f2.y), fp.ConvertFrom(f2.z));

                var q1 = fpQuaternion.LookRotation(f11, f12);
                var q2 = Unity.Mathematics.quaternion.LookRotation(f11, f12);

                var angle = fpMath.angle(q1, new fpQuaternion(fp.ConvertFrom(q2.value.x),  fp.ConvertFrom(q2.value.y), fp.ConvertFrom(q2.value.z), fp.ConvertFrom(q2.value.w)));

                Assert.IsTrue(angle < 0.01f, $"{q1} {q2} {f1.ToString("F5")} {f2.ToString("F5")} {angle}" );
            }
        }

         [Test]
        public void TestAngle()
        {
            InitLookupTable();
            for(int i = 0; i < 100000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var f2 = Random.insideUnitSphere.normalized;

                if(Unity.Mathematics.math.dot(f1, f2) > 0.95f || Unity.Mathematics.math.dot(f1, f2) < 0.05f) 
                {
                    i--;
                    continue;
                }

                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                fp3 f12 = new fp3(fp.ConvertFrom(f2.x), fp.ConvertFrom(f2.y), fp.ConvertFrom(f2.z));

                var q1 = fpQuaternion.LookRotation(f11, f12);
                var q2 = Unity.Mathematics.quaternion.LookRotation(f11, f12);

                var angle = fpMath.angle(q1, new fpQuaternion(fp.ConvertFrom(q2.value.x),  fp.ConvertFrom(q2.value.y), fp.ConvertFrom(q2.value.z), fp.ConvertFrom(q2.value.w)));

                Assert.IsTrue(angle < 0.01f, $"{q1} {q2} {f1.ToString("F5")} {f2.ToString("F5")} {angle}" );
            }
        }


        [Test]
        public void TestEuler()
        {
            InitLookupTable();
            fpMath.Approximately(fpQuaternion.identity, fpQuaternion.identity);

            for(int i = 0; i < 10000; i++)
            {
                var d1 = Random.Range(-720, 720);
                var d2 = Random.Range(-720, 720);
                var d3 = Random.Range(-720, 720);

                var fix3 = new fp3(d1, d2, d3);
                var float3 = new Unity.Mathematics.float3(d1, d2, d3);

                for(RotationOrder r = RotationOrder.XYZ; r < RotationOrder.ZYX; r++)
                {
                    var q1 = fpQuaternion.Euler(fix3, r);
                    var q2 = Unity.Mathematics.quaternion.Euler(float3, (Unity.Mathematics.math.RotationOrder)r);
                    Assert.IsTrue(fpMath.Approximately(q1, q2), $"{q1} {q2}");
                }

                for(RotationOrder r = RotationOrder.XYZ; r < RotationOrder.ZYX; r++)
                {
                    var q1 = fpQuaternion.Euler(d1, d2, d3, r);
                    var q2 = Unity.Mathematics.quaternion.Euler(d1, d2, d3, (Unity.Mathematics.math.RotationOrder)r);
                    Assert.IsTrue(fpMath.Approximately(q1, q2), $"{q1} {q2}");
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
                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));

                var d3 = Random.Range(-720, 720);

                var q1 = fpQuaternion.AxisAngle(f11, d3);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);

                Assert.IsTrue(fpMath.Approximately(q1, q2), $"{q1} {q2}");
            }
        }

        [Test]
        public void TestDot()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                var f2 = Random.insideUnitSphere.normalized;
                fp3 f12 = new fp3(fp.ConvertFrom(f2.x), fp.ConvertFrom(f2.y), fp.ConvertFrom(f2.z));

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q1 = fpQuaternion.AxisAngle(f11, d3);
                var q11 = fpQuaternion.AxisAngle(f12, d4);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);
                var q22 = Unity.Mathematics.quaternion.AxisAngle(f2, d4);

                var q111 = fpMath.dot(q11, q1);
                var q222 = Unity.Mathematics.math.dot(q22, q2);

                Assert.IsTrue(fpMath.Approximately(q222, q111), $"{q111} {q222}");
            }
        }

        [Test]
        public void TestInverse()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q1 = fpQuaternion.AxisAngle(f11, d3);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);

                var q111 = fpMath.inverse(q1);
                var q222 = Unity.Mathematics.math.inverse(q2);

                Assert.IsTrue(fpMath.Approximately(q111, q222), $"{q111} {q222}");
            }
        }
        

        [Test]
        public void TestNormalize()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));

                var d3 = Random.Range(-720, 720);
                var d4 = Random.Range(-720, 720);

                var q1 = fpQuaternion.AxisAngle(f11, d3);
                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);

                var q111 = fpMath.normalize(q1);
                var q222 = Unity.Mathematics.math.normalize(q2);

                Assert.IsTrue(fpMath.Approximately(q111, q222), $"{q111} {q222}");
            }
        }

        [Test]
        public void TestRotate()
        {
            InitLookupTable();

            for(int i = 0; i < 10000; i++)
            {
                var d3 = Random.Range(-720, 720);

                var q1 = fpQuaternion.RotateX(d3);
                var q2 = Unity.Mathematics.quaternion.RotateX(d3);
                Assert.IsTrue(fpMath.Approximately(q1, q2), $"{q1} {q2}");

                var q11 = fpQuaternion.RotateY(d3);
                var q21 = Unity.Mathematics.quaternion.RotateY(d3);
                Assert.IsTrue(fpMath.Approximately(q11, q21), $"{q11} {q21}");

                var q12 = fpQuaternion.RotateZ(d3);
                var q22 = Unity.Mathematics.quaternion.RotateZ(d3);
                Assert.IsTrue(fpMath.Approximately(q12, q22), $"{q12} {q22}");

                var qqq1 = fpMath.mul(q12, fpMath.mul(q1, q11));
                var qqq2 = Unity.Mathematics.math.mul(q12, Unity.Mathematics.math.mul(q1, q11));

                var f1 = Random.insideUnitSphere.normalized;
                fp3 f11 = new fp3(fp.ConvertFrom(f1.x), fp.ConvertFrom(f1.y), fp.ConvertFrom(f1.z));
                var qq = fpMath.rotate(qqq1, f11);
                var qq1 = Unity.Mathematics.math.rotate(qqq2, f1);

                Assert.IsTrue(fpMath.Approximately(qq, qq1), $"{qq} {qq1}");
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

                var q1 = new fpQuaternion(fp.ConvertFrom(q2.value.x), fp.ConvertFrom(q2.value.y), fp.ConvertFrom(q2.value.z), fp.ConvertFrom(q2.value.w));
                var q11 = new fpQuaternion(fp.ConvertFrom(q22.value.x), fp.ConvertFrom(q22.value.y), fp.ConvertFrom(q22.value.z), fp.ConvertFrom(q22.value.w));

                var d5 = Random.Range(0.0f, 1.0f);
                var q111 = fpMath.slerp(q11, q1, fp.ConvertFrom(d5));
                var q222 = Unity.Mathematics.math.slerp(q22, q2, d5);

                if(fpMath.Approximately(q111, q222))
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

            HashSet<fpQuaternion> _allQuaternion = new HashSet<fpQuaternion>();
            HashSet<int> _hashCodes = new HashSet<int>();
            for(int i = 0; i < 100000; i++)
            {
                var f1 = Random.insideUnitSphere.normalized;
                var d3 = Random.Range(-720, 720);

                var q2 = Unity.Mathematics.quaternion.AxisAngle(f1, d3);
                var q1 = new fpQuaternion(fp.ConvertFrom(q2.value.x), fp.ConvertFrom(q2.value.y), fp.ConvertFrom(q2.value.z), fp.ConvertFrom(q2.value.w));

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
