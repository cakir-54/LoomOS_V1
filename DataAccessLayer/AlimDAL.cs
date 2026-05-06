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
                INNER JOIN Envanter_Stoklar E ON  A.Envanter_ID=E.Envanter_ID
                INNER JOIN Urunler U ON E   .Urun_ID = U.Urun_ID
                ORDER BY A.Alim_ID DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, new SqlParameter[0]);
        }
        public static bool SatinAlimYap(int urunID, int tedarikciID, int miktar, decimal alisFiyati)
        {
            SqlConnection baglanti = SQLBaglantisi.BaglantiGetir();
            SqlTransaction islem = baglanti.BeginTransaction();
            try
            {
                //Envanteri Güncelle/Ekle ve Envanter_ID'yi Geri Ver
                string sorgu1 = @"
                IF EXISTS (SELECT 1 FROM Envanter_Stoklar WHERE Urun_ID = @uId)
                BEGIN
                    UPDATE Envanter_Stoklar SET Stok_Adeti = Stok_Adeti + @miktar WHERE Urun_ID = @uId;
                    SELECT Envanter_ID FROM Envanter_Stoklar WHERE Urun_ID = @uId;
                END

                ELSE
                BEGIN
                    INSERT INTO Envanter_Stoklar (Urun_ID, Stok_Adeti) VALUES (@uId, @miktar);
                    SELECT SCOPE_IDENTITY(); -- SQL'in otomatik verdiği yeni ID'yi yakala
                END";

                SqlCommand cmd1 = new SqlCommand(sorgu1, baglanti, islem);
                cmd1.Parameters.AddWithValue("@uId", urunID);
                cmd1.Parameters.AddWithValue("@miktar", miktar);
                // ExecuteScalar, sorgudan dönen İLK satırın İLK sütununu (Yani Envanter_ID'yi) C#'a integer olarak çeker!
                int envanterId = Convert.ToInt32(cmd1.ExecuteScalar());
                //Alım Hareketlerine kaydet
                string sorgu2 = "INSERT INTO Alim_Hareketleri (Envanter_ID, Tedarikci_ID, Alim_Miktari, Alis_Fiyati) VALUES (@eId, @tId, @miktar, @fiyat)";
                SqlCommand cmd2 = new SqlCommand(sorgu2, baglanti, islem);
                cmd2.Parameters.AddWithValue("@eId", envanterId); // Bulduğumuz ID'yi buraya veriyoruz
                cmd2.Parameters.AddWithValue("@tId", tedarikciID);
                cmd2.Parameters.AddWithValue("@miktar", miktar);
                cmd2.Parameters.AddWithValue("@fiyat", alisFiyati);
                cmd2.ExecuteNonQuery();
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
    

