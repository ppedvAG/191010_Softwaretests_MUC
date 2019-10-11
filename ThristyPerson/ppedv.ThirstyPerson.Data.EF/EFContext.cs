using ppedv.ThirstyPerson.Domain;
using System;
using System.Data.Entity;

namespace ppedv.ThirstyPerson.Data.EF
{
    // Konfiguration für die Datenbankverbindung
    public class EFContext : DbContext
    {
        // Konstruktor: ConnectionString

        // Teilnehmer: "Server=.;Database=ThirstyPerson_Produkiv;Trusted_Connection=true"
        // Trainer: "Server=(localdb)\MSSQLLocalDB;Database=ThirstyPerson_Produkiv;Trusted_Connection=true;AttachDbFileName=C:\temp\ThirstyPerson.mdf"
        // https://www.connectionstrings.com/
        public EFContext() : this(@"Server=(localdb)\MSSQLLocalDB; Database=ThirstyPerson_Produkiv;Trusted_Connection=true;AttachDbFileName=C:\temp\ThirstyPerson.mdf") { }
        public EFContext(string connectionString) : base(connectionString) { }

        // DBSets -> Für den Zugriff auf die Tabellen

        public DbSet<Person> Person { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Company> Company { get; set; }

    }
}
