using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EntityLayer;
using DataAccessLayer;  

namespace BusinessLayer
{
    public class SiparisManager
    {
        public static bool SatisiTamamlaBL(EntityLayer.Siparis s, DataTable sepet)
        {
            if (sepet == null || sepet.Rows.Count == 0) throw new Exception("Sepet bomboş! Önce ürün okutunuz.");
            if (s.Toplam_Tutar <= 0) throw new Exception("Toplam tutar 0 olamaz!");
            if (string.IsNullOrWhiteSpace(s.Odeme_Turu)) throw new Exception("Lütfen ödeme türünü seçiniz!");

            // Çalışan ID şimdilik sabit (Örn: 1). İleride Login ekranı yaparsan giriş yapan adamın ID'sini verirsin.
            s.Calisan_ID = EntityLayer.KullaniciSession.Calisan_ID;
            s.Siparis_Tarihi = DateTime.Now;

            return SiparisDAL.SatisiTamamla(s, sepet);
        }
        public static DataTable SiparisGecmisiniGetirBL()
        {
            return SiparisDAL.SiparisGecmisiniGetir();
        }
    }
}
