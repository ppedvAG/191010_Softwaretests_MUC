using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErstenUnitTests
{
    public class Taschenrechner
    {
        public int Add(int z1, int z2)
        {
            checked // Prüft auf Overflow
            {
                return z1 + z2;
            }
        }
    }
}
