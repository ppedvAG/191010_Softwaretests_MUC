using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErstenUnitTests.Tests
{
    [TestClass]
    public class TaschenrechnerTests
    {
        [TestMethod]
        public void Add_5_and_3_returns_8()
        {
            // Arrange:
            Taschenrechner tr = new Taschenrechner();

            // Act:
            var result = tr.Add(5, 3);

            // Assert:
            Assert.AreEqual(8, result);
        }

        // Testfälle:
        // Normalfall
        // Null-Fall
        // Extremfall


        // testm + TAB + TAB

        [TestMethod]
        public void Add_Int32MAX_and_1_throws_OverflowException()
        {
            // Arrange:
            Taschenrechner tr = new Taschenrechner();

            // Act:
            Assert.ThrowsException<OverflowException>(() =>
            {
               tr.Add(Int32.MaxValue, 1);
            });
        }

        [TestMethod]
        public void Add_Int32MIN_and_N1_throws_OverflowException()
        {
            // Arrange:
            Taschenrechner tr = new Taschenrechner();

            // Act:
            Assert.ThrowsException<OverflowException>(() =>
            {
                tr.Add(Int32.MinValue, -1);
            });
        }

        [TestMethod]
        [DataRow(5,3,8)]
        [DataRow(100,200,300)]
        [DataRow(200,100,300)]
        [DataRow(5,-3,2)]
        [DataRow(-5,3,-2)]
        public void Add_returns_expectedValue(int z1, int z2, int expectedResult)
        {
            Taschenrechner tr = new Taschenrechner();
            var result = tr.Add(z1, z2);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
