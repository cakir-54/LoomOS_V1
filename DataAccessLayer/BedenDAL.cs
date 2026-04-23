using EntityLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class BedenDAL
    {
        public static DataTable BedenListele()
        {
            // Bedenleri SQL'den tek hamlede Tablo olarak çekiyoruz
            return SQLBaglantisi.SorguCalistirTablo("SELECT * FROM Bedenler", null);
        }
    }
}
