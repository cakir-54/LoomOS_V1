using System;
using System.Data;
using EntityLayer;
using DataAccessLayer;

namespace BusinessLayer
{
    public class SiparisManager
    {
        public static bool SatisiTamamlaBL(Siparis s, DataTable sepet)
        {
            if (sepet == null || sepet.Rows.Count == 0)
                throw new Exception("Sepet bomboş! Önce ürün okutunuz.");

            if (s.Toplam_Tutar <= 0)
                throw new Exception("Toplam tutar 0 olamaz!");

            if (string.IsNullOrWhiteSpace(s.Odeme_Turu))
                throw new Exception("Lütfen ödeme türünü seçiniz!");

            if (KullaniciSession.Calisan_ID <= 0)
                throw new Exception("Oturum geçersiz. Lütfen yeniden giriş yapınız.");

            s.Calisan_ID = KullaniciSession.Calisan_ID;
            s.Siparis_Tarihi = DateTime.Now;

            return SiparisDAL.SatisiTamamla(s, sepet);
        }

        public static DataTable SiparisGecmisiniGetirBL()
        {
            return SiparisDAL.SiparisGecmisiniGetir();
        }
    }
}
