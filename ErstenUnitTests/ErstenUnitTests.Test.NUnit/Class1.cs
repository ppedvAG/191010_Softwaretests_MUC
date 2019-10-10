using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErstenUnitTests.Test.NUnit
{
    // NuGet: NUnit + NUnit3TestAdapter
    [TestFixture]
    public class TaschenrechnerNUnitTests
    {
        [Test]
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

        [Test]
        public void Add_Int32MAX_and_1_throws_OverflowException()
        {
            // Arrange:
            Taschenrechner tr = new Taschenrechner();

            // Act:
            Assert.Throws<OverflowException>(() =>
            {
                tr.Add(Int32.MaxValue, 1);
            });
        }

        [Test]
        public void Add_Int32MIN_and_N1_throws_OverflowException()
        {
            // Arrange:
            Taschenrechner tr = new Taschenrechner();

            // Act:
            Assert.Throws<OverflowException>(() =>
            {
                tr.Add(Int32.MinValue, -1);
            });
        }

        [Test]
        [TestCase(5, 3, 8)]
        [TestCase(100, 200, 300)]
        [TestCase(200, 100, 300)]
        [TestCase(5, -3, 2)]
        [TestCase(-5, 3, -2)]
        public void Add_returns_expectedValue(int z1, int z2, int expectedResult)
        {
            Taschenrechner tr = new Taschenrechner();
            var result = tr.Add(z1, z2);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
