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

        public static Calisan GirisKontrolBL(string tc, string sifre)
        {
            if (string.IsNullOrWhiteSpace(tc) || string.IsNullOrWhiteSpace(sifre))
            {
                throw new Exception("TC Kimlik No veya Şifre boş bırakılamaz!");
            }

            // Kuralları geçtiyse veritabanına sor
            return CalisanDAL.GirisYap(tc, sifre);
        }

        public static int CalisanEkleBL(Calisan c)
        {
            if (string.IsNullOrWhiteSpace(c.Calisan_Ad) || string.IsNullOrWhiteSpace(c.Calisan_Soyad))
            {
                throw new Exception("Çalışanın Adı ve Soyadı boş bırakılamaz!");
            }
            if (c.Departman_ID <= 0)
            {
                throw new Exception("Lütfen geçerli bir departman seçiniz!");
            }
            return CalisanDAL.CalisanEkle(c);
        }

        public static string RastgeleSifreUret()
        {
            // Şifrede kullanılmasını istediğimiz havuz (Büyük harf, küçük harf ve rakamlar)
            string karakterler = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rastgele = new Random();
            string uretilenSifre = "";

            for (int i = 0; i < 6; i++)
            {
                uretilenSifre += karakterler[rastgele.Next(karakterler.Length)];
            }

            return uretilenSifre;
        }
        public static int SifreGuncelleBL(int id, string yeniSifre, string yeniSifreTekrar)
        {
            if (string.IsNullOrWhiteSpace(yeniSifre))
                throw new Exception("Şifre boş bırakılamaz!");

            if (yeniSifre != yeniSifreTekrar)
                throw new Exception("Girdiğiniz yeni şifreler birbiriyle uyuşmuyor!");

            if (yeniSifre.Length < 4)
                throw new Exception("Yeni şifreniz güvenlik sebebiyle en az 4 haneli olmalıdır.");

            return CalisanDAL.SifreGuncelle(id, yeniSifre);
        }
    }
}
