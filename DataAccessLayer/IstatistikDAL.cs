using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EntityLayer;


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
        public static decimal GunlukCiroGetir(string odemeTuru = "")// İsteğe bağlı olarak ödeme türüne göre günlük ciroyu getiren bir metot. Eğer ödeme türü belirtilmezse tüm satışların cirosunu getirir.
        {
            decimal ciro = 0;
            SqlConnection baglanti=SQLBaglantisi.BaglantiGetir();
            // O günkü satışların toplamını alıyoruz. Satış yoksa 0 dönsün diye ISNULL kullandık
            string sorgu = "SELECT ISNULL(SUM(Toplam_Tutar), 0) FROM Siparisler WHERE CAST(Siparis_Tarihi AS DATE) = CAST(GETDATE() AS DATE)";

            if (!string.IsNullOrEmpty(odemeTuru))
            {
                sorgu += " AND Odeme_Turu = @p1";
            }

            SqlCommand cmd = new SqlCommand(sorgu, baglanti);

            if (!string.IsNullOrEmpty(odemeTuru))
            {
                cmd.Parameters.AddWithValue("@p1", odemeTuru);
            }
            ciro = Convert.ToDecimal(cmd.ExecuteScalar());
            return ciro;
        }
        public static System.Data.DataTable KritikStoklariGetir()// Stok adeti 5 veya daha az olan ürünleri getiriyoruz
        {
            string sorgu = "SELECT Envanter_ID, Urun_Adi, Beden, Renk, Stok_Adeti FROM VW_EnvanterDetay WHERE Stok_Adeti <= 5 ORDER BY Stok_Adeti ASC";
            SqlParameter[] bosParametre = new SqlParameter[0];

            return SQLBaglantisi.SorguCalistirTablo(sorgu, bosParametre);
        }
    }
}
