using ppedv.ThirstyPerson.Domain;
using ppedv.ThirstyPerson.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.ThirstyPerson.Logic
{
    public class Core
    {
        public Core(IDevice device)
        {
            this.device = device;
        }
        public Core(IDevice device, IRepository repository)
        {
            this.device = device;
            this.repository = repository;
        }

        private readonly IDevice device;
        private readonly IRepository repository;

        // Irgendeine Logik, die mit der Maschine was macht
        // ---> Unittests für Core machen
        public IEnumerable<Person> RecruitManyPersonsForCompany(int amount)
        {
            if (amount < 0)
                throw new ArgumentException();

            List<Person> newPersons = new List<Person>();
            for (int i = 0; i < amount; i++)
            {
                newPersons.Add(device.RecruitPerson()); // Hier wird intern die Maschine genutzt
                //newPersons.Add(new Person()); // ohne maschine
            }
          
            return newPersons;
        }

        // Logik, in der etwas mit der Datenbank gemacht wird:
        public IEnumerable<Person> GetAllPeople()
        {
            return repository.GetAll<Person>();
        }
        public Person GetPersonWithHighestBalance()
        {
            return GetAllPeople().OrderByDescending(x => x.Balance)
                                 .First();
        }

        // Kombination: Nutze die Hardware und die Datenbank gleichzeitig:
        public void RecruitPersonsAndSaveIntoDB(int amount)
        {
            var persons = RecruitManyPersonsForCompany(amount); // Hardwarelogik
            foreach (var item in persons)
            {
                repository.Add(item); // DB-Logik
            }
            repository.Save(); // DB-Logik
        }

    }
}
