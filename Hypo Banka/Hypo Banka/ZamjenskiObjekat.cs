using System;
using System.Collections.Generic;
using System.Text;

namespace Hypo_Banka
{

    public class ZamjenskiObjekat : IZahtjev
    {
        Kredit kredits;
        public ZamjenskiObjekat(Kredit kredit){
            kredits = kredit;
        }


        public bool DaLiJePovoljan()
        {
            foreach(var r in kredits.Klijent.Racuni)
            {
                if (r.StanjeRacuna > kredits.Rata)
                {
                    return true;
                }
            }
            return false;
        }
    }
}



