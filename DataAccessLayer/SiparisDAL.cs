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
                string sorguSiparis = "INSERT INTO Siparis (Musteri_ID, Calisan_ID, Siparis_Tarihi, Toplam_Tutar, Odeme_Turu) OUTPUT INSERTED.Siparis_ID VALUES (@p1, @p2, @p3, @p4, @p5)";
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
                    string sorguDetay = "INSERT INTO Siparis_Detay (Siparis_ID, Envanter_ID, Miktar, Birim_Fiyati) VALUES (@d1, @d2, @d3, @d4)";
                    SqlCommand cmdDetay = new SqlCommand(sorguDetay, baglanti, islem);
                    cmdDetay.Parameters.AddWithValue("@d1", yeniSiparisID);
                    cmdDetay.Parameters.AddWithValue("@d2", satir["Envanter_ID"]);
                    cmdDetay.Parameters.AddWithValue("@d3", satir["Miktar"]);
                    cmdDetay.Parameters.AddWithValue("@d4", satir["Birim_Fiyati"]);
                    cmdDetay.ExecuteNonQuery();

                    // Envanterden (Stoktan) Düş
                    string sorguStok = "UPDATE EnvanterStoklar SET Stok_Adeti = Stok_Adeti - @s1 WHERE Envanter_ID = @s2";
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

    }
}
