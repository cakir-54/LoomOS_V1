using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class UrunManager
    {
       public static List<Urun> UrunListeleBL()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return UrunDAL.UrunListele();
        }
    }
}
