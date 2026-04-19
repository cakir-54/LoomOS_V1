using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class SiparisDetay
    {
        public int Siparis_Detay_ID { get; set; }
        public int Siparis_ID { get; set; }
        public int Envanter_ID { get; set; }
        public int Miktar { get; set; }
        public decimal Birim_Fiyati { get; set; }
    }
}
