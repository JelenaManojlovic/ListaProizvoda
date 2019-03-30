using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListaProizvoda.Models
{
    public class Proizvodi
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Proizvodjac { get; set; }
        public string Dobavljac { get; set; }
        public int Cena { get; set; }
    }
}