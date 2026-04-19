using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
