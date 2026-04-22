using System;
using System.Collections.Generic;
using System.Text;
using EntityLayer;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class MusteriDAL
    {
        public static List<Musteri> MusteriListele()
        {
            //Liste oluşturuluyor
            List<Musteri>musteriListesi=new List<Musteri>();
            //Sql bağlantısı oluşturuluyor
            SqlDataReader oku=SQLBaglantisi.SorguCalistir("SELECT * FROM Musteriler");
            //Veriler okunuyor ve listeye ekleniyor
            while (oku.Read())
            {
                Musteri m = new Musteri();
                m.Musteri_ID = int.Parse(oku["Musteri_ID"].ToString());
                m.Musteri_Adi = oku["Ad"].ToString();
                m.Musteri_Soyadi = oku["Soyad"].ToString();
                m.Musteri_TelNO = oku["Tel_NO"].ToString();
                m.Musteri_Mail = oku["Mail"].ToString();
                m.Kayit_Tarihi = Convert.ToDateTime(oku["Kayit_Tarihi"]);

                musteriListesi.Add(m);
            }
            oku.Close();
            return musteriListesi;
        }

        public static List<Musteri> MusteriAra(string aranacakKelime)
        {
            //Liste oluşturuluyor
            List<Musteri> musteriListesi = new List<Musteri>();
            //SorguCalistir metodu için parametreler hazırlanıyor
            SqlParameter[] prm = { new SqlParameter("@kelime", "%" + aranacakKelime + "%") };
            //Sql bağlantısı oluşturuluyor
            SqlDataReader oku = SQLBaglantisi.SorguCalistir("SELECT * FROM Musteriler WHERE Ad LIKE @kelime", prm);
            //Veriler okunuyor ve listeye ekleniyor
            while (oku.Read())
            {
                Musteri m = new Musteri();
                m.Musteri_ID = int.Parse(oku["Musteri_ID"].ToString());
                m.Musteri_Adi = oku["Ad"].ToString();
                m.Musteri_Soyadi = oku["Soyad"].ToString();
                m.Musteri_TelNO = oku["Tel_NO"].ToString();
                m.Musteri_Mail = oku["Mail"].ToString();
                m.Kayit_Tarihi = Convert.ToDateTime(oku["Kayit_Tarihi"]);

                musteriListesi.Add(m);
            }
            oku.Close(); 
            return musteriListesi;

        }

    }
}
