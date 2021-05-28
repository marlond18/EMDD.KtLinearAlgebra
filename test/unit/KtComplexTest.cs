using EMDD.KtNumerics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;

namespace KtNumericsTest
{
    [TestClass]
    public class NumberTest
    {
        [TestMethod]
        public void TostringTest()
        {
            Number b = (1, 2);
            Assert.AreEqual(b.ToString(), "1+2i");
            b = (1.0 / 3, 2);
            Assert.AreEqual(b.ToString(), "0.333+2i");
        }

        [DataTestMethod]
        [DynamicData(nameof(Numbers), DynamicDataSourceType.Method)]
        public void CastDoublesToNumbersTest(double number)
        {
            Number n = number;
            Assert.IsTrue(n == number);
        }

        [DataTestMethod]
        [DynamicData(nameof(RandomPairOfNumbers), DynamicDataSourceType.Method)]
        public void AddRandomNumbersTest(double v1, double v2)
        {
            var expected = v1 + v2;
            var n1 = new KtRealNumber(v1);
            var n2 = new KtRealNumber(v2);
            var actual = n1 + n2;
            Assert.IsTrue(actual == expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(RandomPairOfNumbers), DynamicDataSourceType.Method)]
        public void SubtractRandomNumbersTest(double v1, double v2)
        {
            var expected = v1 - v2;
            var n1 = new KtRealNumber(v1);
            var n2 = new KtRealNumber(v2);
            var actual = n1 - n2;
            Assert.IsTrue(actual == expected);
        }

        private static IEnumerable<object[]> Numbers()
        {
            yield return new object[] { 0 };
            yield return new object[] { 1 };
            yield return new object[] { 3.4 };
            yield return new object[] { 4.6 };
        }

        private static IEnumerable<object[]> RandomPairOfNumbers()
        {
            var rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                var v1 = rand.NextDouble();
                var v2 = rand.NextDouble();
                yield return new object[] { v1, v2 };
            }
        }
    }
}
