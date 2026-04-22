using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class CalisanManager
    {
        public static List<Calisan> CalisanListeleBL()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return CalisanDAL.CalisanListele();
        }
    }
}
