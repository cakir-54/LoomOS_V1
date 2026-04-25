using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class MusteriDAL
    {
        public static DataTable MusterileriListele()
        {
            string sorgu = "SELECT Musteri_ID, Ad, Soyad, Tel_NO, Mail,Kayit_Tarihi FROM Musteriler";
            SqlParameter[] bos = new SqlParameter[0];
            return SQLBaglantisi.SorguCalistirTablo(sorgu, bos);
        }
        public static bool MusteriEkle(string ad, string soyad, string telefon, string mail)
        {
            SqlConnection baglanti = SQLBaglantisi.BaglantiGetir();
            string sorgu = "INSERT INTO Musteriler (Ad, Soyad, Tel_NO, Mail,Kayit_Tarihi) VALUES (@p1, @p2, @p3, @p4,GETDATE( ))";
            SqlCommand cmd = new SqlCommand(sorgu, baglanti);
            cmd.Parameters.AddWithValue("@p1", ad);
            cmd.Parameters.AddWithValue("@p2", soyad);
            cmd.Parameters.AddWithValue("@p3", telefon);
            if (string.IsNullOrWhiteSpace(mail)) cmd.Parameters.AddWithValue("@p4", System.DBNull.Value);
            else cmd.Parameters.AddWithValue("@p4", mail);
            int sonuc = cmd.ExecuteNonQuery();
            return sonuc > 0;
        }
        
        public static DataTable MusteriAra(string arananKelime)
        {
            SqlParameter[] prm = new SqlParameter[1];
            string sorgu = "SELECT Musteri_ID, Ad, Soyad, Telefon, Email, Kayit_Tarihi FROM Musteriler WHERE (Ad + ' ' + Soyad) LIKE @p1 OR Telefon LIKE @p1";
            prm[0] = new SqlParameter("@p1", "%" + arananKelime + "%");

            return SQLBaglantisi.SorguCalistirTablo(sorgu, prm);
        }

    }
}
