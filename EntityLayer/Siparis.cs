using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class Siparis
    {
        public int Siparis_ID { get; set; }
        public int? Musteri_ID { get; set; }
        public int Calisan_ID { get; set; }
        public DateTime Siparis_Tarihi { get; set; }
        public string Odeme_Turu { get; set; }
        public decimal Toplam_Tutar { get; set; }
    }
}
