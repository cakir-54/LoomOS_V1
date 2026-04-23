using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessLayer
{
    public class KategoriDAL
    {
        public static DataTable KategoriListele()
        {
            // Kategorileri veritabanından Tablo olarak çekiyoruz
            return SQLBaglantisi.SorguCalistirTablo("SELECT * FROM Kategoriler", null);
        }
    }
}
