using System;
using System.Collections.Generic;
using System.Text;

namespace Hypo_Banka
{
    public class Kredit
    {
        #region Atributi

        Klijent klijent;
        double iznos, rata, kamatnaStopa;
        DateTime rokOtplate;

        #endregion

        #region Properties

        public Klijent Klijent
        {
            get => klijent;
            set => klijent = value;
        }

        public double Iznos
        {
            get => iznos;
            set
            {
                if (value < 0.00 || value > 100000.00)
                    throw new InvalidOperationException("Pogrešna vrijednost iznosa!");

                iznos = value;
            }
        }

        public double Rata
        {
            get => rata;
            set
            {
                if (value < 0.00 || value > 4000.00)
                    throw new InvalidOperationException("Pogrešna vrijednost rate!");

                rata = value;
            }
        }

        public double KamatnaStopa
        {
            get => kamatnaStopa;
            set
            {
                if (value < 0.02 || value > 0.1)
                    throw new ArgumentException("Pogrešan iznos kamatne stope!");

                kamatnaStopa = value;
            }
        }

        public DateTime RokOtplate
        {
            get => rokOtplate;
            set
            {
                if (DateTime.Compare(value, DateTime.Now) < 0 ||
                    DateTime.Now.AddYears(10) < value)
                    throw new ArgumentException("Pogrešan rok otplate!");

                rokOtplate = value;
            }
        }

        #endregion

        #region Konstruktor

        public Kredit(Klijent client, double amount, double monthlyAmount, double interestRate, DateTime dueDate)
        {
            Klijent = client;
            Iznos = amount;
            Rata = monthlyAmount;
            KamatnaStopa = interestRate;
            RokOtplate = dueDate;
        }

        #endregion

        #region Metode

        /// <summary>
        /// Metoda u kojoj se vrši provjera stanje otplate kredita.
        /// Povratni string je sljedećeg formata:
        /// "Kredit koji se treba vratiti najkasnije na dan XX.XX.XXXX. godine
        /// ima preostali iznos od XXXX KM. Iznos rate je XXXX KM, po stopi od
        /// XX %."
        /// </summary>
        /// <returns></returns>
        public string ProvjeriStanjeOtplate()

        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
