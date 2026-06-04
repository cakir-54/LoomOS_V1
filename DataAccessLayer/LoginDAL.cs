using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class LoginDAL
    {
        public static DataTable KullaniciGirisiniKontrolEt(string tcKimlik, string sifre)
        {
            string sorgu = @"SELECT Calisan_ID, Ad, Soyad, Departman_ID 
                FROM Calisanlar 
                WHERE TC_NO = @tc AND Sifre = @sifre 
                  AND ISNULL(Aktif_Mi, 1) = 1 
                  AND Sifre <> 'KOVULDU'";

            SqlParameter[] prm = {
                new SqlParameter("@tc", tcKimlik),
                new SqlParameter("@sifre", sifre)
            };

            return SQLBaglantisi.SorguCalistirTablo(sorgu, prm);
        }
    }
}
