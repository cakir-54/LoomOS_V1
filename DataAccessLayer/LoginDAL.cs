using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public class LoginDAL
    {
        public static DataTable KullaniciGirisiniKontrolEt(string tcKimlik, string sifre)
        {
            // TC Kimlik ve Şifre eşleşiyorsa, çalışanın ID, Ad, Soyad ve Departman bilgisini çekeriz.
            string sorgu = "SELECT Calisan_ID, Ad, Soyad, Departman_ID FROM Calisanlar WHERE TC_NO = @tc AND Sifre = @sifre";

            using (SqlConnection baglanti = SQLBaglantisi.BaglantiGetir())
            {
                SqlCommand cmd = new SqlCommand(sorgu, baglanti);
                cmd.Parameters.AddWithValue("@tc", tcKimlik);
                cmd.Parameters.AddWithValue("@sifre", sifre);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
