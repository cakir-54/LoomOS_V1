    using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class EnvanterStok
    {
        public int Envanter_ID { get; set; }
        public int Urun_ID { get; set; }
        public int Stok_Adeti { get; set; }
        public decimal Satis_Fiyati { get; set; }
        public decimal Alis_Fiyati { get; set; }

        public int Beden_ID { get; set; }
        public string Renk { get; set; }
    }



}
