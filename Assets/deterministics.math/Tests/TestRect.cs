using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;
using Mathematics.FixedPoint;
using Random = UnityEngine.Random;

namespace Tests
{
    public class TestRect
    {
        [Test]
        public void TestRect1()
        {
            TestRotation.InitLookupTable();

            fpRect rect = new fpRect(1, 2, 3, 4);

            Assert.AreEqual(rect.min, new fp2(1, 2));
            Assert.AreEqual(rect.max, new fp2(4, 6));
            Assert.AreEqual(rect.xMin, new fp(1));
            Assert.AreEqual(rect.yMin, new fp(2));
            Assert.AreEqual(rect.xMax, new fp(4));
            Assert.AreEqual(rect.yMax, new fp(6));
            Assert.IsTrue(fpMath.Approximately(rect.center, new fp2(fp.ConvertFrom(2.5f), 4)));
            Assert.AreEqual(rect.position, rect.min);
            Assert.AreEqual(rect.size, new fp2(3, 4));
            Assert.AreEqual(rect, fpRect.MinMaxRect(1, 2, 4, 6));
            Assert.IsTrue(fpMath.Approximately(rect.center, fpRect.NormalizedToPoint(rect, new fp2(fp.ConvertFrom(0.5f), fp.ConvertFrom(0.5f)))));
            Assert.IsTrue(fpMath.Approximately(new fp2(fp.ConvertFrom(0.5f), fp.ConvertFrom(0.5f)), fpRect.PointToNormalized(rect, rect.center)));

            Assert.IsTrue(rect.Contains(new fp2(2, 3)));
            Assert.IsTrue(!rect.Contains(new fp2(4, 6)));

            Assert.IsTrue(rect.Overlaps(new fpRect(0, 0, 3, 3)));
            Assert.IsTrue(!rect.Overlaps(new fpRect(-3, -3, 0, 0)));
        }
    }
}
