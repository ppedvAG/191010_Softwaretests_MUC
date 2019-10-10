using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÖsterreichischeZentralbank
{
    public class Bankkonto
    {
        public Bankkonto() : this(100m){}

        public Bankkonto(decimal neuerWert)
        {
            if (neuerWert < 0)
                throw new ArgumentException();
            Kontostand = neuerWert;
        }

        public decimal Kontostand { get; private set; }

        public void Einzahlen(decimal betrag)
        {
            if (betrag < 0)
                throw new ArgumentException();
            Kontostand += betrag;
        }

        public void Abheben(decimal betrag)
        {
            if (betrag < 0)
                throw new ArgumentException();
            else if (Kontostand - betrag < 0)
                throw new InvalidOperationException();

            Kontostand -= betrag;
        }
    }
}
