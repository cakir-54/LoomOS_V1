using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class MusteriDAL
    {
        public static DataTable MusterileriListele()
        {
            string sorgu = "SELECT Musteri_ID, Ad, Soyad, Tel_NO, Mail,Kayit_Tarihi FROM Musteriler";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, null);
        }

        public static bool MusteriEkle(string ad, string soyad, string telefon, string mail)
        {
            string sorgu = "INSERT INTO Musteriler (Ad, Soyad, Tel_NO, Mail,Kayit_Tarihi) VALUES (@p1, @p2, @p3, @p4,GETDATE())";
            SqlParameter[] prm = {
                new SqlParameter("@p1", ad),
                new SqlParameter("@p2", soyad),
                new SqlParameter("@p3", string.IsNullOrWhiteSpace(telefon) ? (object)DBNull.Value : telefon),
                new SqlParameter("@p4", string.IsNullOrWhiteSpace(mail) ? (object)DBNull.Value : mail)
            };
            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm) > 0;
        }

        public static DataTable MusteriAra(string arananKelime)
        {
            SqlParameter[] prm = {
                new SqlParameter("@p1", "%" + arananKelime + "%")
            };
            string sorgu = "SELECT Musteri_ID, Ad, Soyad, Tel_NO, Mail, Kayit_Tarihi FROM Musteriler WHERE (Ad + ' ' + Soyad) LIKE @p1 OR Tel_NO LIKE @p1";
            return SQLBaglantisi.SorguCalistirTablo(sorgu, prm);
        }
    }
}
