using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ppedv.ThirstyPerson.Domain;
using ppedv.ThirstyPerson.Domain.Interfaces;
// using Robotech.Hardware;

namespace ppedv.ThirstyPerson.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        // Ich will testen, ob am ende auch wirklich 5 Personen in der Liste rauskommen
        // Problem 1: null (Maschine ist noch nicht im Büro)
        #region Variante ohne Mock
        //[TestMethod]
        //public void RecruitManyPersonsForCompany_can_create_5_Persons_nullError()
        //{
        //    Core core = new Core(null); // Problem: Gerät ist nicht da !!!

        //    IEnumerable<Person> generatedPersons = core.RecruitManyPersonsForCompany(5);
        //    generatedPersons.Should().HaveCount(5);
        //}

        //[TestMethod]
        //public void RecruitManyPersonsForCompany_can_create_5_Persons_with_real_Device()
        //{
        //    Core core = new Core(new XINGRecruiter3000()); // Problem: Gerät ist nicht da !!!

        //    IEnumerable<Person> generatedPersons = core.RecruitManyPersonsForCompany(5);
        //    generatedPersons.Should().HaveCount(5);
        //} 
        #endregion

        // UnitTest: Teste die Funktionalität der Methode
        [TestMethod]
        public void RecruitManyPersonsForCompany_can_recruit_5_persons()
        {
            Mock<IDevice> dummy = new Mock<IDevice>(); // Erstellt ein Fake für IDevice
            // Konfiguration:
            dummy.Setup(x => x.RecruitPerson())
                 .Returns(() => new Person { FirstName = "Tom", LastName = "Ate" });

            Core core = new Core(dummy.Object); // Core nutzt intern das Fake-Objekt

            var persons = core.RecruitManyPersonsForCompany(5);

            persons.Should().HaveCount(5);

            dummy.Verify(x => x.RecruitPerson(), Times.Exactly(5));
        }

        [TestMethod]
        public void RecruitManyPersonsForCompany_with_invalid_argument_throws_ArgumentException()
        {
            Mock<IDevice> dummy = new Mock<IDevice>(); // Erstellt ein Fake für IDevice

            Core core = new Core(dummy.Object); // Core nutzt intern das Fake-Objekt


            core.Invoking(x => core.RecruitManyPersonsForCompany(-5))
                .Should().Throw<ArgumentException>();
            

            dummy.Verify(x => x.RecruitPerson(), Times.Never);
        }
    }
}
