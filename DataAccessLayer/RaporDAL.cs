using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class RaporDAL
    {
        //Bu günün satış özetini hseapla ve getir
        public static DataTable BugununOzetiniGetir()
        {
            string sorgu = @" SELECT
                COUNT(Siparis_ID) AS FisSayisi,
                ISNULL(SUM(Toplam_Tutar),0)AS Ciro,
                ISNULL(SUM(CASE WHEN Odeme_Turu='Nakit' THEN Toplam_Tutar ELSE 0 END ))AS NakitToplam,
                ISNULL(SUM(CASE WHEN Odeme_Turu = 'Kredi Kartı' THEN Toplam_Tutar ELSE 0 END), 0) AS KartToplam,
            FROM Siparis
            WHERE CAST(Satis_Tarihi AS DATE)=CAST(GETDATE() AS DATE)";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, new SqlParameter[0]);
        }
        //Kasayı kapat ve Geçmişe at
        public static bool Z_RaporuKaydet(int fisSayisi,decimal ciro,decimal nakit,decimal kart)
        {
            SqlConnection baglanti=SQLBaglantisi.BaglantiGetir();
            string sorgu = @"INSERT INTO GunSonuRaporlari (Toplam_Fis_Sayisi, Toplam_Ciro, Nakit_Toplam, Kart_Toplam) 
                                 VALUES (@fis, @ciro, @nakit, @kart)";
            SqlCommand cmd = new SqlCommand(sorgu, baglanti);
            cmd.Parameters.AddWithValue("@fis", fisSayisi);
            cmd.Parameters.AddWithValue("@ciro", ciro);
            cmd.Parameters.AddWithValue("@nakit", nakit);
            cmd.Parameters.AddWithValue("@kart", kart);
            return cmd.ExecuteNonQuery() > 0;
        }
        //Geçmiş raporları tabloya dökme
        public static DataTable GecmisRaporlariGetir()
        {
            string sorgu = "SELECT * FROM GunSonuRaporlari ORDER BY Rapor_Tarihi DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, new SqlParameter[0]);
        }
    }
}
