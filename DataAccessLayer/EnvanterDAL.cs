using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class EnvanterDAL
    {
        public static DataTable EnvanterListeleTablo()
        {
            // O harika sanal tablomuzu (VIEW) çağırıyoruz
            return SQLBaglantisi.SorguCalistirTablo("SELECT * FROM VW_EnvanterDetay", null);
        }
        public static int StokVaryantEkle(EntityLayer.EnvanterStok e)
        {
            SqlParameter[] prm = {
        new SqlParameter("@p1", e.Urun_ID),
        new SqlParameter("@p2", e.Beden_ID),
        new SqlParameter("@p3", e.Renk),
        new SqlParameter("@p4", e.Stok_Adeti),
        new SqlParameter("@p5", e.Alis_Fiyati),
        new SqlParameter("@p6", e.Satis_Fiyati)
            };

            string sorgu = "INSERT INTO Envanter_Stoklar (Urun_ID, Beden_ID, Renk, Stok_Adeti, Alis_Fiyati, Satis_Fiyati) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";

            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm);
        }
    }
}
