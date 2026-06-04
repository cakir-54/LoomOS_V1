using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class AlimDAL
    {
        public static DataTable AlimGecmisiGetir()
        {
            string sorgu = @"
                SELECT 
                    A.Alim_ID AS [İşlem No],
                    T.Firma_Adi AS [Tedarikçi],
                    U.Urun_Adi AS [Ürün],
                    A.Alim_Miktari AS [Adet],
                    A.Alis_Fiyati AS [Birim Fiyat],
                    A.Alim_Miktari * A.Alis_Fiyati AS [Toplam Tutar]
                FROM Alim_Hareketleri A
                INNER JOIN Tedarikciler T ON A.Tedarikci_ID = T.Tedarikci_ID
                INNER JOIN Envanter_Stoklar E ON  A.Envanter_ID=E.Envanter_ID
                INNER JOIN Urunler U ON E.Urun_ID = U.Urun_ID
                ORDER BY A.Alim_ID DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, null);
        }

        public static bool SatinAlimYap(int urunID, int tedarikciID, int miktar, decimal alisFiyati)
        {
            if (miktar <= 0 || alisFiyati <= 0)
                return false;

            using (SqlConnection baglanti = SQLBaglantisi.BaglantiOlustur())
            {
                baglanti.Open();
                SqlTransaction islem = baglanti.BeginTransaction();
                try
                {
                    string sorgu1 = @"
                IF EXISTS (SELECT 1 FROM Envanter_Stoklar WHERE Urun_ID = @uId)
                BEGIN
                    UPDATE Envanter_Stoklar SET Stok_Adeti = Stok_Adeti + @miktar WHERE Urun_ID = @uId;
                    SELECT Envanter_ID FROM Envanter_Stoklar WHERE Urun_ID = @uId;
                END
                ELSE
                BEGIN
                    INSERT INTO Envanter_Stoklar (Urun_ID, Stok_Adeti, Alis_Fiyati) VALUES (@uId, @miktar, @fiyat);
                    SELECT CAST(SCOPE_IDENTITY() AS int);
                END";

                    int envanterId;
                    using (SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti, islem))
                    {
                        cmd1.Parameters.AddWithValue("@uId", urunID);
                        cmd1.Parameters.AddWithValue("@miktar", miktar);
                        cmd1.Parameters.AddWithValue("@fiyat", alisFiyati);
                        envanterId = Convert.ToInt32(cmd1.ExecuteScalar());
                    }

                    string sorgu2 = "INSERT INTO Alim_Hareketleri (Envanter_ID, Tedarikci_ID, Alim_Miktari, Alis_Fiyati) VALUES (@eId, @tId, @miktar, @fiyat)";
                    using (SqlCommand cmd2 = new SqlCommand(sorgu2, baglanti, islem))
                    {
                        cmd2.Parameters.AddWithValue("@eId", envanterId);
                        cmd2.Parameters.AddWithValue("@tId", tedarikciID);
                        cmd2.Parameters.AddWithValue("@miktar", miktar);
                        cmd2.Parameters.AddWithValue("@fiyat", alisFiyati);
                        cmd2.ExecuteNonQuery();
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

        public static DataTable TedarikcileriGetir() =>
            SQLBaglantisi.SorguCalistirTablo("SELECT Tedarikci_ID, Firma_Adi FROM Tedarikciler", null);

        public static DataTable UrunleriGetir() =>
            SQLBaglantisi.SorguCalistirTablo("SELECT Urun_ID, Urun_Adi FROM Urunler", null);
    }
}
