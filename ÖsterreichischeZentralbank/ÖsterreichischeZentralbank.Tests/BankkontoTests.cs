using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ÖsterreichischeZentralbank.Tests
{
    [TestClass]
    public class BankkontoTests
    {
        [TestMethod]
        public void Bankkonto_neues_Konto_hat_Kontostand_100()
        {
            Bankkonto konto = new Bankkonto();

            Assert.AreEqual(100m, konto.Kontostand);
        }

        [TestMethod]
        public void Bankkonto_neues_Konto_bekommt_Kontostand_vom_Konstruktor()
        {
            decimal neuerWert = 200_000m;
            Bankkonto konto = new Bankkonto(neuerWert);

            Assert.AreEqual(neuerWert, konto.Kontostand);
        }

        [TestMethod]
        public void Bankkonto_neues_Konto_mit_falschem_Wert_im_Konstruktor_wirft_ArgumentException()
        {
            decimal neuerWert = -200_000m;

            Assert.ThrowsException<ArgumentException>(() => new Bankkonto(neuerWert));
        }

        [TestMethod]
        public void Einzahlen_mit_gültigem_Wert_erhöht_Kontostand()
        {
            Bankkonto konto = new Bankkonto(0);

            // Feature aus C# 7: 
            decimal betrag = 100_000m;
            konto.Einzahlen(betrag);

            Assert.AreEqual(betrag, konto.Kontostand);
        }

        [TestMethod]
        public void Einzahlen_mit_ungültigem_Wert_wirft_ArgumentException()
        {
            Bankkonto konto = new Bankkonto(0);

            decimal betrag = -100_000m;
            Assert.ThrowsException<ArgumentException>(() => konto.Einzahlen(betrag));
        }

        [TestMethod]
        public void Einzahlen_über_DezimalMaxValue_wirft_OverflowException()
        {
            Bankkonto konto = new Bankkonto(decimal.MaxValue);

            Assert.ThrowsException<OverflowException>(() => konto.Einzahlen(1));
        }

        [TestMethod]
        public void Abheben_mit_gültigem_Wert_verringert_Kontostand()
        {
            Bankkonto konto = new Bankkonto(100);

            decimal betrag = 80;
            decimal alterKontostand = konto.Kontostand;

            konto.Abheben(betrag);

            Assert.AreEqual(alterKontostand - betrag, konto.Kontostand);
        }

        [TestMethod]
        public void Abheben_mit_gültigem_Wert_aber_mehr_als_Kontostand_wift_InvalidOperationException()
        {
            Bankkonto konto = new Bankkonto(100);

            decimal betrag = 200;

            Assert.ThrowsException<InvalidOperationException>(() => konto.Abheben(betrag));
        }
        [TestMethod]
        public void Abheben_mit_ungültigem_Wert_wirft_ArgumentException()
        {
            Bankkonto konto = new Bankkonto(0);

            decimal betrag = -100_000m;
            Assert.ThrowsException<ArgumentException>(() => konto.Abheben(betrag));
        }

        // Zustand erreicht => Einchecken


        [TestMethod]
        public void Reichtum_gibt_den_richten_Wert_aus()
        {
            using(ShimsContext.Create())
            {
                Bankkonto ba = new Bankkonto(); // In wirklichkeit 100

                Fakes.ShimBankkonto.AllInstances.KontostandGet = x => 0;
                Assert.AreEqual(Reichtum.Pleite, ba.Reichtum);

                Fakes.ShimBankkonto.AllInstances.KontostandGet = x => 50;
                Assert.AreEqual(Reichtum.Arm, ba.Reichtum);

                Fakes.ShimBankkonto.AllInstances.KontostandGet = x => 500;
                Assert.AreEqual(Reichtum.Normal, ba.Reichtum);

                Fakes.ShimBankkonto.AllInstances.KontostandGet = x => 50_000;
                Assert.AreEqual(Reichtum.GehobeneMittelschicht, ba.Reichtum);

                Fakes.ShimBankkonto.AllInstances.KontostandGet = x => 9_000_000;
                Assert.AreEqual(Reichtum.MegaReich, ba.Reichtum);
            }
        }
    }
}
