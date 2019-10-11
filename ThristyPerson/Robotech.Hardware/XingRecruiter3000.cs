using AutoFixture;
using ppedv.ThirstyPerson.Domain;
using ppedv.ThirstyPerson.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotech.Hardware
{
    public class XINGRecruiter3000 : IDevice
    {
        private Fixture fix = new Fixture();
        public Person RecruitPerson()
        {
            Console.Beep(8000,500);
            Console.Beep(5000, 500);
            return fix.Create<Person>();
        }
    }
}
