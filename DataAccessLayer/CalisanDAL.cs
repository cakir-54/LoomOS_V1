using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class CalisanDAL
    {
        public static List<Calisan> CalisanListele()
        {
            List<Calisan> calisanListesi = new List<Calisan>();
            SqlDataReader oku = SQLBaglantisi.SorguCalistir(
                @"SELECT V.* FROM Vw_Calisan_Detay V
                  INNER JOIN Calisanlar C ON V.Calisan_ID = C.Calisan_ID
                  WHERE ISNULL(C.Aktif_Mi, 1) = 1");

            try
            {
                while (oku.Read())
                {
                    Calisan cal = new Calisan();
                    cal.Calisan_ID = int.Parse(oku["Calisan_ID"].ToString());
                    cal.Departman_ID = int.Parse(oku["Departman_ID"].ToString());
                    cal.Departman_Adi = oku["Departman_Adi"].ToString();
                    cal.Calisan_Ad = oku["Ad"].ToString();
                    cal.Calisan_Soyad = oku["Soyad"].ToString();
                    cal.Calisan_TC = oku["TC_NO"].ToString();
                    cal.Calisan_TelNO = oku["Tel_NO"].ToString();
                    if (oku["Maas"] != DBNull.Value)
                        cal.Calisan_Maas = Convert.ToDecimal(oku["Maas"]);
                    else
                        cal.Calisan_Maas = 0;

                    cal.Giris_Tarihi = DateTime.Parse(oku["Giris_Tarihi"].ToString());
                    cal.Sifre = oku["Sifre"].ToString();

                    if (oku["Cikis_Tarihi"] != DBNull.Value)
                        cal.Cikis_Tarihi = Convert.ToDateTime(oku["Cikis_Tarihi"].ToString());
                    else
                        cal.Cikis_Tarihi = null;

                    calisanListesi.Add(cal);
                }
            }
            finally
            {
                oku.Close();
            }

            return calisanListesi;
        }

        public static Calisan GirisYap(string tc, string sifre)
        {
            Calisan c = null;
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@tc", tc),
                new SqlParameter("@sifre", sifre)
            };

            SqlDataReader oku = SQLBaglantisi.SorguCalistir(
                "SELECT * FROM Calisanlar WHERE TC_NO = @tc AND Sifre = @sifre AND ISNULL(Aktif_Mi, 1) = 1 AND Sifre <> 'KOVULDU'",
                prm);

            try
            {
                if (oku.Read())
                {
                    c = new Calisan();
                    c.Calisan_ID = Convert.ToInt32(oku["Calisan_ID"]);
                    c.Departman_ID = Convert.ToInt32(oku["Departman_ID"]);
                    c.Calisan_Ad = oku["Ad"].ToString();
                    c.Calisan_Soyad = oku["Soyad"].ToString();
                }
            }
            finally
            {
                oku.Close();
            }

            return c;
        }

        public static int CalisanEkle(Calisan c)
        {
            SqlParameter[] prm = {
                new SqlParameter("@p1", c.Departman_ID),
                new SqlParameter("@p2", c.Calisan_Ad),
                new SqlParameter("@p3", c.Calisan_Soyad),
                new SqlParameter("@p4", c.Calisan_TC),
                new SqlParameter("@p5", c.Sifre),
                new SqlParameter("@p6", c.Calisan_TelNO)
            };

            string sorgu = "INSERT INTO Calisanlar (Departman_ID, Ad, Soyad, TC_NO, Sifre, Tel_NO, Giris_Tarihi, Aktif_Mi) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, GETDATE(), 1)";
            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm);
        }

        public static int SifreGuncelle(int id, string yeniSifre)
        {
            SqlParameter[] prm = {
                new SqlParameter("@p1", yeniSifre),
                new SqlParameter("@p2", id)
            };
            return SQLBaglantisi.EkleSilGuncelle("UPDATE Calisanlar SET Sifre = @p1 WHERE Calisan_ID = @p2 AND ISNULL(Aktif_Mi, 1) = 1", prm);
        }

        public static bool MaasGuncelle(string tcNo, decimal yeniMaas)
        {
            string sorgu = "UPDATE Calisanlar SET Maas = @yeniMaas WHERE TC_NO = @tc AND ISNULL(Aktif_Mi, 1) = 1";
            SqlParameter[] prm = {
                new SqlParameter("@yeniMaas", yeniMaas),
                new SqlParameter("@tc", tcNo)
            };
            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm) > 0;
        }

        public static bool IstenCikar(string tcNo)
        {
            string sorgu = "UPDATE Calisanlar SET Aktif_Mi = 0, Sifre = 'KOVULDU', Cikis_Tarihi = GETDATE() WHERE TC_NO = @tc AND ISNULL(Aktif_Mi, 1) = 1";
            SqlParameter[] prm = { new SqlParameter("@tc", tcNo) };
            return SQLBaglantisi.EkleSilGuncelle(sorgu, prm) > 0;
        }
    }
}
