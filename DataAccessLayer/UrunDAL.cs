using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using EntityLayer;

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
    }
}
