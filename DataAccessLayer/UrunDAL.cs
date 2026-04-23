using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class UrunDAL
    {
        public static List<Urun> UrunListele()
        {
            //Liste oluştuluyor
            List<Urun> urunListesi = new List<Urun>();
            //SQL sorgusu çalıştırılıyor
            SqlDataReader oku=SQLBaglantisi.SorguCalistir("SELECT * FROM Urunler");
            //Okunan veriler listeye ekleniyor
            while (oku.Read())
            {
                //Yeni bir ürün nesnesi oluşturuluyor
                Urun u = new Urun();
                //SQL komutu okunuyor ve listeye ekleniyor
                u.Urun_ID=int.Parse(oku["Urun_ID"].ToString());
                u.Kategori_ID=int.Parse(oku["Kategori_ID"].ToString());
                u.Urun_Adi=oku["Urun_Adi"].ToString();
                u.Barkod_NO=oku["Barkod_NO"].ToString();
                u.Marka=oku["Marka"].ToString();
                //Oluşturulan ürün nesnesi listeye ekleniyor
                urunListesi.Add(u);
            }
            //Okuma işlemi tamamlandıktan sonra bağlantı kapatılıyor
            oku.Close();
            //Oluşturulan ürün listesi geri döndürülüyor
            return urunListesi;
        }
        public static int UrunEkle(EntityLayer.Urun u)
        {

            SqlParameter[] prm = {
        new SqlParameter("@p1", u.Urun_Adi),
        new SqlParameter("@p2", u.Kategori_ID),
        new SqlParameter("@p3", u.Marka),
        new SqlParameter("@p4", u.Barkod_NO)
    };
            string sorgu = "INSERT INTO URUNLER (Urun_Adi, Kategori_ID, Marka, Barkod_NO) VALUES (@p1, @p2, @p3, @p4)";

            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm);
        }
        public static DataTable UrunListeleTablo()

        {
            string sorgu = "SELECT * FROM Urunler";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, null);
        }
        public static DataTable UrunAra(string arananKelime)
        {
            SqlParameter[] prm = {
        // Yüzde (%) işaretleri "Önünde veya arkasında ne olduğu önemli değil, içinde bu kelime geçsin" demektir.
        new SqlParameter("@p1", "%" + arananKelime + "%")
         };

            string sorgu = "SELECT * FROM Urunler WHERE Urun_Adi LIKE @p1";

            // Zaten hazırda olan SorguCalistir (DataTable dönen) metodumuzu kullanıyoruz
            return SQLBaglantisi.SorguCalistirTablo(sorgu, prm);
        }
    }
}
