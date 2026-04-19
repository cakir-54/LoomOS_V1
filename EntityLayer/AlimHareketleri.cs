using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer
{
    public class AlimHareketi
    {
        public int Alim_ID { get; set; }
        public int Tedarikci_ID { get; set; }
        public int Envanter_ID { get; set; }
        public int Alim_Miktari { get; set; }
        public decimal Alis_Fiyati { get; set; }
        public DateTime Alis_Tarihi { get; set; }
    }

}
