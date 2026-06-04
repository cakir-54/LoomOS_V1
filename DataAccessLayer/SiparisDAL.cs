using EntityLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SiparisDAL
    {
        public static bool SatisiTamamla(Siparis s, DataTable sepet)
        {
            using (SqlConnection baglanti = SQLBaglantisi.BaglantiOlustur())
            {
                baglanti.Open();
                SqlTransaction islem = baglanti.BeginTransaction();
                try
                {
                    foreach (DataRow satir in sepet.Rows)
                    {
                        int envanterId = Convert.ToInt32(satir["Envanter_ID"]);
                        int istenenMiktar = Convert.ToInt32(satir["Miktar"]);
                        if (istenenMiktar <= 0)
                            throw new Exception("Sepette geçersiz miktar var.");

                        string sorguStokKontrol = "SELECT Stok_Adeti FROM Envanter_Stoklar WHERE Envanter_ID = @eId";
                        using (SqlCommand cmdKontrol = new SqlCommand(sorguStokKontrol, baglanti, islem))
                        {
                            cmdKontrol.Parameters.AddWithValue("@eId", envanterId);
                            object stokSonuc = cmdKontrol.ExecuteScalar();
                            if (stokSonuc == null || stokSonuc == DBNull.Value)
                                throw new Exception("Ürün stok kaydı bulunamadı.");

                            int mevcutStok = Convert.ToInt32(stokSonuc);
                            if (mevcutStok < istenenMiktar)
                                throw new Exception($"Yetersiz stok! (Envanter #{envanterId}) Mevcut: {mevcutStok}, İstenen: {istenenMiktar}");
                        }
                    }

                    string sorguSiparis = "INSERT INTO Siparisler (Musteri_ID, Calisan_ID, Siparis_Tarihi, Toplam_Tutar, Odeme_Turu) OUTPUT INSERTED.Siparis_ID VALUES (@p1, @p2, @p3, @p4, @p5)";
                    using (SqlCommand cmdSiparis = new SqlCommand(sorguSiparis, baglanti, islem))
                    {
                        if (s.Musteri_ID == null || s.Musteri_ID == 0)
                            cmdSiparis.Parameters.AddWithValue("@p1", DBNull.Value);
                        else
                            cmdSiparis.Parameters.AddWithValue("@p1", s.Musteri_ID);

                        cmdSiparis.Parameters.AddWithValue("@p2", s.Calisan_ID);
                        cmdSiparis.Parameters.AddWithValue("@p3", s.Siparis_Tarihi);
                        cmdSiparis.Parameters.AddWithValue("@p4", s.Toplam_Tutar);
                        cmdSiparis.Parameters.AddWithValue("@p5", s.Odeme_Turu);

                        int yeniSiparisID = (int)cmdSiparis.ExecuteScalar();

                        foreach (DataRow satir in sepet.Rows)
                        {
                            string sorguDetay = "INSERT INTO Siparis_Detaylari (Siparis_ID, Envanter_ID, Miktar, Birim_Fiyati) VALUES (@d1, @d2, @d3, @d4)";
                            using (SqlCommand cmdDetay = new SqlCommand(sorguDetay, baglanti, islem))
                            {
                                cmdDetay.Parameters.AddWithValue("@d1", yeniSiparisID);
                                cmdDetay.Parameters.AddWithValue("@d2", satir["Envanter_ID"]);
                                cmdDetay.Parameters.AddWithValue("@d3", satir["Miktar"]);
                                cmdDetay.Parameters.AddWithValue("@d4", satir["Birim_Fiyati"]);
                                cmdDetay.ExecuteNonQuery();
                            }

                            string sorguStok = "UPDATE Envanter_Stoklar SET Stok_Adeti = Stok_Adeti - @s1 WHERE Envanter_ID = @s2 AND Stok_Adeti >= @s1";
                            using (SqlCommand cmdStok = new SqlCommand(sorguStok, baglanti, islem))
                            {
                                cmdStok.Parameters.AddWithValue("@s1", satir["Miktar"]);
                                cmdStok.Parameters.AddWithValue("@s2", satir["Envanter_ID"]);
                                if (cmdStok.ExecuteNonQuery() == 0)
                                    throw new Exception("Stok güncellenemedi; eşzamanlı satış veya yetersiz stok.");
                            }
                        }
                    }

                    islem.Commit();
                    return true;
                }
                catch
                {
                    islem.Rollback();
                    throw;
                }
            }
        }

        public static DataTable SiparisGecmisiniGetir()
        {
            string sorgu = @"SELECT s.Siparis_ID AS [Fiş No],ISNULL(M.Ad+' '+M.Soyad, 'Genel Müşteri') AS [Müşteri],C.Ad+'  '+C.Soyad AS [Kasiyer],S.Siparis_Tarihi AS[Tarih],S.Odeme_Turu AS [Ödeme Tipi],S.Toplam_Tutar AS [Tutar] 
                FROM Siparisler S 
                LEFT JOIN Musteriler M ON S.Musteri_ID =M.Musteri_ID
                INNER JOIN Calisanlar C ON S.Calisan_ID=C.Calisan_ID
                ORDER BY S.Siparis_Tarihi DESC";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, null);
        }
    }
}
