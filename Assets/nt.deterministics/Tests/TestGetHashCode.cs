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
    public class TestGetHashCode
    {
        [Test]
        public void TestSinCos1()
        {
            TestRotation.InitLookupTable();

            int totalCollide = 0;

            HashSet<fp2> _allQuaternion = new HashSet<fp2>();
            HashSet<int> _hashCodes = new HashSet<int>();
            for(int i = 0; i < 100000; i++)
            {
                (var x, var xx) = TestMath.GetRandom2(-9999f, 99999f);

                if(_allQuaternion.Contains(x))
                {
                    i--;
                    continue;
                }

                var hashCode = x.GetHashCode();

                if(_hashCodes.Contains(hashCode))
                {
                    totalCollide ++;
                }                
                else
                {
                    _hashCodes.Add(hashCode);
                    _allQuaternion.Add(x);
                }
            }

            if(totalCollide > 100)
            {
                Assert.IsTrue(false, $"{totalCollide}");
            }
        }

        [Test]
        public void TestSinCos2()
        {
            TestRotation.InitLookupTable();

            int totalCollide = 0;

            HashSet<fp3> _allQuaternion = new HashSet<fp3>();
            HashSet<int> _hashCodes = new HashSet<int>();
            for(int i = 0; i < 100000; i++)
            {
                (var x, var xx) = TestMath.GetRandom3();

                if(_allQuaternion.Contains(x))
                {
                    i--;
                    continue;
                }

                var hashCode = x.GetHashCode();

                if(_hashCodes.Contains(hashCode))
                {
                    totalCollide ++;
                }                
                else
                {
                    _hashCodes.Add(hashCode);
                    _allQuaternion.Add(x);
                }
            }

            if(totalCollide > 100)
            {
                Assert.IsTrue(false, $"{totalCollide}");
            }
        }

        [Test]
        public void TestSinCos4()
        {
            TestRotation.InitLookupTable();

            int totalCollide = 0;

            HashSet<fp4> _allQuaternion = new HashSet<fp4>();
            HashSet<int> _hashCodes = new HashSet<int>();
            for(int i = 0; i < 100000; i++)
            {
                (var x, var xx) = TestMath.GetRandom4();

                if(_allQuaternion.Contains(x))
                {
                    i--;
                    continue;
                }

                var hashCode = x.GetHashCode();

                if(_hashCodes.Contains(hashCode))
                {
                    totalCollide ++;
                }                
                else
                {
                    _hashCodes.Add(hashCode);
                    _allQuaternion.Add(x);
                }
            }

            if(totalCollide > 100)
            {
                Assert.IsTrue(false, $"{totalCollide}");
            }
        }
    }
}
