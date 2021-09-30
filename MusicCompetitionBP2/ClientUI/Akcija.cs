using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class Akcija
    {
        private string naziv;
        Object objekat;

        public Akcija(string naziv, object objekat)
        {
            Naziv = naziv;
            Objekat = objekat;
        }

        public string Naziv { get => naziv; set => naziv = value; }
        public object Objekat { get => objekat; set => objekat = value; }
    }
}
