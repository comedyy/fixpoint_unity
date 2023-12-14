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
    public class TestRandom
    {
        [Test]
        public void TestSinCos1()
        {
            TestRotation.InitLookupTable();
            fpRandom fpRandom = new fpRandom(1);

            // nextFp = [0, 1)
            int count = 0;
            for(int i = 0; i < 100000; i++)
            {
                var randomFp = fpRandom.NextFp();
                Assert.IsTrue(randomFp < 1 && randomFp >= 0, randomFp.ToString());

                if(randomFp < fp.Create(0, 5000))
                {
                    count++;
                }
            }
            Assert.IsTrue(count > 40000 && count < 60000, count.ToString());
            
             count = 0;
            for(int i = 0; i < 100000; i++)
            {
                var randomFp = fpRandom.NextFp(1, 10);
                Assert.IsTrue(randomFp < 10 && randomFp >= 1);

                if(randomFp < fp.Create(5, 5000))
                {
                    count++;
                }
            }
            Assert.IsTrue(count > 40000 && count < 60000, count.ToString());

             count = 0;
            for(int i = 0; i < 100000; i++)
            {
                var randomFp = fpRandom.NextBool();
                if(randomFp)
                {
                    count++;
                }
            }
            Assert.IsTrue(count > 40000 && count < 60000, count.ToString());

             count = 0;
            for(int i = 0; i < 100000; i++)
            {
                var randomFp = fpRandom.NextInt(-999, 999);
                Assert.IsTrue(randomFp < 999 && randomFp >= -999);
                if(randomFp < 0)
                {
                    count++;
                }
            }
            Assert.IsTrue(count > 40000 && count < 60000);


            for(int i = 0; i < 100000; i++)
            {
                var randomFp = fpRandom.NextDirection2D();
                Assert.IsTrue(fpMath.Approximately(fpMath.length(randomFp), 1f));
            }

            for(int i = 0; i < 100000; i++)
            {
                var randomFp = fpRandom.NextDirection3D();
                Assert.IsTrue(fpMath.Approximately(fpMath.length(randomFp), 1f));
            }
        }
    }
}
