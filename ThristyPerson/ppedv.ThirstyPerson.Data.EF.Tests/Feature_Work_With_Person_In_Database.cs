using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.ThirstyPerson.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.ThirstyPerson.Data.EF.Tests
{
    [TestClass]
    public class Feature_Work_With_Person_In_Database
    {
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ThirstyPerson_Test;Trusted_Connection=true;AttachDbFileName=C:\temp\ThirstyPerson.mdf";
        private static  Person p1 = new Person { FirstName = "Tom", LastName = "Ate", Age = 10, Balance = 100 };
        private static Person loadedPerson;
        private static EFContext context;

        [TestMethod]
        public void T00_Given_that_i_can_connect_to_the_database()
        {
            context = new EFContext(connectionString);
        }
        [TestMethod]
        public void T01_And_that_a_Database_Exists()
        {
            context.Database.Exists().Should().BeTrue();
        }
        [TestMethod] // Create
        public void T02_It_should_be_possible_to_insert_a_Person()
        {
            context.Person.Add(p1);
            context.SaveChanges();
        }
        [TestMethod] // Create
        public void T03_Then_i_should_be_able_to_read_it_from_the_Database()
        {
            loadedPerson = context.Person.Find(p1.ID);
        }
        [TestMethod]
        public void T04_Then_the_loadedPerson_should_be_Equivalent_to_the_inserted_person()
        {
            loadedPerson.Should().BeEquivalentTo(p1);
        }
        // ... weiter bis zum delete ...
    }
}
