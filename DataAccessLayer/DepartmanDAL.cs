using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using EntityLayer;


namespace DataAccessLayer
{
    public class DepartmanDAL
    {
        public static List<Departman> DepartmanListele()
        {
            //Liste oluşturuluyor
            List<Departman> departmanListesi=new List<Departman>();
            //SQL bağlantısı açılıyor
            SqlConnection baglan =SQLBaglantisi.BaglantiGetir();
            //SQLkomutu oluşturuluyor
            SqlCommand komut =new SqlCommand("SELECT * FROM DEPARTMANLAR",baglan);
            //SQL komutu çalıştırılıyor
            SqlDataReader oku =komut.ExecuteReader();
            //SQL komutu okunuyor
            while (oku.Read())
            {
                Departman dep=new Departman();
                //SQL komutu okunuyor ve listeye ekleniyor
                dep.Departman_ID = int.Parse(oku["Departman_ID"].ToString());
                dep.Departman_Adi = oku["Departman_Adi"].ToString();
                //Listeye ekleniyor
                departmanListesi.Add(dep);
            }
            //Kapatılıyor
            oku.Close();
            baglan.Close();

            //Liste döndürülüyor
            return departmanListesi;
        }
    }  
    
}
