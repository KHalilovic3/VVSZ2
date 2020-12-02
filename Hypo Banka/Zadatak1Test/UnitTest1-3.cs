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
            Kredit kredit = new Kredit(new Klijent(), 500, 50, 0.05, new DateTime(2021, 5, 5));
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


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDajUkupanIznosNovcaNaSvimRačunimaIzuzetak()
        {

            DateTime datum1 = new DateTime(2010, 8, 18);
            List<Racun> racuni = new List<Racun>();
            Klijent Zoran = new Klijent();
            var novac = Zoran.DajUkupanIznosNovcaNaSvimRačunima();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDajUkupanIznosNovcaNaSvimRačunimaSviBlokirani()
        {

            DateTime datum1 = new DateTime(2010, 8, 18);
            List<Racun> racuni = new List<Racun>();
            Klijent Zoran = new Klijent();
            Racun r = new Racun(30.0);
            r.PromijeniStanjeRačuna("BANKAR12345", -30.0);
            r.Blokiran = true;
            Zoran.Racuni.Add(r);
            var novac = Zoran.DajUkupanIznosNovcaNaSvimRačunima();
        }


        [TestMethod]
        public void TestDajUkupanIznosNovcaNaSvimRačunimaRacun()
        {

            DateTime datum1 = new DateTime(2010, 8, 18);
            List<Racun> racuni = new List<Racun>();
            Klijent Zoran = new Klijent();
            Racun r = new Racun(30.0);
            Racun r1 = new Racun(35.0);
            Zoran.Racuni.Add(r);
            Zoran.Racuni.Add(r1);
            var novac = Zoran.DajUkupanIznosNovcaNaSvimRačunima();
            Assert.AreEqual(65, Convert.ToInt32(novac));
        }

        [TestMethod]
        public void TestAutomatskoGenerisanjePodataka1()
        {
            Klijent k = new Klijent();
            k.Ime = "Homer";
            k.Prezime = "Simpson";

            Tuple<string, string> t = k.AutomatskoGenerisanjePodataka();
            var nk = k.KorisnickoIme;
            var nl = k.Lozinka.Length;


            byte[] data = System.Text.Encoding.ASCII.GetBytes("HSimpson1HSimpsonHSimpson14$");
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            int tl = System.Text.Encoding.ASCII.GetString(data).Length;

            Tuple<string, int> n1 = new Tuple<string, int>(nk, nl);
            Tuple<string, int> n2 = new Tuple<string, int>("HSimpson1", tl);

            Assert.AreEqual(n1, n2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAutomatskoGenerisanjePodataka2()
        {
            Klijent k = new Klijent();
            k.Prezime = "Simpson";

            Tuple<string, string> t = k.AutomatskoGenerisanjePodataka();
        }

        //List<string> podaci ne radi nista u metodi, nema efekta u testiranju
        [TestMethod]
        public void TestRadaSaKlijentomOpcijaPrva()
        {
            Banka banka = new Banka();
            Klijent klijent = new Klijent();
            banka.RadSaKlijentom(klijent, 0, null);
            Assert.IsTrue(banka.Klijenti.Contains(klijent));
        }

        [TestMethod]
        public void TestRadaSaKlijentomOpcijaDruga()
        {
            Banka banka = new Banka();
            Klijent klijent = new Klijent("Homer", "Simp", "HSimp1", "lozinkalozinkalozinka42=", new DateTime(2000, 05, 05), "123H456");
            banka.Klijenti.Add(klijent);
            banka.RadSaKlijentom(klijent, 1, null);
            Assert.AreEqual(new Tuple<string, string>(banka.Klijenti[0].KorisnickoIme, banka.Klijenti[0].Lozinka), klijent.AutomatskoGenerisanjePodataka());
        }
        [TestMethod]
        public void TestRadaSaKlijentomOpcijaTreca()
        {
            Banka banka = new Banka();
            Klijent klijent = new Klijent();
            banka.Klijenti.Add(klijent);
            banka.RadSaKlijentom(klijent, 2, null);
            Assert.IsFalse(banka.Klijenti.Contains(klijent));
        }

        static IEnumerable<object[]> PodaciKlijenta
        {
            get
            {
                return new[]
                {
                    new object[]{"Homer","Simp","HSimp1","lozinkalozinkalozinka42=",new DateTime(2000,05,05),"123H456"},
                    new object[]{"Bart","Simponoivovis","BSimp1","lozinkalozinkalozinka1+",new DateTime(2000,05,05),"123B456"},
                    new object[]{"Bob","Simp","BSimp1","lozinkalozinkalozinka41-",new DateTime(2000, 05,05),"123I456"},
                    new object[]{"Lisa","Simp","LSimp1", "lozinkalozinkalozinkalozinkalozinkalozinkalozinkalozinkalozinkalozinkalozinkalozinka4-", new DateTime(1999, 05, 05), "123L456" },
                    new object[]{"Marg","Simp","HSimp1","lozinkalozinkalozinka43=",new DateTime(1900,05,05),"123M456"},
                    new object[]{"Mike","Simp","MSimp1","lozinkalozinkalozinka41-",new DateTime(2000, 05,05),"123I456"}
                };
            }
        }
        static IEnumerable<object[]> PodaciKlijentaGreska
        {
            get
            {
                return new[]
                {
                    new object[]{"h","Simp","HSimp1","lozinkalozinkalozinka42=",new DateTime(2000,05,05),"123H456"},
                    new object[]{"Bart","s","BSimp1","lozinkalozinkalozinka1+",new DateTime(2000,05,05),"123B456"},
                    new object[]{"Bob","Simp","","lozinkalozinkalozinka41-",new DateTime(2000, 05,05),"123I456"},
                    new object[]{"Lisa","Simp","LSimp1", "l4-", new DateTime(1999, 05, 05), "123L456" },
                    new object[]{"Marg","Simp","HSimp1", "lozinkalozinkalozinka41-", new DateTime(2020,05,05),"123M456"},
                    new object[]{"Mike","Simp","HSimp1","lozinkalozinkalozinka41-",new DateTime(2000, 05,05),"123I46A"}
                };
            }
        }
        [TestMethod]
        [DynamicData("PodaciKlijenta")]
        public void DataDrivenKorisnicko(string ime, string prezime, string korisnickoIme, string lozinka,
            DateTime rodenje, string licna)
        {
            Klijent k = new Klijent(ime, prezime, korisnickoIme, lozinka, rodenje, licna);
            StringAssert.Equals(ime, k.Ime);
            StringAssert.Equals(prezime, k.Prezime);
            StringAssert.Equals(korisnickoIme, k.KorisnickoIme);
            byte[] data = System.Text.Encoding.ASCII.GetBytes(lozinka);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string Lozinka = System.Text.Encoding.ASCII.GetString(data);
            Assert.AreEqual(Lozinka.Length, k.Lozinka.Length);
            Assert.AreEqual(rodenje, k.DatumRodenja);
            StringAssert.Equals(licna, k.BrojLicneKarte);
        }
        [TestMethod]
        [DynamicData("PodaciKlijentaGreska")]
        [ExpectedException(typeof(ArgumentException))]
        public void DataDrivenKorisnickoGreske(string ime, string prezime, string korisnickoIme, string lozinka,
            DateTime rodenje, string licna)
        {
            Klijent k = new Klijent();
            k.Ime = ime;
            k.Prezime = prezime;
            k.KorisnickoIme = korisnickoIme;
            k.Lozinka = lozinka;
            k.BrojLicneKarte = licna;
            //Posto datum vraca drugaciju vrstu greske, potrebno je uhvatiti tu i onda baciti onu koja se ocekuje
            try
            {
                k.DatumRodenja = rodenje;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException();
            }
        }
        static IEnumerable<object[]> PodaciKredit
        {
            get
            {
                return new[]
                {
                   new object[]{new Klijent(),500,50,0.05,new DateTime(2021,05,05)},
                    new object[]{new Klijent(), 99999, 50,0.05,new DateTime(2021,05,05)},
                    new object[]{new Klijent(),500,3999,0.05,new DateTime(2021,05,05)},
                    new object[]{new Klijent(),500,50,0.05,new DateTime(2029,05,05)}
                };
            }
        }
        static IEnumerable<object[]> PodaciKreditGreska
        {
            get
            {
                return new[]
                {
                    new object[]{new Klijent(), 100001.00, 50,0.05,new DateTime(2021,05,05)},
                    new object[]{new Klijent(),500, 4002.00, 0.01,new DateTime(2021,05,05)},
                     new object[]{new Klijent(),500, 50, 0.2,new DateTime(2021,05,05)},
                    new object[]{new Klijent(),500,50,0.05,new DateTime(2040,05,05)}
                };
            }
        }
        [TestMethod]
        [DynamicData("PodaciKredit")]
        public void DataDrivenKredit(Klijent client, double amount, double monthlyAmount, double interestRate, DateTime dueDate)
        {
            Kredit k = new Kredit(client, amount, monthlyAmount, interestRate, dueDate);
            Assert.IsNotNull(k.Klijent);
            Assert.AreEqual(amount, k.Iznos);
            Assert.AreEqual(monthlyAmount, k.Rata);
            Assert.AreEqual(interestRate, k.KamatnaStopa);
            Assert.AreEqual(dueDate, k.RokOtplate);
        }
        [TestMethod]
        [DynamicData("PodaciKreditGreska")]
        [ExpectedException(typeof(ArgumentException))]
        public void DataDrivenKreditGreska(Klijent client, double amount, double monthlyAmount, double interestRate, DateTime dueDate)
        {
            try
            {
                Kredit k = new Kredit(client, amount, monthlyAmount, interestRate, dueDate);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException();
            }
        }
        static IEnumerable<object[]> PodaciRacun
        {
            get
            {
                return new[]
                {
                    new object[] { 50 },
                    new object[] { 999999999999 },
                    new object[] { 1 }
                };
            }
        }
      
        static IEnumerable<object[]> PodaciRacunGreska
        {
            get
            {
                return new[]
                {
                    new object[] { 50,true},
                    new object[] { -10,false },
                    new object[] { -1,false }
                };
            }
        }
        [TestMethod]
        [DynamicData("PodaciRacun")]
        public void DataDrivenRacun(double pocetnoStanje)
        {
            Racun r = new Racun(pocetnoStanje);
            Assert.IsFalse(r.Blokiran);
        }
        //Zbog konstruktora Racuna nije moguce postaviti racun na 0 na kreaciji objekta, pa se mora promjeniti radi validacije podataka. 
        [TestMethod]
        [DynamicData("PodaciRacunGreska")]
        [ExpectedException(typeof(ArgumentException))]
        public void DataDrivenRacunGreska(double promjena,bool blokiran)
        {
            Racun r = new Racun(1);
            r.PromijeniStanjeRačuna("BANKAR12345", promjena);
            r.Blokiran = blokiran;
        }

        /*Testiranje pravilnog izvršavanje metode*/
        [TestMethod]
        public void TestOtvaranjeNovogRačuna1()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            k.BrojLicneKarte ="123A456";
            b.Klijenti.Add(k);

            Racun r = new Racun(200);

            b.OtvaranjeNovogRačuna(k, r);

            Assert.AreEqual(k.Racuni[0], r);
        }

        /*Testiranje u slučaju kada klijent nije registrovan u banci*/
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOtvaranjeNovogRačuna2()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            k.BrojLicneKarte = "123A456";

            Racun r = new Racun(200);

            b.OtvaranjeNovogRačuna(k, r); 

        }

        /*Testiranje u slučaju kada klijent ima više od 3 računa*/
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestOtvaranjeNovogRačuna3()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            k.BrojLicneKarte = "123A456";
            b.Klijenti.Add(k);

            Racun r1 = new Racun(200);
            Racun r2 = new Racun(300);
            Racun r3 = new Racun(400);
            Racun r4 = new Racun(500);

            b.OtvaranjeNovogRačuna(k, r1);
            b.OtvaranjeNovogRačuna(k, r2);
            b.OtvaranjeNovogRačuna(k, r3);
            b.OtvaranjeNovogRačuna(k, r4);
        }

        /*Testiranje pravilnog izvršavanje metode*/
        [TestMethod]
        public void TestDajKredit1()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            k.BrojLicneKarte = "123A456";
            b.Klijenti.Add(k);

            Kredit kr = new Kredit(k, 500, 50, 0.05, new DateTime(2021, 5, 5));

            b.DajKredit(kr);

            Assert.AreEqual(b.Krediti[0], kr);
        }

        /*Testiranje slučaja kada ne postoji klijent u banci sa tim kreditom*/
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDajKredit2()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            k.BrojLicneKarte = "123A456";
            

            Kredit kr = new Kredit(k, 500, 50, 0.05, new DateTime(2021, 5, 5));

            b.DajKredit(kr);
        }


        /*Testiranje pravilnog izvršavanje metode*/
        [TestMethod]
        public void TestOdobriKredit1()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
            
            k.BrojLicneKarte = "123A456";
            b.Klijenti.Add(k);

            Racun r = new Racun(5000);

            b.OtvaranjeNovogRačuna(k, r);

            Kredit kr = new Kredit(k, 500, 50, 0.05, new DateTime(2021, 5, 5));

            IZahtjev zahtjev = new ZamjenskiObjekat(kr);

            Assert.AreEqual(b.OdobriKredit(zahtjev, kr), true);
        }

        /*Testiranjeizvršavanje metode u slučaju da zahtjev nije povoljan*/
        [TestMethod]
        public void TestOdobriKredit2()
        {
            Banka b = new Banka();
            Klijent k = new Klijent();
             
            k.BrojLicneKarte = "123A456";
            b.Klijenti.Add(k);

            Racun r = new Racun(10);

            b.OtvaranjeNovogRačuna(k, r);

            Kredit kr = new Kredit(k, 500, 50, 0.05, new DateTime(2021, 5, 5));

            IZahtjev zahtjev = new ZamjenskiObjekat(kr);

            Assert.AreEqual(b.OdobriKredit(zahtjev, kr), false);
        }




    }
}
