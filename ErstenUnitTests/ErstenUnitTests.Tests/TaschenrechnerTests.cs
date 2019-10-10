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
    }
}
