using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

using EntityLayer;
using System.Data;
using System.Security.Cryptography.X509Certificates;


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
                INNER JOIN Urunler U ON A.Urun_ID = U.Urun_ID
                ORDER BY A.Alim_ID DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, new SqlParameter[0]);
        }
        public static bool SatinAlimYap(int urunID, int tedarikciID, int miktar, decimal alisFiyati)
        {
            SqlConnection baglanti = SQLBaglantisi.BaglantiGetir();
            SqlTransaction islem = baglanti.BeginTransaction();
            try
            {
                // Alım hareketlerini ekle
                string sorgu1 = "INSERT INTO Alim_Hareketleri (Urun_ID, Tedarikci_ID, Alim_Miktari, Alis_Fiyati) VALUES (@uId, @tId, @miktar, @fiyat)";
                SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti, islem);
                cmd1.Parameters.AddWithValue("@uId", urunID);
                cmd1.Parameters.AddWithValue("@tId", tedarikciID);
                cmd1.Parameters.AddWithValue("@miktar", miktar);
                cmd1.Parameters.AddWithValue("@fiyat", alisFiyati);
                cmd1.ExecuteNonQuery();
                // Stokları güncelle
                string sorgu2 = @"
                        IF EXISTS (SELECT 1 FROM Envanter_Stoklar WHERE Urun_ID = @uId)
                            UPDATE Envanter_Stoklar SET Stok_Adeti = Stok_Adeti + @miktar WHERE Urun_ID = @uId
                        ELSE
                            INSERT INTO Envanter_Stoklar (Urun_ID, Stok_Adeti) VALUES (@uId, @miktar)";
                SqlCommand cmd2 = new SqlCommand(sorgu2, baglanti, islem);
                cmd2.Parameters.AddWithValue("@uId", urunID);
                cmd2.Parameters.AddWithValue("@miktar", miktar);
                cmd2.ExecuteNonQuery();
                //Ürünün alış fiyatını güncelle
                string sorgu3 = "UPDATE Urunler SET Alis_Fiyati = @fiyat WHERE Urun_ID = @uId";
                SqlCommand cmd3 = new SqlCommand(sorgu3, baglanti, islem);
                cmd3.Parameters.AddWithValue("@fiyat", alisFiyati);
                cmd3.Parameters.AddWithValue("@uId", urunID);
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
        // Yardımcı Metotlar (ComboBox'ları doldurmak için)
        public static DataTable TedarikcileriGetir() => SQLBaglantisi.SorguCalistirTablo("SELECT Tedarikci_ID, Firma_Adi FROM Tedarikciler", new SqlParameter[0]);
        public static DataTable UrunleriGetir() => SQLBaglantisi.SorguCalistirTablo("SELECT Urun_ID, Urun_Adi FROM Urunler", new SqlParameter[0]);
    }
}
    

