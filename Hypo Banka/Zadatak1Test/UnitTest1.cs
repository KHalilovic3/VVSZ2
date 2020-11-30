using Hypo_Banka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Zadatak1Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProvjeriStanjeOtplate1()
        {
            Kredit kredit = new Kredit(new Klijent(),500,50,0.05,new DateTime(2021,5,5));
            String except = "Kredit koji se treba vratiti najkasnije na dan 05.05.2021. godine ima preostali iznos od 525 KM. Iznos rate je 50 KM, po stopi od 5 %.";
            Assert.AreEqual(except, kredit.ProvjeriStanjeOtplate());
        }
        [TestMethod]
        public void TestProvjeriStanjeOtplate2()
        {
            Kredit kredit = new Kredit(new Klijent(), 500, 50, 0.05, new DateTime(2021, 10, 10));
            String except = "Kredit koji se treba vratiti najkasnije na dan 10.10.2021. godine ima preostali iznos od 525 KM. Iznos rate je 50 KM, po stopi od 5 %.";
            Assert.AreEqual(except, kredit.ProvjeriStanjeOtplate());
        }

        [TestMethod]

        public void TestKlijentiSBlokiranimRačunimaRadi()
        {
            Banka banka = new Banka();
            Klijent test1 = new Klijent();
            Racun testni = new Racun(5);
            testni.PromijeniStanjeRačuna("BANKAR12345", -5);
            testni.Blokiran = true;
            test1.Racuni.Add(testni);
            Klijent test2 = new Klijent();
            test2.Racuni.Add(new Racun(50));
            banka.Klijenti.Add(test1);
            banka.Klijenti.Add(test2);
            List<Klijent> klijents = new List<Klijent>
            {
                test1
            };
            CollectionAssert.AreEqual(klijents, banka.KlijentiSBlokiranimRačunima());
        }

        [TestMethod] 
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestKlijentiSBlokiranimRačunimaGreska()
        {
            Banka banka = new Banka();
            Klijent test1 = new Klijent();
            test1.Racuni.Add(new Racun(5));
            Klijent test2 = new Klijent();
            test2.Racuni.Add(new Racun(50));
            banka.Klijenti.Add(test1);
            banka.Klijenti.Add(test2);

            banka.KlijentiSBlokiranimRačunima();
        }

    }
}
