using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SQLBaglantisi
    {
        public static SqlConnection BaglantiGetir()
        {
            string baglantiCumlesi = @"Data Source=localhost; Initial Catalog=LoomOS; Integrated Security=True;";
            SqlConnection baglan = new SqlConnection(baglantiCumlesi);
            baglan.Open(); // Köprüyü trafiğe açıyoruz

            return baglan;
        }

        public static SqlDataReader SorguCalistir(string sorgu)
        {
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);

            // SİHİR BURADA: Bu komut, "oku.Close()" dendiği an "baglan.Close()" işlemini de otomatik yapar!
            return komut.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        //Parametreli sorgu çalıştırmak için aşırı yüklenmiş bir metot ekleyelim
        public static SqlDataReader SorguCalistir(string sorgu, SqlParameter[] parametreler)
        {
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);

            // Eğer parametre gönderilmişse, bunları komuta zırh olarak ekle
            if (parametreler != null)
            {
                komut.Parameters.AddRange(parametreler);
            }

            return komut.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        public static DataTable SorguCalistirTablo(string sorgu, SqlParameter[] parametreler)
        {
            // RAM'de boş bir sanal tablo oluşturuyoruz
            DataTable tablo = new DataTable();

            //Kendi sistemindeki bağlantı nesneni kullan (Aşağıdaki Baglanti yazan yeri kendi koduna göre uyarla)
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);

            if (parametreler != null)
            {
                komut.Parameters.AddRange(parametreler);
            }

            // 4. SqlDataAdapter ile SQL'e gidiyoruz
            SqlDataAdapter adaptor = new SqlDataAdapter(komut);

            adaptor.Fill(tablo);

            return tablo;
        }

        public static string TekDegerGetir(string sorgu)
        {
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);
            // ExecuteScalar, tablonun sadece en üstteki sol hücresini (tek bir veriyi) alır.
            string sonuc=komut.ExecuteScalar().ToString();

            baglan.Close(); // Bağlantıyı kapatmayı unutmayalım!
            return sonuc;
        }

        public static int EkleSilGuncelle(string sorgu, SqlParameter[] parametreler)
        {
            SqlConnection baglan = BaglantiGetir();
            SqlCommand komut = new SqlCommand(sorgu, baglan);

            if (parametreler != null)
            {
                komut.Parameters.AddRange(parametreler);
            }

            // ExecuteNonQuery: Tabloda değişiklik yapan komutları çalıştırır.
            int sonuc = komut.ExecuteNonQuery();

            baglan.Close();
            return sonuc;
        }
    }
}
