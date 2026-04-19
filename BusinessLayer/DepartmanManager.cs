using System;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;

namespace BusinessLayer
{
    public class DepartmanManager
    {
        public static List<Departman> DepartmanListele()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return DepartmanDAL.DepartmanListele();
        }

    }
}
