using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class IadeDAL
    {
        public static DataTable FisiGetir(int siparisId)
        {
            string sorgu = @"
                SELECT 
                    SD.Siparis_Detay_ID,
                    SD.Siparis_ID,
                    SD.Envanter_ID, 
                    U.Urun_Adi AS [Ürün Adı], 
                    SD.Miktar AS [Satılan Adet], 
                    SD.Birim_Fiyati AS [Birim Fiyat],
                    (SD.Miktar * SD.Birim_Fiyati) AS [Toplam Tutar]
                FROM Siparis_Detaylari SD
                INNER JOIN Envanter_Stoklar E ON SD.Envanter_ID = E.Envanter_ID
                INNER JOIN Urunler U ON E.Urun_ID = U.Urun_ID
                WHERE SD.Siparis_ID = @sId";

            SqlParameter[] prm = { new SqlParameter("@sId", siparisId) };
            return SQLBaglantisi.SorguCalistirTablo(sorgu, prm);
        }

        public static bool IadeIsleminiYap(int siparisDetayId, int siparisId, int envanterId, int miktar, decimal iadeTutari, string iadeNedeni)
        {
            if (miktar <= 0 || iadeTutari <= 0)
                return false;

            using (SqlConnection baglanti = SQLBaglantisi.BaglantiOlustur())
            {
                baglanti.Open();
                SqlTransaction islem = baglanti.BeginTransaction();

                try
                {
                    string sorgu1 = "INSERT INTO Iadeler (Siparis_Detay_ID, Iade_Miktari, Iade_Tutari, Iade_Nedeni) VALUES (@sdId, @miktar, @tutar, @neden)";
                    using (SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti, islem))
                    {
                        cmd1.Parameters.AddWithValue("@sdId", siparisDetayId);
                        cmd1.Parameters.AddWithValue("@miktar", miktar);
                        cmd1.Parameters.AddWithValue("@tutar", iadeTutari);
                        cmd1.Parameters.AddWithValue("@neden", iadeNedeni);
                        cmd1.ExecuteNonQuery();
                    }

                    string sorgu2 = "UPDATE Envanter_Stoklar SET Stok_Adeti = Stok_Adeti + @miktar WHERE Envanter_ID = @eId";
                    using (SqlCommand cmd2 = new SqlCommand(sorgu2, baglanti, islem))
                    {
                        cmd2.Parameters.AddWithValue("@miktar", miktar);
                        cmd2.Parameters.AddWithValue("@eId", envanterId);
                        cmd2.ExecuteNonQuery();
                    }

                    string sorgu3 = "UPDATE Siparisler SET Toplam_Tutar = Toplam_Tutar - @tutar WHERE Siparis_ID = @sId AND Toplam_Tutar >= @tutar";
                    using (SqlCommand cmd3 = new SqlCommand(sorgu3, baglanti, islem))
                    {
                        cmd3.Parameters.AddWithValue("@tutar", iadeTutari);
                        cmd3.Parameters.AddWithValue("@sId", siparisId);
                        if (cmd3.ExecuteNonQuery() == 0)
                            throw new Exception("Sipariş tutarı güncellenemedi.");
                    }

                    islem.Commit();
                    return true;
                }
                catch
                {
                    islem.Rollback();
                    return false;
                }
            }
        }
    }
}
