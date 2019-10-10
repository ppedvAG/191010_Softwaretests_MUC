using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErstenUnitTests.Tests
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void Equals_With_Null_Returns_False()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };

            Assert.IsFalse(p1.Equals(null));
        }

        [TestMethod]
        public void Equals_With_WrongDataType_Returns_False()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };

            Assert.IsFalse(p1.Equals(new StringBuilder()));
            // Assert.IsFalse(p1.Equals(123));
            // Assert.IsFalse(p1.Equals("abcde"));
        }

        [TestMethod]
        public void Equals_With_same_Reference_Returns_True()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            Person p2 = p1; // Referenzkopie

            Assert.IsTrue(p1.Equals(p2));
        }

        [TestMethod]
        public void Equals_With_same_Values_Returns_True()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            Person p2 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };

            Assert.IsTrue(p1.Equals(p2));
        }

        [TestMethod]
        public void Equals_With_different_Values_Returns_False()
        {
            Person p1 = new Person { Vorname = "Tom", Nachname = "Ate", Alter = 10, Kontostand = 100 };
            Person p2 = new Person { Vorname = "Anna", Nachname = "Nass", Alter = 10, Kontostand = 100 };

            Assert.IsFalse(p1.Equals(p2));
        }
    }
}
