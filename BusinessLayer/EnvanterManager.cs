using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace BusinessLayer
{
    public class EnvanterManager
    {
        public static DataTable EnvanterListeleBL()
        {
            return EnvanterDAL.EnvanterListeleTablo();
        }
        public static int StokVaryantEkleBL(EnvanterStok e)
        {
            if (e.Urun_ID <= 0) throw new Exception("Lütfen stok eklenecek bir ürün seçiniz!");
            if (e.Stok_Adeti <= 0) throw new Exception("Stok miktarı 0 veya eksi olamaz!");
            if (e.Satis_Fiyati <= e.Alis_Fiyati) throw new Exception("Zararına satış yapamazsınız! Satış fiyatı, Alış fiyatından büyük olmalıdır.");

            return EnvanterDAL.StokVaryantEkle(e);
        }
        public static int StokGuncelleBL(EntityLayer.EnvanterStok e)
        {
            if (e.Envanter_ID <= 0) throw new Exception("Güncellenecek kayıt seçilemedi!");
            if (e.Stok_Adeti < 0) throw new Exception("Stok eksi (-) olamaz!");
            if (e.Satis_Fiyati <= e.Alis_Fiyati) throw new Exception("Satış fiyatı alıştan yüksek olmalıdır!");

            return EnvanterDAL.StokGuncelle(e);
        }
        public static int StokSilBL(int envanterID)
        {
            if (envanterID <= 0) throw new Exception("Silinecek kayıt geçerli değil!");
            return EnvanterDAL.StokSil(envanterID);
        }
        public static System.Data.DataTable BarkodIleUrunGetirBL(string barkod)
        {
            if (string.IsNullOrWhiteSpace(barkod)) throw new System.Exception("Barkod boş olamaz!");
            return EnvanterDAL.BarkodIleUrunGetir(barkod);
        }
    }
}
