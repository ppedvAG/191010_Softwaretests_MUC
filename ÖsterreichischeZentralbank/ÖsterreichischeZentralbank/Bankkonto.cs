using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÖsterreichischeZentralbank
{
    public class Bankkonto
    {
        private decimal neuerWert;

        public Bankkonto()
        {
        }

        public Bankkonto(decimal neuerWert)
        {
            this.neuerWert = neuerWert;
        }

        public decimal Kontostand { get; set; }

        public void Einzahlen(decimal betrag)
        {
            throw new NotImplementedException();
        }

        public void Abheben(decimal betrag)
        {
            throw new NotImplementedException();
        }
    }
}
