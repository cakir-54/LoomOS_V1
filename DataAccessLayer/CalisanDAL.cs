using EntityLayer;
using System;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class CalisanDAL
    {
        public static List<Calisan> CalisanListele()
        {
            //Liste oluşturuluyor
            List<Calisan> calisanListesi = new List<Calisan>();
            //SQL sorgusu çalıştırılıyor
            SqlDataReader oku = SQLBaglantisi.SorguCalistir("SELECT * FROM Vw_Calisan_Detay");
            //Okunan veriler listeye ekleniyor
            while (oku.Read())
            {
                //Yeni bir çalışan nesnesi oluşturuluyor
                Calisan cal = new Calisan();
                //SQL komutu okunuyor ve listeye ekleniyor
                cal.Calisan_ID = int.Parse(oku["Calisan_ID"].ToString());
                cal.Departman_ID = int.Parse(oku["Departman_ID"].ToString());
                cal.Departman_Adi = oku["Departman_Adi"].ToString();
                cal.Calisan_Ad = oku["Ad"].ToString();
                cal.Calisan_Soyad = oku["Soyad"].ToString();
                cal.Calisan_TC = oku["TC_NO"].ToString();
                cal.Calisan_TelNO = oku["Tel_NO"].ToString();
                if (oku["Maas"] != DBNull.Value)
                {
                    cal.Calisan_Maas = Convert.ToDecimal(oku["Maas"]);
                }
                else
                {
                    // EĞER BOŞSA SIFIR (0) KABUL ET Kİ SİSTEM ÇÖKMESİN
                    cal.Calisan_Maas = 0;
                }
                cal.Giris_Tarihi = DateTime.Parse(oku["Giris_Tarihi"].ToString());
                cal.Sifre = oku["Sifre"].ToString();

                //Çıkış tarihi null ise null olarak atanıyor
                if ((oku["Cikis_Tarihi"] != DBNull.Value))
                    cal.Cikis_Tarihi = Convert.ToDateTime(oku["Cikis_Tarihi"].ToString());
                else
                    cal.Cikis_Tarihi = null;
                //Listeye ekleniyor
                calisanListesi.Add(cal);

            }
            //Bağlantı kapatılıyor
            oku.Close();
            //Liste döndürülüyor
            return calisanListesi;
        }
        public static Calisan GirisYap(string tc,string sifre)
        {
            //Döndürülecek çalışan nesnesi oluşturuluyor
            Calisan c = null;
            //SQL parametreleri oluşturuluyor
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@tc",tc),
                new SqlParameter("@sifre",sifre)
            };
            //SQL sorgusu çalıştırılıyor
            SqlDataReader oku = SQLBaglantisi.SorguCalistir("SELECT * FROM Calisanlar WHERE TC_NO = @tc AND Sifre = @sifre", prm);

            //Eğer veri okunursa çalışan nesnesi oluşturuluyor
            if (oku.Read())
            {
                c=new Calisan();
                c.Calisan_ID = Convert.ToInt32(oku["Calisan_ID"]);
                c.Departman_ID = Convert.ToInt32(oku["Departman_ID"]);
                c.Calisan_Ad = oku["Ad"].ToString();
                c.Calisan_Soyad = oku["Soyad"].ToString();
            }

            oku.Close();
            //Çalışan nesnesi döndürülüyor
            return c;
        }
        public static int CalisanEkle(Calisan c)
        {

            SqlParameter[] prm = {
        new SqlParameter("@p1", c.Departman_ID),
        new SqlParameter("@p2", c.Calisan_Ad),
        new SqlParameter("@p3", c.Calisan_Soyad),
        new SqlParameter("@p4", c.Calisan_TC),
        new SqlParameter("@p5", c.Sifre),
        new SqlParameter("@p6", c.Calisan_TelNO)
    };

            // SİHİR BURADA: Giris_Tarihi için parametre göndermiyoruz, direkt SQL'in GETDATE() fonksiyonunu çağırıyoruz!
            string sorgu = "INSERT INTO Calisanlar (Departman_ID, Ad, Soyad, TC_NO, Sifre, Tel_NO, Giris_Tarihi) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, GETDATE())";

            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm);
        }
        public static int SifreGuncelle(int id, string yeniSifre)
        {
            SqlParameter[] prm = {
            new SqlParameter("@p1", yeniSifre),
            new SqlParameter("@p2", id)
            };

            // Sadece şifreyi değiştir, kimin şifresini? Yaka kartındaki (Session) ID'nin!
            return SQLBaglantisi.EkleSilGuncelle("UPDATE CALISANLAR SET Sifre = @p1 WHERE Calisan_ID = @p2", prm);
        }
    }
}
