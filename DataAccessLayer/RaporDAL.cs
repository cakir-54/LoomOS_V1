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
        public static DataTable BugununOzetiniGetir()//BUraya sonradan bakacağım ~
        {
            using (SqlConnection baglanti = SQLBaglantisi.BaglantiGetir())
            {
                SqlCommand cmd = new SqlCommand("sp_BugununOzetiniGetir", baglanti);

                // SQL'e bunun normal bir sorgu değil, bir Stored Procedure olduğunu söylüyoruz.
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
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
            string sorgu = "SELECT * FROM vw_GecmisZRaporlari ORDER BY [Kapatma Tarihi] DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, new SqlParameter[0]);
        }
    }
}
