using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErstenUnitTests
{
    public class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public byte Alter { get; set; }
        public decimal Kontostand { get; set; }

        public override bool Equals(object obj)
        {
            // Anforderungen:
            // Wenn obj == null -> false
            // Wenn obj den falschen Datentyp hat -> false
            // Wenn obj und "this" Referenzgleich sind -> true
            // ----> Wenn nicht, sind sie wertegleich ?

            // Teil1: Equals bauen
            // Teil2: Tests schreiben und alle Möglichkeiten durchtesten

            if (obj is null)
                return false;
            if (obj is Person == false)
                return false;
            else // Es ist eine Person
            {
                var p2 = (Person)obj;
                if (obj == this) // Referenzgleich
                    return true;
                else if (this.Vorname == p2.Vorname &&
                         this.Nachname == p2.Nachname &&
                         this.Alter == p2.Alter &&
                         this.Kontostand == p2.Kontostand) // Vlt Wertegleich ?
                    return true;
                else
                    return false; // Unterschiedliche Personen
            }
        }
    }
}
