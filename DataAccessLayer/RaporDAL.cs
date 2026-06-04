using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class RaporDAL
    {
        public static DataTable BugununOzetiniGetir()
        {
            using (SqlConnection baglanti = SQLBaglantisi.BaglantiOlustur())
            {
                baglanti.Open();
                using (SqlCommand cmd = new SqlCommand("sp_BugununOzetiniGetir", baglanti))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static bool Z_RaporuKaydet(int fisSayisi, decimal ciro, decimal nakit, decimal kart)
        {
            string sorgu = @"INSERT INTO GunSonuRaporlari (Toplam_Fis_Sayisi, Toplam_Ciro, Nakit_Toplam, Kart_Toplam) 
                                 VALUES (@fis, @ciro, @nakit, @kart)";
            SqlParameter[] prm = {
                new SqlParameter("@fis", fisSayisi),
                new SqlParameter("@ciro", ciro),
                new SqlParameter("@nakit", nakit),
                new SqlParameter("@kart", kart)
            };
            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm) > 0;
        }

        public static DataTable GecmisRaporlariGetir()
        {
            string sorgu = "SELECT * FROM vw_GecmisZRaporlari ORDER BY [Kapatma Tarihi] DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, null);
        }
    }
}
