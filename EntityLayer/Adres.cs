using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class Adres
    {
        public int Adres_ID { get; set; }
        public int Musteri_ID { get; set; }
        public string Adres_Basligi { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Sokak_Cadde { get; set; }
    }

}
