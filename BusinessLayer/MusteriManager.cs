using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class MusteriManager
    {
        public static DataTable MusteriListeleBL()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return MusteriDAL.MusterileriListele();
        }

        // Arama isteği gelirse onu da yönlendiriyoruz
        public static DataTable MusteriAraBL(string arananKelime)
        {
            if (string.IsNullOrWhiteSpace(arananKelime))
                return MusteriDAL.MusterileriListele();

            arananKelime = arananKelime.Trim();
            if (arananKelime.Length < 2)
                throw new System.Exception("Arama için en az 2 karakter giriniz!");

            return MusteriDAL.MusteriAra(arananKelime);
        }
        public static bool MusteriEkleBL(string ad, string soyad, string telefon, string email)
        {
            if (string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(soyad))
            {
                throw new System.Exception("Müşteri adı ve soyadı boş bırakılamaz!");
            }

            ad = ad.Trim().ToUpper();
            soyad = soyad.Trim().ToUpper();

            // Opsiyonel: Eğer e-posta yazılmışsa içinde '@' var mı diye kontrol edebiliriz
            if (!string.IsNullOrWhiteSpace(email) && !email.Contains("@"))
            {
                throw new System.Exception("Lütfen geçerli bir e-posta adresi giriniz!");
            }

            return DataAccessLayer.MusteriDAL.MusteriEkle(ad, soyad, telefon, email);
        }

    }
}
