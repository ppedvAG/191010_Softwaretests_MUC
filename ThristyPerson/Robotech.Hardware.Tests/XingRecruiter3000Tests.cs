using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robotech.Hardware.Tests
{
    [TestClass]
    public class XingRecruiter3000Tests
    {
        // Unittest: Ich teste ob die Maschine das macht, was sie soll
        [TestMethod]
        public void XingRecruiter3000_can_recruit_Person()
        {
            XINGRecruiter3000 r = new XINGRecruiter3000();

            var person = r.RecruitPerson();

            person.Should().NotBeNull();
        }
    }
}
