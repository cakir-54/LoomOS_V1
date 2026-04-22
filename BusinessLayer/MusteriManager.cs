using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class MusteriManager
    {
        public static List<Musteri> MusteriListeleBL()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return MusteriDAL.MusteriListele();
        }

        // Arama isteği gelirse onu da yönlendiriyoruz
        public static List<Musteri> MusteriAraBL(string kelime)
        {
            // 1. KURAL: Kullanıcı kutuyu boş bırakıp butona bastıysa
            if (string.IsNullOrWhiteSpace(kelime))
            {
                // UI katmanına (Forma) hata fırlatıyoruz
                throw new Exception("Lütfen aramak için bir müşteri adı giriniz!");
            }

            // 2. KURAL: Kullanıcı çok kısa bir harf (örn: sadece "A") girdiyse (Sunucuyu yormamak için)
            if (kelime.Length < 2)
            {
                throw new Exception("Arama yapabilmek için en az 2 harf girmelisiniz!");
            }
            return MusteriDAL.MusteriAra(kelime);
        }
    }
}
