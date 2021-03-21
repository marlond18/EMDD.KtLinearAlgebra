using EMDD.KtNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void EqualityTest()
        {
            Number n = 0;
            Assert.IsTrue(n == 0);
        }
    }
}
