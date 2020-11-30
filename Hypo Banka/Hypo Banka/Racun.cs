using System;
using System.Collections.Generic;
using System.Text;

namespace Hypo_Banka
{
    public class Racun
    {
        #region Atributi

        static int trenutniBroj = 0;
        string brojRacuna;
        double stanjeRacuna;
        bool blokiran;

        #endregion

        #region Properties

        public string BrojRacuna
        {
            get => brojRacuna;
        }
        public double StanjeRacuna
        {
            get => stanjeRacuna;
        }
        public bool Blokiran
        {
            get => blokiran;
            set
            {
                if (stanjeRacuna > 0.0 && value == true)
                    throw new ArgumentException("Račun nije prazan!");
                else if (stanjeRacuna < 0.1 && value == false)
                    throw new ArgumentException("Račun je prazan!");

                blokiran = value;
            }
        }

        #endregion

        #region Konstruktor

        public Racun(double pocetnoStanje)
        {
            trenutniBroj += 1;
            brojRacuna = trenutniBroj.ToString().GetHashCode().ToString();
            stanjeRacuna = pocetnoStanje;
            Blokiran = false;
        }

        #endregion

        #region Metode

        public void PromijeniStanjeRačuna(string verifikacija, double promjena)
        {
            if (verifikacija == "BANKAR12345")
                stanjeRacuna += promjena;
            else
                throw new AccessViolationException("Niste autorizovani za vršenje promjene!");
        }

        #endregion
    }
}
