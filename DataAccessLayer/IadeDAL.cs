using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class IadeDAL
    {
        // 1. FİŞ NUMARASINA GÖRE SATIŞ DETAYLARINI GETİR
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

            using (SqlConnection baglanti = SQLBaglantisi.BaglantiGetir())
            {
                SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@sId", siparisId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. KUSURSUZ İADE İŞLEMİ (TRANSACTION)
        public static bool IadeIsleminiYap(int siparisDetayId, int siparisId, int envanterId, int miktar, decimal iadeTutari, string iadeNedeni)
        {
            using (SqlConnection baglanti = SQLBaglantisi.BaglantiGetir())
            {
                baglanti.Open();
                SqlTransaction islem = baglanti.BeginTransaction();

                try
                {
                    string sorgu1 = "INSERT INTO Iadeler (Siparis_Detay_ID, Iade_Miktari, Iade_Tutari, Iade_Nedeni) VALUES (@sdId, @miktar, @tutar, @neden)";
                    SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti, islem);
                    cmd1.Parameters.AddWithValue("@sdId", siparisDetayId);
                    cmd1.Parameters.AddWithValue("@miktar", miktar);
                    cmd1.Parameters.AddWithValue("@tutar", iadeTutari);
                    cmd1.Parameters.AddWithValue("@neden", iadeNedeni);
                    cmd1.ExecuteNonQuery();

                    // ürünün stoğunu geri artır
                    string sorgu2 = "UPDATE ENVANTER_STOKLAR SET Stok_Miktari = Stok_Miktari + @miktar WHERE Envanter_ID = @eId";
                    SqlCommand cmd2 = new SqlCommand(sorgu2, baglanti, islem);
                    cmd2.Parameters.AddWithValue("@miktar", miktar);
                    cmd2.Parameters.AddWithValue("@eId", envanterId);
                    cmd2.ExecuteNonQuery();

                    //  Kasanın o günkü genel toplamından bu tutarı eksilt
                    string sorgu3 = "UPDATE Siparisler SET Toplam_Tutar = Toplam_Tutar - @tutar WHERE Siparis_ID = @sId";
                    SqlCommand cmd3 = new SqlCommand(sorgu3, baglanti, islem);
                    cmd3.Parameters.AddWithValue("@tutar", iadeTutari);
                    cmd3.Parameters.AddWithValue("@sId", siparisId);
                    cmd3.ExecuteNonQuery();
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
