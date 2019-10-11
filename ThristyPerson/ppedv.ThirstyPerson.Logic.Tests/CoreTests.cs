using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.ThirstyPerson.Domain;
using Robotech.Hardware;

namespace ppedv.ThirstyPerson.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        // Ich will testen, ob am ende auch wirklich 5 Personen in der Liste rauskommen
        // Problem 1: null (Maschine ist noch nicht im Büro)
        [TestMethod]
        public void RecruitManyPersonsForCompany_can_create_5_Persons_nullError()
        {
            Core core = new Core(null); // Problem: Gerät ist nicht da !!!

            IEnumerable<Person> generatedPersons = core.RecruitManyPersonsForCompany(5);
            generatedPersons.Should().HaveCount(5);
        }

        [TestMethod]
        public void RecruitManyPersonsForCompany_can_create_5_Persons_with_real_Device()
        {
            Core core = new Core(new XINGRecruiter3000()); // Problem: Gerät ist nicht da !!!

            IEnumerable<Person> generatedPersons = core.RecruitManyPersonsForCompany(5);
            generatedPersons.Should().HaveCount(5);
        }
    }
}
