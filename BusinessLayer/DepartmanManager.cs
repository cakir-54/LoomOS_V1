using System;
using System.Collections.Generic;
using EntityLayer;
using DataAccessLayer;

namespace BusinessLayer
{
    public class DepartmanManager
    {
        public static List<Departman> DepartmanListeleBL()
        {
            // İleride buraya "Eğer kullanıcının yetkisi yoksa boş liste gönder" gibi if-else kuralları yazacağız.
            // Şimdilik özel bir kuralımız yok, DAL'dan gelen veriyi direkt onaylayıp yukarı gönderiyoruz.
            return DepartmanDAL.DepartmanListele();
        }

        public static int DepartmanEkleBL(Departman d)
        {
        // KURAL 1: Departman adı boş olamaz!
        if (string.IsNullOrWhiteSpace(d.Departman_Adi))
        {
        throw new Exception("Departman adı boş bırakılamaz!");
        }

        // KURAL 2: Departman adı en az 2 karakter olmalı (Örn: "A" diye departman olmaz)
        if (d.Departman_Adi.Length < 2)
        {
        throw new Exception("Departman adı en az 2 karakter olmalıdır!");
         }

        // Kuralları geçtiyse DAL'a yolla ve SQL'e kaydet!
        return DepartmanDAL.DepartmanEkle(d);
        }

        public static int DepartmanSilBL(int id)
        {
            if (id <= 0)
                throw new Exception("Lütfen silmek için geçerli bir departman seçiniz");
            return DepartmanDAL.DepartmanSil(id);
        }

        public static int DepartmanGuncelleBL(Departman d)
        {
            if (d.Departman_ID <= 0)
                throw new Exception("Güncellenecek departman seçilmedi!");

            if (string.IsNullOrWhiteSpace(d.Departman_Adi) || d.Departman_Adi.Length < 2)
                throw new Exception("Geçerli bir yeni departman adı giriniz!");

            return DepartmanDAL.DepartmanGuncelle(d);
        }

    }
}
