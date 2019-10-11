using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ppedv.ThirstyPerson.Data.EF;
using ppedv.ThirstyPerson.Domain;
using ppedv.ThirstyPerson.Domain.Interfaces;
using Robotech.Hardware;
// using Robotech.Hardware;

namespace ppedv.ThirstyPerson.Logic.Tests
{
    [TestClass]
    public class CoreTests
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ThirstyPerson_Test;Trusted_Connection=true;AttachDbFileName=C:\temp\ThirstyPerson.mdf";


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


        // DB-Tests
        // Unittest: Stelle sicher, dass "alle" Personen aus der DB kommen
        // ... ohne DB ... häää ????
        [TestMethod]
        public void GetAllPeople() // Hier wird der Befehl "repository.GetAll<T>" ausgeführt
        {
            Mock<IRepository> dummy = new Mock<IRepository>();
            // Variante 1: Selbst machen
            //dummy.Setup(x => x.GetAll<Person>())
            //     .Returns(() => new Person[]
            //     {
            //        new Person { FirstName = "Tom", LastName = "Ate" },
            //        new Person { FirstName = "Anna", LastName = "Nass" },
            //        new Person { FirstName = "Peter", LastName = "Silie" } 
            //     });

            // Variante 2: Autofixture
            int numberOfPersons = 1000;
            Fixture fix = new Fixture();
            Person[] output = fix.CreateMany<Person>(numberOfPersons).ToArray();

            dummy.Setup(x => x.GetAll<Person>())
                 .Returns(() => output);

            Core core = new Core(null, dummy.Object);

            var result = core.GetAllPeople();

            result.Count().Should().Be(numberOfPersons);
            dummy.Verify(x => x.GetAll<Person>(), Times.Exactly(1)); // Ich will dass der DB-Befehl genutzt wird und kein anderer Befehl
        }

        [TestMethod]
        public void GetPersonWithHighestBalance_returns_corrent_Person()
        {
            Person correctOutput = new Person { FirstName = "Anna", LastName = "Nass", Balance = 1000 };
            
            Mock<IRepository> dummy = new Mock<IRepository>();
            // Variante 1: Selbst machen
            dummy.Setup(x => x.GetAll<Person>())
                 .Returns(() => new Person[]
                 {
                    new Person { FirstName = "Tom", LastName = "Ate",Balance=500 },
                    correctOutput,
                    new Person { FirstName = "Peter", LastName = "Silie",Balance=-12345 }
                 });
            // Ergebnis ist bekannt: Person Nr 2

            Core core = new Core(null, dummy.Object);
            var result = core.GetPersonWithHighestBalance();

            result.Should().BeEquivalentTo(correctOutput);
        }

        // UnitTest: Testet ob die Recruit-Logik richtig intern ausgeführt wird
        [TestMethod]
        public void RecruitPersonsAndSaveIntoDB_can_recruit_100_Persons_and_save_all_into_DB()
        {
            Mock<IDevice> hwMock = new Mock<IDevice>();
            Mock<IRepository> repoMock = new Mock<IRepository>();

            Core core = new Core(hwMock.Object, repoMock.Object);
            int numberOfPersons = 100;
            core.RecruitPersonsAndSaveIntoDB(numberOfPersons);
            // Verifiziere dass 100 mal der .Add-Befehl ausgeführt wurde

            hwMock.Verify(x => x.RecruitPerson(), Times.Exactly(numberOfPersons));
            repoMock.Verify(x => x.Add<Person>(It.IsAny<Person>()), Times.Exactly(numberOfPersons));
        }

        // Integrationstest für die DB: 
        [TestMethod]
        public void RecruitPersonsAndSaveIntoDB_can_recruit_5_Persons_and_save_all_into_EF_Database()
        {
            Fixture fix = new Fixture();
            var persons = fix.CreateMany<Person>(5);

            Mock<IDevice> hwMock = new Mock<IDevice>();
            IRepository repo = new EFRepository(new EFContext(connectionString)); // Echte DB
            hwMock.SetupSequence(x => x.RecruitPerson())
                  .Returns(persons.ElementAt(0))
                  .Returns(persons.ElementAt(1))
                  .Returns(persons.ElementAt(2))
                  .Returns(persons.ElementAt(3))
                  .Returns(persons.ElementAt(4));

            Core core = new Core(hwMock.Object, repo);
            int numberOfPersons = 5;
            core.RecruitPersonsAndSaveIntoDB(numberOfPersons);

            // Test, ob auch alle in der DB sind
            repo.GetAll<Person>().Count().Should().Be(numberOfPersons); //  -> muss 100 sein
        }

        // Integrationstest für die Hardware: 
        [TestMethod]
        public void RecruitPersonsAndSaveIntoDB_can_recruit_5_Persons_with_Hardware_and_save_all_into_Database()
        {
            Mock<IRepository> repoMock = new Mock<IRepository>();
            IDevice device = new XINGRecruiter3000(); // Echte Hardware
           
            Core core = new Core(device, repoMock.Object);
            int numberOfPersons = 5;
            core.RecruitPersonsAndSaveIntoDB(numberOfPersons);

            // Test, ob die "real von der Maschine" erzeugten Objekte "gespeichert" werde
            repoMock.Verify(x => x.Add<Person>(It.IsAny<Person>()), Times.Exactly(5));
            // Verifiziere dass gespeichert wird
            repoMock.Verify(x => x.Save(), Times.Once());
        }

        // Akzeptanztest für gesamten Ablauf
        [TestMethod]
        public void RecruitPersonsAndSaveIntoDB_can_recruit_5_Persons_FOR_REAL()
        {
            IRepository repository = new EFRepository(new EFContext(connectionString));
            IDevice device = new XINGRecruiter3000(); // Echte Hardware

            Core core = new Core(device, repository);
            int numberOfPersons = 5;
            core.RecruitPersonsAndSaveIntoDB(numberOfPersons);

            // Test, ob die neuen Elemente aus der Maschine in der echten DB sind
            var savedPersons = repository.GetAll<Person>();
            savedPersons.Should().HaveCount(numberOfPersons);
        }

    }
}
