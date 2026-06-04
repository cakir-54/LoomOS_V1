using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SQLBaglantisi
    {
        private const string BaglantiCumlesi = @"Data Source=localhost; Initial Catalog=LoomOS; Integrated Security=True;";

        /// <summary>Kapalı bağlantı nesnesi döndürür; using ile kullanın.</summary>
        public static SqlConnection BaglantiOlustur()
        {
            return new SqlConnection(BaglantiCumlesi);
        }

        public static SqlConnection BaglantiGetir()
        {
            SqlConnection baglan = BaglantiOlustur();
            baglan.Open();
            return baglan;
        }

        public static SqlDataReader SorguCalistir(string sorgu)
        {
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);
            return komut.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader SorguCalistir(string sorgu, SqlParameter[] parametreler)
        {
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);
            if (parametreler != null)
            {
                komut.Parameters.AddRange(parametreler);
            }
            return komut.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static DataTable SorguCalistirTablo(string sorgu, SqlParameter[] parametreler)
        {
            DataTable tablo = new DataTable();
            using (SqlConnection baglan = BaglantiOlustur())
            {
                baglan.Open();
                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                {
                    if (parametreler != null)
                    {
                        komut.Parameters.AddRange(parametreler);
                    }
                    using (SqlDataAdapter adaptor = new SqlDataAdapter(komut))
                    {
                        adaptor.Fill(tablo);
                    }
                }
            }
            return tablo;
        }

        public static string TekDegerGetir(string sorgu)
        {
            using (SqlConnection baglan = BaglantiOlustur())
            {
                baglan.Open();
                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                {
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc == null || sonuc == DBNull.Value)
                        return "0";
                    return sonuc.ToString();
                }
            }
        }

        public static int EkleSilGuncelle(string sorgu, SqlParameter[] parametreler)
        {
            using (SqlConnection baglan = BaglantiOlustur())
            {
                baglan.Open();
                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                {
                    if (parametreler != null)
                    {
                        komut.Parameters.AddRange(parametreler);
                    }
                    return komut.ExecuteNonQuery();
                }
            }
        }
    }
}
