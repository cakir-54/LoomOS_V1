using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class Calisan
    {
        public int Calisan_ID { get; set; }
        public int Departman_ID { get; set; }

        public string Departman_Adi { get; set; }
        public string Calisan_Ad { get; set; }
        public string Calisan_Soyad { get; set; }
        public string Calisan_TC { get; set; }
        public string Calisan_TelNO { get; set; }
        public decimal Calisan_Maas { get; set; }
        public DateTime Giris_Tarihi { get; set; }
        public DateTime? Cikis_Tarihi_null { get; set; }
        public string Sifre { get; set;}


    }
}
