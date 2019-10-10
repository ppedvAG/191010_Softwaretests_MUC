using ÖsterreichischeZentralbank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.QualityTools.Testing.Fakes;
using System.IO;
using Pose;

namespace ÖsterreichischeZentralbank.Tests
{
    [TestClass]
    public class ÖffnungszeitenTests
    {
        [TestMethod]
        [DataRow(2019, 10, 7, 10, 29, false)]   // Mo vor offen
        [DataRow(2019, 10, 7, 10, 30, true)]    // Mo offen
        [DataRow(2019, 10, 7, 18, 59, true)]    // Mo vor schluss
        [DataRow(2019, 10, 7, 19, 00, false)]   // Mo schluss
        [DataRow(2019, 10, 8, 10, 29, false)]   // Di vor offen
        [DataRow(2019, 10, 8, 10, 30, true)]    // Di offen
        [DataRow(2019, 10, 8, 18, 59, true)]    // Di vor schluss
        [DataRow(2019, 10, 8, 19, 00, false)]   // Di schluss
        [DataRow(2019, 10, 9, 10, 29, false)]   // Mi vor offen
        [DataRow(2019, 10, 9, 10, 30, true)]    // Mi offen
        [DataRow(2019, 10, 9, 18, 59, true)]    // Mi vor schluss
        [DataRow(2019, 10, 9, 19, 00, false)]   // Mi schluss
        [DataRow(2019, 10, 10, 10, 29, false)]  // Do vor offen
        [DataRow(2019, 10, 10, 10, 30, true)]   // Do offen
        [DataRow(2019, 10, 10, 18, 59, true)]   // Do vor schluss
        [DataRow(2019, 10, 10, 19, 00, false)]  // Do schluss
        [DataRow(2019, 10, 11, 10, 29, false)]  // Fr vor offen
        [DataRow(2019, 10, 11, 10, 30, true)]   // Fr offen
        [DataRow(2019, 10, 11, 18, 59, true)]   // Fr vor schluss
        [DataRow(2019, 10, 11, 19, 00, false)]  // Fr schluss
        [DataRow(2019, 10, 12, 10, 29, false)]  // Sa vor offen
        [DataRow(2019, 10, 12, 10, 30, true)]   // Sa offen
        [DataRow(2019, 10, 12, 13, 59, true)]   // Sa vor schluss
        [DataRow(2019, 10, 12, 14, 00, false)]  // Sa schluss
        [DataRow(2019, 10, 13, 10, 29, false)]  // So zu
        [DataRow(2019, 10, 13, 10, 30, false)]  // So zu
        [DataRow(2019, 10, 13, 18, 59, false)]  // So zu
        [DataRow(2019, 10, 13, 19, 00, false)]  // So zu
        public void IsOpen_Tests(int jahr, int monat, int tag, int stunde, int minute, bool erwartetesErgebnis)
        {
            Öffnungszeiten öz = new Öffnungszeiten();
            DateTime time = new DateTime(jahr, monat, tag, stunde, minute, 00);

            Assert.AreEqual(erwartetesErgebnis, öz.IsOpen(time));
        }

        [TestMethod]
        public void IsNowOpenTest()
        {
            Öffnungszeiten öz = new Öffnungszeiten();
            // Problem: Am Fr gehts, am So nicht -> DateTime.Now
            Assert.IsTrue(öz.IsNowOpen());

            // Lösung 1: Fakes-Framework: Visual Studio Enterprise
            // Lösung 2: Tonerdo.Pose -> OpenSource-Variante vom Fakes-Framework
        }

        [TestMethod]
        public void IsNowOpenTest_FakesFramwork()
        {
            // 1) Referenzen -> FakeAssembly hinzufügen
            Öffnungszeiten öz = new Öffnungszeiten();
            
            // 2) Shims.Context -> Bereich, in dem die Fake-Version gültig ist

            using(ShimsContext.Create())
            {
                // Hier gelten die Fakes
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(1848, 3, 12, 12, 32, 55);

                var fakevariante = DateTime.Now;
                Assert.IsFalse(öz.IsNowOpen()); // Sonntag hat zu !!!
                // <--- Hier wird intern DateTime.Now verwendet

                // Andere Anwendungsfälle:
                System.IO.Fakes.ShimFile.ExistsString = x => true; // "Datei existiert immer"

                if (File.Exists("7:\\/abcde\\demo.%&/([].txt🙂😍😎😊☺☺"))
                    ;
            }

            var original = DateTime.Now;
            // Hier gilt das Original
        }

        // https://github.com/tonerdo/pose
        [TestMethod]
        public void IsNowOpenTest_Pose()
        {
            // NUGet: Pose

            // Fakekonfiguration
            Shim dateShim = Shim.Replace(() => DateTime.Now)
                                .With(() => new DateTime(1848, 3, 12, 12, 32, 55));
            DateTime date = default;
            PoseContext.Isolate(() =>
            {
                // Aktuell (10.10.2019) kann man Breakpoins innerhalb von PoseContext.Isolate nicht nutzen :(
               date = DateTime.Now;
            },dateShim);

        }
    }
}

