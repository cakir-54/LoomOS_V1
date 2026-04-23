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
            if (e.Stok_Adeti < 0) throw new Exception("Stok miktarı eksi (-) olamaz!");
            if (e.Satis_Fiyati <= e.Alis_Fiyati) throw new Exception("Zararına satış yapamazsınız! Satış fiyatı, Alış fiyatından büyük olmalıdır.");

            return EnvanterDAL.StokVaryantEkle(e);
        }
    }
}
