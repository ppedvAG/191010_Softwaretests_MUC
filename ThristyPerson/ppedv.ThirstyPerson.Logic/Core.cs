using ppedv.ThirstyPerson.Domain;
using ppedv.ThirstyPerson.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ppedv.ThirstyPerson.Logic
{
    public class Core
    {
        public Core(IDevice device)
        {
            this.device = device;
        }

        private readonly IDevice device;

        // Irgendeine Logik, die mit der Maschine was macht
        // ---> Unittests für Core machen
        public IEnumerable<Person> RecruitManyPersonsForCompany(int amount)
        {
            List<Person> newPersons = new List<Person>();
            for (int i = 0; i < amount; i++)
            {
                newPersons.Add(device.RecruitPerson()); // Hier wird intern die Maschine genutzt
            }
            return newPersons;
        }
    }
}
