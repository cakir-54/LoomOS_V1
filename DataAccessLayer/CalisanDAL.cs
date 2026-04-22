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
                cal.Calisan_Ad = oku["Calisan_Ad"].ToString();
                cal.Calisan_Soyad = oku["Calisan_Soyad"].ToString();
                cal.Calisan_TC = oku["Calisan_TC"].ToString();
                cal.Calisan_TelNO = oku["Calisan_TelNO"].ToString();
                cal.Calisan_Maas = decimal.Parse(oku["Calisan_Maas"].ToString());
                cal.Giris_Tarihi = DateTime.Parse(oku["Giris_Tarihi"].ToString());
                cal.Sifre = oku["Sifre"].ToString();

                //Çıkış tarihi null ise null olarak atanıyor
                if ((oku["Cikis_Tarihi_null"] != DBNull.Value))
                    cal.Cikis_Tarihi_null = Convert.ToDateTime(oku["Cikis_Tarihi_null"].ToString());
                else
                    cal.Cikis_Tarihi_null = null;
                //Listeye ekleniyor
                calisanListesi.Add(cal);

            }
            //Bağlantı kapatılıyor
            oku.Close();
            //Liste döndürülüyor
            return calisanListesi;
        }
    }
}
