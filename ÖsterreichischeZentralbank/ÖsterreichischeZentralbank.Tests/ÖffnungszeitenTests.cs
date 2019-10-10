using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÖsterreichischeZentralbank.Tests
{
    [TestClass]
    public class ÖffnungszeitenTests
    {
        [TestMethod]
        [DataRow(2019,10,10,12,45,true)] // DO
        public void IsOpen_Tests(int jahr,int monat, int tag, int stunde, int minute, bool erwartetesErgebnis)
        {
            // Tipp:
            DateTime time = new DateTime(jahr, monat, tag, stunde, minute, 00);
        }
    }
}
