using Hypo_Banka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Testovi
{
    [TestClass]
    public class NoviTestovi
    {
        #region Zamjenski Objekti

        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            Racun r = new Racun(100);
            b.RadSaKlijentom(k, 0, null);
            b.OtvaranjeNovogRačuna(k, r);

            Kredit kredit = new Kredit(k, 50000, 1000, 0.05, DateTime.Now.AddYears(1));

            IZahtjev zahtjev = new Zahtjev();

            b.OdobriKredit(zahtjev, kredit);

            Assert.AreEqual(b.Krediti.Count, 0);
        }

        #endregion

        #region TDD

        [TestMethod]
        public void TestSkidanjaIznosaSRačuna()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            Racun r = new Racun(1000);
            b.RadSaKlijentom(k, 0, null);
            b.OtvaranjeNovogRačuna(k, r);

            k.SkiniIznosSaNekogOdRačuna(1000);

            Assert.AreEqual(k.Racuni[0].StanjeRacuna, 0);
        }

        [TestMethod]
        public void TestSkidanjaIznosaSVišeRačuna()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            Racun r1 = new Racun(1000);
            Racun r2 = new Racun(1000);
            b.RadSaKlijentom(k, 0, null);
            b.OtvaranjeNovogRačuna(k, r1);
            b.OtvaranjeNovogRačuna(k, r2);

            r1.PromijeniStanjeRačuna("BANKAR12345", -1000);
            r1.Blokiran = true;

            k.SkiniIznosSaNekogOdRačuna(1000);

            Assert.AreEqual(k.Racuni[1].StanjeRacuna, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSkidanjaIznosaIzuzetak()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            Racun r1 = new Racun(1000);
            Racun r2 = new Racun(1000);
            b.RadSaKlijentom(k, 0, null);
            b.OtvaranjeNovogRačuna(k, r1);
            b.OtvaranjeNovogRačuna(k, r2);

            r1.PromijeniStanjeRačuna("BANKAR12345", -1000);
            r1.Blokiran = true;

            r2.PromijeniStanjeRačuna("BANKAR12345", -1000);
            r2.Blokiran = true;

            k.SkiniIznosSaNekogOdRačuna(1000);
        }

        #endregion
    }
}
