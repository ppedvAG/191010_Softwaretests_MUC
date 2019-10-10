using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ErstenUnitTests.Test.XUnit
{
    // Nuget: XUnit und Xunit.runner.visualstudio

    public class TaschenrechnerXUnitTests
    {
        [Fact]
        public void Add_5_and_3_returns_8()
        {
            // Arrange:
            Taschenrechner tr = new Taschenrechner();

            // Act:
            var result = tr.Add(5, 3);

            // Assert:
            Assert.Equal(8, result);
        }

        // Testfälle:
        // Normalfall
        // Null-Fall
        // Extremfall


        // testm + TAB + TAB

        [Fact]
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

        [Fact]
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

        [Theory]
        [InlineData(5, 3, 8)]
        [InlineData(100, 200, 300)]
        [InlineData(200, 100, 300)]
        [InlineData(5, -3, 2)]
        [InlineData(-5, 3, -2)]
        public void Add_returns_expectedValue(int z1, int z2, int expectedResult)
        {
            Taschenrechner tr = new Taschenrechner();
            var result = tr.Add(z1, z2);
            Assert.Equal(expectedResult, result);
        }
    }
}
