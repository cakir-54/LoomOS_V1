using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class EkGelir
    {
        public int Gelir_ID { get; set; }
        public int Gelir_Tipi_ID { get; set; }
        public decimal Tutar { get; set; }
        public DateTime Gelir_Tarihi { get; set; }

    }
}
