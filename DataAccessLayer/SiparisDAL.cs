using EntityLayer;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLayer
{
    public class SiparisDAL
    {
        public static bool SatisiTamamla(Siparis s,DataTable sepet)
        {
            SqlConnection baglanti=SQLBaglantisi.BaglantiGetir();
            //İşlem yapılırken bir sorun oluşursa geri alma işlemi yapabilmek için transaction başlatıyoruz
            SqlTransaction islem=baglanti.BeginTransaction();
            try
            {
                //Sipariş tablosuna ekleme yaparken, eklenen siparişin ID'sini almak için OUTPUT INSERTED.Siparis_ID ifadesini kullanıyoruz
                string sorguSiparis = "INSERT INTO Siparisler (Musteri_ID, Calisan_ID, Siparis_Tarihi, Toplam_Tutar, Odeme_Turu) OUTPUT INSERTED.Siparis_ID VALUES (@p1, @p2, @p3, @p4, @p5)";
                SqlCommand cmdSiparis = new SqlCommand(sorguSiparis, baglanti, islem);
                //Müsteri_ID null olabilir, bu yüzden DBNull.Value kullanarak parametre ekliyoruz
                if (s.Musteri_ID == null || s.Musteri_ID == 0)
                    cmdSiparis.Parameters.AddWithValue("@p1", DBNull.Value);
                else
                    cmdSiparis.Parameters.AddWithValue("@p1", s.Musteri_ID);
                //Geriye kalan parametreler null olamayacağı için direkt ekliyoruz
                cmdSiparis.Parameters.AddWithValue("@p2", s.Calisan_ID);
                cmdSiparis.Parameters.AddWithValue("@p3", s.Siparis_Tarihi);
                cmdSiparis.Parameters.AddWithValue("@p4", s.Toplam_Tutar);
                cmdSiparis.Parameters.AddWithValue("@p5", s.Odeme_Turu);
                

                // ExecuteScalar, oluşan yeni Siparis_ID'yi bize geri döndürür!
                int yeniSiparisID = (int)cmdSiparis.ExecuteScalar();

                //Sipariş detayları sepetten tek tek eklenirken, aynı zamanda stoktan düşme işlemi de yapılır
                foreach (DataRow satir in sepet.Rows)
                {
                    // Sipariş Detayına Ekle
                    string sorguDetay = "INSERT INTO Siparis_Detaylari (Siparis_ID, Envanter_ID, Miktar, Birim_Fiyati) VALUES (@d1, @d2, @d3, @d4)";
                    SqlCommand cmdDetay = new SqlCommand(sorguDetay, baglanti, islem);
                    cmdDetay.Parameters.AddWithValue("@d1", yeniSiparisID);
                    cmdDetay.Parameters.AddWithValue("@d2", satir["Envanter_ID"]);
                    cmdDetay.Parameters.AddWithValue("@d3", satir["Miktar"]);
                    cmdDetay.Parameters.AddWithValue("@d4", satir["Birim_Fiyati"]);
                    cmdDetay.ExecuteNonQuery();

                    // Envanterden (Stoktan) Düş
                    string sorguStok = "UPDATE Envanter_Stoklar SET Stok_Adeti = Stok_Adeti - @s1 WHERE Envanter_ID = @s2";
                    SqlCommand cmdStok = new SqlCommand(sorguStok, baglanti, islem);
                    cmdStok.Parameters.AddWithValue("@s1", satir["Miktar"]);
                    cmdStok.Parameters.AddWithValue("@s2", satir["Envanter_ID"]);
                    cmdStok.ExecuteNonQuery();
                }

                // 3. HER ŞEY KUSURSUZSA İŞLEMİ ONAYLA (Veritabanına kalıcı yaz)
                islem.Commit();
                return true;
            }
            catch(Exception)
            {
                islem.Rollback();
                throw;
            }
        }
        public static DataTable SiparisGecmisiniGetir()
        {
            string sorgu = @"SELECT s.Siparis_ID AS [Fiş No],ISNULL(M.Ad+' '+M.Soyad, 'Genel Müşteri') AS [Müşteri],C.Ad+'  '+C.Soyad AS [Kasiyer],S.Siparis_Tarihi AS[Tarih],S.Odeme_Turu AS [Ödeme Tipi],S.Toplam_Tutar AS [Tutar] 
                FROM Siparisler S 
                LEFT JOIN Musteriler M ON S.Musteri_ID =M.Musteri_ID
                INNER JOIN Calisanlar C ON S.Calisan_ID=C.Calisan_ID
                ORDER BY S.Siparis_Tarihi DESC";
            SqlParameter[] bos= new SqlParameter[0];
            return SQLBaglantisi.SorguCalistirTablo(sorgu, bos);
        }

    }
}
