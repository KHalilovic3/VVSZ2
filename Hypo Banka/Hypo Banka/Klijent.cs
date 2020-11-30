using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hypo_Banka
{
    public class Klijent : Korisnik
    {
        #region Atributi

        DateTime datumRodenja;
        string brojLicneKarte;
        List<Racun> racuni;

        #endregion

        #region Properties

        public DateTime DatumRodenja
        {
            get => datumRodenja;
            set
            {
                if (DateTime.Compare(DateTime.Now, value) < 0
                    || value.AddYears(18) > DateTime.Now)
                    throw new InvalidOperationException("Datum nije ispravan!");

                datumRodenja = value;
            }
        }
        public string BrojLicneKarte
        {
            get => brojLicneKarte;
            set
            {
                if (String.IsNullOrEmpty(value)
                    || value.Length != 7
                    || !value.Substring(0, 3).All(char.IsDigit)
                    || !value.Substring(3, 1).All(char.IsLetter)
                    || !value.Substring(4).All(char.IsDigit))
                    throw new ArgumentException("Pogrešan broj lične karte!");

                brojLicneKarte = value;
            }
        }
        public List<Racun> Racuni
        {
            get => racuni;
        }

        #endregion

        #region Konstruktor

        public Klijent(string ime, string prezime, string korisnickoIme, string lozinka,
            DateTime rodenje, string licna)
            : base(ime, prezime, korisnickoIme, lozinka)
        {
            DatumRodenja = rodenje;
            BrojLicneKarte = licna;
            racuni = new List<Racun>();
        }

        public Klijent() : base()
        {
            racuni = new List<Racun>();
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se vraća ukupan iznos novca na svim računima klijenta.
        /// Ukoliko klijent nema nijedan račun, dolazi do pojave izuzetka, kao i u
        /// slučaju da su svi njegovi računi blokirani.
        /// </summary>
        /// <param name="r"></param>
        public double DajUkupanIznosNovcaNaSvimRačunima()
        {   if (racuni.Count == 0)
            {
                throw new ArgumentException("Klijent nema nijedan račun!");
            }

            int cblock = 0;
            double stanje = 0;
            foreach (var racun in racuni)
            {
                if (racun.Blokiran == true)
                {
                    cblock++;
                }

                else
                {
                    stanje=stanje+racun.StanjeRacuna;
                }
            }
            
            if (cblock == racuni.Count)
            {
                throw new ArgumentException("Klijentu su svi računi blokirani!");
            }

            return stanje;
        }

        public bool SkiniIznosSaNekogOdRačuna(double ukupniIznos)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
