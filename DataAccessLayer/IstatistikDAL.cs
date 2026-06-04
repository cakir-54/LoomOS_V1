using System;
using System.Data;
using System.Data.SqlClient;

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
            return SQLBaglantisi.TekDegerGetir("SELECT COUNT(*) FROM Calisanlar WHERE ISNULL(Aktif_Mi, 1) = 1");
        }

        public static decimal GunlukCiroGetir(string odemeTuru = "")
        {
            string sorgu = "SELECT ISNULL(SUM(Toplam_Tutar), 0) FROM Siparisler WHERE CAST(Siparis_Tarihi AS DATE) = CAST(GETDATE() AS DATE)";

            if (!string.IsNullOrEmpty(odemeTuru))
                sorgu += " AND Odeme_Turu = @p1";

            using (SqlConnection baglanti = SQLBaglantisi.BaglantiOlustur())
            {
                baglanti.Open();
                using (SqlCommand cmd = new SqlCommand(sorgu, baglanti))
                {
                    if (!string.IsNullOrEmpty(odemeTuru))
                        cmd.Parameters.AddWithValue("@p1", odemeTuru);

                    object sonuc = cmd.ExecuteScalar();
                    if (sonuc == null || sonuc == DBNull.Value)
                        return 0;
                    return Convert.ToDecimal(sonuc);
                }
            }
        }

        public static DataTable KritikStoklariGetir()
        {
            string sorgu = "SELECT Envanter_ID, Urun_Adi, Beden, Renk, Stok_Adeti FROM VW_EnvanterDetay WHERE Stok_Adeti <= 5 ORDER BY Stok_Adeti ASC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, null);
        }
    }
}
