using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class IstatistikDAL
    {
        public static string ToplamMusteriSayisi()
        {
            return SQLBaglantisi.TekDegerGetir("SELECT COUNT(*) FROM Musteriler");
        }

        public static string ToplamCalisanSayisi()
        {
            return SQLBaglantisi.TekDegerGetir("SELECT COUNT(*) FROM Calisanlar");
        }
    }
}
