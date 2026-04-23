using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class UrunManager
    {
        public static DataTable UrunListeleTabloBL()
        {
            return UrunDAL.UrunListeleTablo();
        }
        public static int UrunEkleBL(Urun u)
        {
            // 1. Ürün adı kontrolü
            if (string.IsNullOrWhiteSpace(u.Urun_Adi))
                throw new Exception("Ürün adı boş olamaz!");

            // 2. Kategori seçimi kontrolü (ID sıfır veya daha küçük olamaz)
            if (u.Kategori_ID <= 0)
                throw new Exception("Lütfen geçerli bir kategori seçiniz!");

            // 3. Marka kontrolü
            if (string.IsNullOrWhiteSpace(u.Marka))
                throw new Exception("Marka alanı boş bırakılamaz!");

            // 4. Barkod kontrolü
            if (string.IsNullOrWhiteSpace(u.Barkod_NO))
                throw new Exception("Barkod numarası boş bırakılamaz!");

            // Kurallardan geçtiyse Hamal'a (DAL) gönder
            return UrunDAL.UrunEkle(u);
        }
        public static List<Urun> UrunListeleBL()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return UrunDAL.UrunListele();
        }
        public static DataTable UrunAraBL(string arananKelime)
        {
            // KURAL: Eğer kullanıcı arama kutusunu tamamen temizlerse, boşuna SQL'de arama yapma, tüm ürünleri getir!
            if (string.IsNullOrWhiteSpace(arananKelime))
            {
                return UrunDAL.UrunListeleTablo(); // Tüm ürünleri listeleyen standart metodun
            }

            return UrunDAL.UrunAra(arananKelime);
        }
    }
}
