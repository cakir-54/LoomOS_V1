using System;
using System.Collections.Generic;
using DataAccessLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class CalisanManager
    {
        public static List<Calisan> CalisanListeleBL()
        {
            return CalisanDAL.CalisanListele();
        }

        public static Calisan GirisKontrolBL(string tc, string sifre)
        {
            if (string.IsNullOrWhiteSpace(tc) || string.IsNullOrWhiteSpace(sifre))
                throw new Exception("TC Kimlik No veya Şifre boş bırakılamaz!");

            if (tc.Trim().Length != 11)
                throw new Exception("TC Kimlik Numarası 11 haneli olmalıdır!");

            return CalisanDAL.GirisYap(tc.Trim(), sifre);
        }

        public static int CalisanEkleBL(Calisan c)
        {
            if (string.IsNullOrWhiteSpace(c.Calisan_Ad) || string.IsNullOrWhiteSpace(c.Calisan_Soyad))
                throw new Exception("Çalışanın Adı ve Soyadı boş bırakılamaz!");

            if (string.IsNullOrWhiteSpace(c.Calisan_TC) || c.Calisan_TC.Trim().Length != 11)
                throw new Exception("Geçerli bir 11 haneli TC Kimlik Numarası giriniz!");

            if (c.Departman_ID <= 0)
                throw new Exception("Lütfen geçerli bir departman seçiniz!");

            return CalisanDAL.CalisanEkle(c);
        }

        public static string RastgeleSifreUret()
        {
            string karakterler = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rastgele = new Random();
            string uretilenSifre = "";

            for (int i = 0; i < 6; i++)
                uretilenSifre += karakterler[rastgele.Next(karakterler.Length)];

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

        /// <summary>Yalnızca yönetim (Departman_ID = 1) maaş güncelleyebilir.</summary>
        public static bool MaasGuncelleBL(string tcNo, decimal yeniMaas)
        {
            if (KullaniciSession.Departman_ID != 1)
                throw new Exception("Maaş güncelleme yetkiniz bulunmuyor. (Yalnızca yönetim)");

            if (string.IsNullOrWhiteSpace(tcNo) || tcNo.Trim().Length != 11)
                throw new Exception("Geçerli bir 11 haneli TC Kimlik Numarası giriniz!");

            if (yeniMaas <= 0)
                throw new Exception("Maaş tutarı 0 veya daha düşük olamaz!");

            return CalisanDAL.MaasGuncelle(tcNo.Trim(), yeniMaas);
        }

        /// <summary>Yalnızca yönetim (Departman_ID = 1) işten çıkarma yapabilir.</summary>
        public static bool IstenCikarBL(string tcNo)
        {
            if (KullaniciSession.Departman_ID != 1)
                throw new Exception("İşten çıkarma yetkiniz bulunmuyor. (Yalnızca yönetim)");

            if (string.IsNullOrWhiteSpace(tcNo) || tcNo.Trim().Length != 11)
                throw new Exception("Geçerli bir 11 haneli TC Kimlik Numarası giriniz!");

            return CalisanDAL.IstenCikar(tcNo.Trim());
        }
    }
}
