using System;
using System.Collections.Generic;
using System.Text;

namespace Hypo_Banka
{
    public interface IZahtjev
    {
        bool DaLiJePovoljan();
    }
    public class Zahtjev : IZahtjev
    {
        public bool DaLiJePovoljan()
        {
            throw new NotImplementedException();
        }
    }
}
