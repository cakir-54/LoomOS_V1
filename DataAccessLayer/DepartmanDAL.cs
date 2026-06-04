using EntityLayer;
using System;
using System.Data.SqlClient;


namespace DataAccessLayer
{
    public class DepartmanDAL
    {
        public static List<Departman> DepartmanListele()
        {
            //Liste oluşturuluyor
            List<Departman> departmanListesi = new List<Departman>();
            //SQL sorgusu çalıştırılıyor
            SqlDataReader oku = SQLBaglantisi.SorguCalistir("Select * from Departmanlar");
            try
            {
            while (oku.Read())
            {
                //Yeni bir departman nesnesi oluşturuluyor
                Departman dep = new Departman();
                //SQL komutu okunuyor ve listeye ekleniyor
                dep.Departman_ID = int.Parse(oku["Departman_ID"].ToString());
                dep.Departman_Adi = oku["Departman_Adi"].ToString();
                //Listeye ekleniyor
                departmanListesi.Add(dep);
            }
            }
            finally
            {
                oku.Close();
            }
            return departmanListesi;
        }

        public static int DepartmanEkle(Departman d)
        {
            // Güvenlik zırhımızı (Parametre) takıyoruz
            SqlParameter[] prm = { new SqlParameter("@p1", d.Departman_Adi) };

            // Kendi gerçek tablo adının DEPARTMANLAR olduğuna emin ol
            return SQLBaglantisi.EkleSilGuncelle("INSERT INTO Departmanlar (Departman_Adi) VALUES (@p1)", prm);
        }

        public static int DepartmanSil(int id)
        {
            SqlParameter[] prm = { new SqlParameter("@p1", id) };
            return SQLBaglantisi.EkleSilGuncelle("DELETE FROM Departmanlar WHERE Departman_ID = @p1", prm);
        }

        public static int DepartmanGuncelle(Departman d)
        {
            SqlParameter[] prm = {
                new SqlParameter("@p1", d.Departman_Adi),
                new SqlParameter("@p2", d.Departman_ID)
        };
            return SQLBaglantisi.EkleSilGuncelle("Update Departmanlar set Departman_Adi=@p1 Where Departman_ID=@p2", prm);
        }
    }
}
