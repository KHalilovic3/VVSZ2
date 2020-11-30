using System;
using System.Collections.Generic;

namespace Hypo_Banka
{
    public class Banka
    {
        #region Atributi

        List<Klijent> klijenti;
        List<Kredit> krediti;

        #endregion

        #region Properties

        public List<Klijent> Klijenti
        {
            get => klijenti;
        }

        public List<Kredit> Krediti
        {
            get => krediti;
        }

        #endregion

        #region Konstruktor

        public Banka()
        {
            klijenti = new List<Klijent>();
            krediti = new List<Kredit>();
        }

        #endregion

        #region Metode

        public void RadSaKlijentom(Klijent k, int opcija, List<string> podaci)
        {
            if (opcija == 0)
                klijenti.Add(k);

            else if (opcija == 1)
            {
                Klijent klijent = klijenti.Find(client => client.BrojLicneKarte == k.BrojLicneKarte);
                klijent.AutomatskoGenerisanjePodataka();
            }

            else if (opcija == 2)
            {
                klijenti.Remove(k);
            }
        }

        public void OtvaranjeNovogRačuna(Klijent k, Racun r)
        {
            Klijent klijent = klijenti.Find(client => client.BrojLicneKarte == k.BrojLicneKarte);
            if (klijent == null)
                throw new ArgumentNullException("Klijent nije registrovan!");

            if (klijent.Racuni.Count < 3)
                klijent.Racuni.Add(r);

            else throw new InvalidOperationException("Nemoguće imati više od 3 računa!");
        }

        /// <summary>
        /// Metoda u kojoj se vraćaju svi klijenti koji posjeduju blokirane račune.
        /// Prvenstveno je potrebno blokirati sve račune koji ispunjavaju uslove za to
        /// prema postojećoj programskoj logici.
        /// Ukoliko nema nijedan klijent s blokiranim računom, baca se izuzetak.
        /// </summary>
        public List<Klijent> KlijentiSBlokiranimRačunima()
        {
            throw new NotImplementedException();
        }

        public void DajKredit(Kredit kredit)
        {
            Klijent k = klijenti.Find(client => client.BrojLicneKarte == kredit.Klijent.BrojLicneKarte);

            if (k == null || Krediti.Count > 99)
                throw new InvalidOperationException("Nemoguće dati kredit klijentu!");

            krediti.Add(kredit);
        }

        public bool OdobriKredit(IZahtjev zahtjev, Kredit kredit)
        {
            if (zahtjev.DaLiJePovoljan())
            {
                DajKredit(kredit);
                return true;
            }

            return false;
        }

        #endregion
    }
}
