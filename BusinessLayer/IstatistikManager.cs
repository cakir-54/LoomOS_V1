using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class IstatistikManager
    {
        public static string ToplamMusteriBL()
        {
            return IstatistikDAL.ToplamMusteriSayisi();
        }

        public static string ToplamCalisanBL()
        {
            return IstatistikDAL.ToplamCalisanSayisi();
        }
    }
}
