using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÖsterreichischeZentralbank
{
    public enum Reichtum { Pleite,Arm,Normal,GehobeneMittelschicht,Reich,MegaReich,Steuerhinterzieher}
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
        public Reichtum Reichtum
        {
            get
            {
                if (Kontostand == 0)
                    return Reichtum.Pleite;
                else if (Kontostand < 100)
                    return Reichtum.Arm;
                else if (Kontostand < 10_000)
                    return Reichtum.Normal;
                else if (Kontostand < 100_000)
                    return Reichtum.GehobeneMittelschicht;
                else if (Kontostand <= 1_000_000)
                    return Reichtum.Reich;
                else if (Kontostand <= 100_000_000)
                    return Reichtum.MegaReich;
                else
                    return Reichtum.Steuerhinterzieher;
            }
        }
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
