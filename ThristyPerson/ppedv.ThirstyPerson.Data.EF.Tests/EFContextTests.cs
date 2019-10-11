using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.ThirstyPerson.Domain;

namespace ppedv.ThirstyPerson.Data.EF.Tests
{
    [TestClass]
    public class EFContextTests
    {
        // ConnectionString für die Testdatenbank
        const string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ThirstyPerson_Test;Trusted_Connection=true;AttachDbFileName=C:\temp\ThirstyPerson.mdf";

        [TestMethod]
        public void EFContext_can_create_context()
        {
            EFContext context = new EFContext(connectionString);
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void EFContext_can_create_Database()
        {
            EFContext context = new EFContext(connectionString);

            // Sicherstellen, dass keine alte DB existiert
            if (context.Database.Exists())
                context.Database.Delete();

            Assert.IsFalse(context.Database.Exists());
            context.Database.Create();
            Assert.IsTrue(context.Database.Exists());
        }

        // Create, Read, Update, Delete => CRUD
        // -> Für alle Datentypen testen

        // Unittest: Testen die Funktionalität der Datenbank/EF
        [TestMethod]
        public void EFContext_can_CRUD_Person() // Atomar im Sinne des Datentypen
        {
            Person p1 = new Person { FirstName = "Tom", LastName = "Ate", Age = 10, Balance = 100 };
            string newLastName = "Atinger";

            using (EFContext context = new EFContext(connectionString))
            {
                // Create:
                context.Person.Add(p1);
                context.SaveChanges(); // Speichern -> Automatisch eine ID
            }

            using (EFContext context = new EFContext(connectionString))
            {
                // Check Create:
                var loadedPerson = context.Person.Find(p1.ID);
                Assert.AreEqual(p1.LastName, loadedPerson.LastName); // ObjectGraph - Vergleich

                // Update 
                loadedPerson.LastName = newLastName;
                context.SaveChanges(); // Speichern
            }

            using (EFContext context = new EFContext(connectionString))
            {
                // Check Update:
                var loadedPerson = context.Person.Find(p1.ID);
                Assert.AreEqual(newLastName, loadedPerson.LastName);

                // Delete 
                context.Person.Remove(loadedPerson);
                context.SaveChanges(); // Speichern
            }

            using (EFContext context = new EFContext(connectionString))
            {
                // Check Delete:
                var loadedPerson = context.Person.Find(p1.ID);
                Assert.IsNull(loadedPerson); // wurde gelöscht
            }
        }

    }
}
