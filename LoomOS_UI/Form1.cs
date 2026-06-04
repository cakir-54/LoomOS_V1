using BusinessLayer;
using DataAccessLayer;
using EntityLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace LoomOS
{
    public partial class Form1 : Form
    {
        // Seçilen satırın ID'sini hafızada tutacak ajanımız
        int secilenEnvanterID = 0;
        // KASA MODÜLÜ İÇİN GEÇİCİ SANAL SEPET 
        System.Data.DataTable sepetTablosu = new System.Data.DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        #region Departmanlar için
        private void button1_Click(object sender, EventArgs e)//Departmanlar
        {
            try
            {
                // 1. İş Katmanına (Business) gidip "Bana departmanları getir" diyoruz.
                var departmanListesi = DepartmanManager.DepartmanListeleBL();

                // 2. Gelen listeyi ekrandaki tablomuza (DataGridView) bağlıyoruz.
                dataGridView1.DataSource = departmanListesi;

                MessageBox.Show("Veriler N-Tier Mimarisiyle Başarıyla Çekildi!", "Operasyon Tamam", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veri çekilirken bir hata oluştu: \n\n" + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Yeni bir departman nesnesi oluşturup, TextBox'taki yazıyı içine koyuyoruz
                Departman yeniDepartman = new Departman();
                yeniDepartman.Departman_Adi = textBox2.Text; // Kendi TextBox adını kontrol et

                // 2. Gümrüğe (BusinessLayer) gönderiyoruz
                int sonuc = DepartmanManager.DepartmanEkleBL(yeniDepartman);

                // 3. Eğer işlem başarılıysa (etkilenen satır sayısı 0'dan büyükse)
                if (sonuc > 0)
                {
                    MessageBox.Show("Yeni departman başarıyla veritabanına eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ekledikten sonra kutunun içini temizle
                    textBox2.Text = "";

                    // İSTEĞE BAĞLI: Ekledikten sonra güncel listeyi anında görmek için listeleme metodunu tekrar çağırabilirsin
                    // dataGridView1.DataSource = DepartmanManager.DepartmanListeleBL();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//Departmanlar
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen silmek için tablodan bir departman seçiniz!", "Seçim Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int secilenID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                int sonuc = DepartmanManager.DepartmanSilBL(secilenID);

                if (sonuc > 0)
                {
                    MessageBox.Show("Departman başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Listeyi yenilemek için listeleme metodunu tekrar çağırabilirsin
                    dataGridView1.DataSource = DepartmanManager.DepartmanListeleBL();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Lütfen güncellemek için tablodan bir departman seçiniz!", "Seçim Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Yeni departman adı boş olamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Departman guncellenecekDep = new Departman();

                guncellenecekDep.Departman_ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                // 2. Yeni ismi TextBox'tan al
                guncellenecekDep.Departman_Adi = textBox2.Text;

                int sonuc = DepartmanManager.DepartmanGuncelleBL(guncellenecekDep);

                if (sonuc > 0)
                {
                    MessageBox.Show("Departman başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = DepartmanManager.DepartmanListeleBL(); // Listeyi yenile
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Ürünler için

        private void textBoxUrunAra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Güvenli bölge: Her harfe basıldığında veriyi filtrele
                dataGridView2.DataSource = BusinessLayer.UrunManager.UrunAraBL(textBoxUrunAra.Text);
            }
            catch (Exception hata)
            {
                // Eğer o an SQL'e ulaşılamazsa veya bir hata koparsa programı ÇÖKERTME, sadece uyar!
                MessageBox.Show("Arama işlemi sırasında sunucuyla bağlantı kurulamadı: \n" + hata.Message, "Bağlantı Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void buttonUrunEkle_Click(object sender, EventArgs e)
        {
            //Hata koruması
            if (string.IsNullOrWhiteSpace(textBoxUrunAd.Text) || string.IsNullOrWhiteSpace(TextBoxMarka.Text) || string.IsNullOrWhiteSpace(TextBoxBarkod.Text))
            {
                MessageBox.Show("Lütfen Ürün Adı, Marka ve Barkod alanlarını doldurunuz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen listeden bir Kategori seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EntityLayer.Urun yeniUrun = new EntityLayer.Urun();
                yeniUrun.Urun_Adi = textBoxUrunAd.Text;
                yeniUrun.Kategori_ID = Convert.ToInt32(comboBoxKategori.SelectedValue);
                yeniUrun.Marka = TextBoxMarka.Text;
                yeniUrun.Barkod_NO = TextBoxBarkod.Text;

                int sonuc = BusinessLayer.UrunManager.UrunEkleBL(yeniUrun);

                if (sonuc > 0)
                {
                    MessageBox.Show("Ürün kataloğa başarıyla eklendi!\nŞimdi sağ taraftan bu ürüne beden/stok girebilirsiniz.", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    comboBoxUrun.DataSource = BusinessLayer.UrunManager.UrunListeleTabloBL();
                    textBoxUrunAd.Clear(); TextBoxMarka.Clear(); TextBoxBarkod.Clear(); comboBoxKategori.SelectedIndex = -1;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Katalog Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonStokGir_Click(object sender, EventArgs e)
        {
            // Hata Koruması
            if (comboBoxUrun.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen stok eklenecek Ürünü seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxBeden.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen listeden bir Beden seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericUpDownSatisFiyati.Value <= numericUpDownAlisFiyati.Value)
            {
                MessageBox.Show("Satış fiyatı, Alış fiyatından (Maliyetten) yüksek olmalıdır!", "Mantık Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numericUpDownStok.Value <= 0)
            {
                MessageBox.Show("Stok adedi 0 veya eksi olamaz!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EntityLayer.EnvanterStok yeniStok = new EntityLayer.EnvanterStok();

                // Sağ taraftaki kutulardan verileri topluyoruz
                yeniStok.Urun_ID = Convert.ToInt32(comboBoxUrun.SelectedValue);
                yeniStok.Beden_ID = Convert.ToInt32(comboBoxBeden.SelectedValue); // Beden ID'sini alıyoruz
                yeniStok.Renk = textBoxRenk.Text;

                // NumericUpDown'lardan değerleri (Value) alıyoruz
                yeniStok.Stok_Adeti = Convert.ToInt32(numericUpDownStok.Value);
                yeniStok.Alis_Fiyati = numericUpDownAlisFiyati.Value;
                yeniStok.Satis_Fiyati = numericUpDownSatisFiyati.Value;

                int sonuc = BusinessLayer.EnvanterManager.StokVaryantEkleBL(yeniStok);

                if (sonuc > 0)
                {
                    MessageBox.Show("Stok ve Varyant bilgisi başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView2.DataSource = BusinessLayer.EnvanterManager.EnvanterListeleBL();
                    // Ekranı yeni stok girişine hazırla (Ürün adı kalsın, beden/renk temizlensin)
                    textBoxRenk.Clear(); numericUpDownStok.Value = 0; numericUpDownAlisFiyati.Value = 0; numericUpDownSatisFiyati.Value = 0; comboBoxBeden.SelectedIndex = -1;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Stok Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//Ürünler Sayfası
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = dataGridView2.Rows[e.RowIndex];
                secilenEnvanterID = Convert.ToInt32(satir.Cells[0].Value);
                numericUpDownStok.Value = Convert.ToDecimal(satir.Cells["Stok_Adeti"].Value);
                numericUpDownAlisFiyati.Value = Convert.ToDecimal(satir.Cells["Alis_Fiyati"].Value);
                numericUpDownSatisFiyati.Value = Convert.ToDecimal(satir.Cells["Satis_Fiyati"].Value);

            }
        }
        private void buttonGuncelle_Click(object sender, EventArgs e)//Ürünler Sayfası
        {
            if (secilenEnvanterID == 0)
            {
                MessageBox.Show("Önce listeden güncellenecek kayda çift tıklayın!");
                return;
            }

            try
            {
                EntityLayer.EnvanterStok guncelStok = new EntityLayer.EnvanterStok();
                guncelStok.Envanter_ID = secilenEnvanterID;
                guncelStok.Stok_Adeti = Convert.ToInt32(numericUpDownStok.Value);
                guncelStok.Alis_Fiyati = numericUpDownAlisFiyati.Value;
                guncelStok.Satis_Fiyati = numericUpDownSatisFiyati.Value;

                int sonuc = BusinessLayer.EnvanterManager.StokGuncelleBL(guncelStok);
                if (sonuc > 0)
                {
                    MessageBox.Show("Kayıt güncellendi!");
                    dataGridView2.DataSource = BusinessLayer.EnvanterManager.EnvanterListeleBL();
                    secilenEnvanterID = 0; // Hafızayı temizle
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
        private void buttonSil_Click(object sender, EventArgs e)
        {
            if (secilenEnvanterID == 0) { MessageBox.Show("Önce listeden silinecek kayda çift tıklayın!"); return; }

            DialogResult cevap = MessageBox.Show("Bu kaydı kalıcı olarak silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    int sonuc = BusinessLayer.EnvanterManager.StokSilBL(secilenEnvanterID);
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kayıt silindi!");
                        dataGridView2.DataSource = BusinessLayer.EnvanterManager.EnvanterListeleBL();
                        secilenEnvanterID = 0;
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message, "Silme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }//Ürünler Sayfası
        private void UrunuSepeteEkle(string okunanBarkod)
        {
            if (string.IsNullOrWhiteSpace(okunanBarkod)) return;

            try
            {
                // 1. Veritabanından (Sanal VIEW tablomuzdan) ürünü getir
                System.Data.DataTable dtUrun = BusinessLayer.EnvanterManager.BarkodIleUrunGetirBL(okunanBarkod);

                if (dtUrun.Rows.Count > 0)
                {
                    System.Data.DataRow urun = dtUrun.Rows[0];
                    int envanterId = Convert.ToInt32(urun["Envanter_ID"]);
                    int stokAdeti = Convert.ToInt32(urun["Stok_Adeti"]);
                    string tamUrunAdi = urun["Urun_Adi"].ToString() + " - " + urun["Beden"].ToString() + " (" + urun["Renk"].ToString() + ")";
                    decimal fiyat = Convert.ToDecimal(urun["Satis_Fiyati"]);

                    if (stokAdeti <= 0)
                    {
                        MessageBox.Show("Bu ürünün stoğu tükenmiş! Satış yapılamaz.", "Stok Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxBarkodKasa.SelectAll();
                        return;
                    }

                    bool sepetteVarMi = false;
                    foreach (System.Data.DataRow satir in sepetTablosu.Rows)
                    {
                        if (Convert.ToInt32(satir["Envanter_ID"]) == envanterId)
                        {
                            int yeniMiktar = Convert.ToInt32(satir["Miktar"]) + 1;
                            if (yeniMiktar > stokAdeti)
                            {
                                MessageBox.Show($"Stok yetersiz! Maksimum eklenebilir adet: {stokAdeti}", "Stok Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            satir["Miktar"] = yeniMiktar;
                            satir["Ara_Toplam"] = yeniMiktar * fiyat;
                            sepetteVarMi = true;
                            break;
                        }
                    }

                    if (!sepetteVarMi) sepetTablosu.Rows.Add(envanterId, okunanBarkod, tamUrunAdi, 1, fiyat, fiyat);

                    GenelToplamGuncelle();
                    textBoxBarkodKasa.Clear();
                }
                else
                {
                    MessageBox.Show("Bu barkoda ait ürün bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxBarkodKasa.SelectAll();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message); }
        }//Ürünler Sayfası
        private void textBoxBarkodKasa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                UrunuSepeteEkle(textBoxBarkodKasa.Text.Trim()); // Merkezi metodu çağır
            }
        }//Ürünler Sayfası
        private void buttonSiparisiTamamla_Click(object sender, EventArgs e)//Ürünler Sayfası
        {
            if (sepetTablosu.Rows.Count == 0)
            {
                MessageBox.Show("Sepet boş! Önce ürün okutunuz.", "Satış İptal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                EntityLayer.Siparis yeniSatis = new EntityLayer.Siparis();

                int secilenMusteriID = Convert.ToInt32(comboBoxMusteri.SelectedValue);

                if (secilenMusteriID == 0)
                {
                    yeniSatis.Musteri_ID = null;
                }
                else
                {
                    yeniSatis.Musteri_ID = secilenMusteriID; // Kayıtlı müşteri seçildiyse onun ID'sini ver
                }

                // Ödeme türünü ComboBox'tan alıyoruz
                yeniSatis.Odeme_Turu = comboBoxOdemeTuru.Text;

                decimal kesinToplam = 0;
                foreach (System.Data.DataRow satir in sepetTablosu.Rows)
                {
                    kesinToplam += Convert.ToDecimal(satir["Ara_Toplam"]);
                }
                yeniSatis.Toplam_Tutar = kesinToplam;

                // Sepet tablosunu Gümrüğe yolluyoruz
                bool sonuc = BusinessLayer.SiparisManager.SatisiTamamlaBL(yeniSatis, sepetTablosu);

                if (sonuc)
                {
                    MessageBox.Show("Satış Başarıyla Tamamlandı! Müşteriye fişini verebilirsiniz.", "Kasa İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Satış bitti, yeni müşteri için ekranı sıfırla!
                    sepetTablosu.Rows.Clear();
                    GenelToplamGuncelle(); // Toplamı tekrar 0.00 TL yapar
                    comboBoxOdemeTuru.SelectedIndex = 0; // Tekrar Nakite çeker
                    textBoxBarkodKasa.Focus(); // İmleci hemen barkod kutusuna geri alır
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Satış İptal Edildi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSiparisIptal_Click(object sender, EventArgs e)
        {
            sepetTablosu.Rows.Clear();
            GenelToplamGuncelle();
        }//Ürünler Sayfası
        private void label19_Click(object sender, EventArgs e)
        {

        }
        private void buttonSepeteEkle_Click(object sender, EventArgs e)//Ürünler Sayfası
        {
            UrunuSepeteEkle(textBoxBarkodKasa.Text.Trim()); // Merkezi metodu çağır
            textBoxBarkodKasa.Focus();
        }




        #endregion

        #region Çalışanlar için
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. İş Katmanından (BusinessLayer) çalışan listesini istiyoruz
                var calisanListesi = CalisanManager.CalisanListeleBL();

                // 2. Gelen listeyi Çalışanlar sekmesindeki DataGridView'e (tabloya) bağlıyoruz.
                // DİKKAT: Senin tablonun adı dataGridView2 değilse, buradaki ismi kendi tablonun adıyla değiştir.
                dataGridView3.DataSource = calisanListesi;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Çalışanlar çekilirken bir hata oluştu: \n\n" + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxCalisanAd.Text) || string.IsNullOrWhiteSpace(textBoxCalisanSoyad.Text))
                {
                    MessageBox.Show("Ad ve soyad alanları boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxTCNO.Text) || textBoxTCNO.Text.Trim().Length != 11)
                {
                    MessageBox.Show("Geçerli bir 11 haneli TC Kimlik Numarası giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBox1.SelectedValue == null || comboBox1.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("Lütfen listeden bir departman seçiniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EntityLayer.Calisan yeniPersonel = new EntityLayer.Calisan();
                //TextBox'lardan verileri alıp yeniPersonel nesnesinin içine atıyoruz
                yeniPersonel.Calisan_Ad = textBoxCalisanAd.Text;
                yeniPersonel.Calisan_Soyad = textBoxCalisanSoyad.Text;
                yeniPersonel.Calisan_TC = textBoxTCNO.Text;
                yeniPersonel.Calisan_TelNO = textBoxTelNO.Text;
                //Şifremizi sistem üretiyor
                yeniPersonel.Sifre = BusinessLayer.CalisanManager.RastgeleSifreUret();
                //ComboBox'tan seçilen departmanın ID'sini alıp yeniPersonel nesnesine 
                yeniPersonel.Departman_ID = Convert.ToInt32(comboBox1.SelectedValue);
                //Yeni personel ekliyoruz
                int sonuc = BusinessLayer.CalisanManager.CalisanEkleBL(yeniPersonel);

                if (sonuc > 0)
                {
                    // Sistemin ürettiği şifreyi İK uzmanına gösteriyoruz!
                    string mesaj = $"Yeni personel başarıyla sisteme kaydedildi!\n\nSistemin Atadığı Geçici Şifre: {yeniPersonel.Sifre}\n\nLütfen bu şifreyi personele iletiniz.";

                    MessageBox.Show(mesaj, "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBoxCalisanAd.Text = "";
                    textBoxCalisanSoyad.Text = "";
                    textBoxTCNO.Text = "";
                    textBoxTelNO.Text = "";

                    dataGridView3.DataSource = BusinessLayer.CalisanManager.CalisanListeleBL();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Müşteriler için
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kutudaki yazıyı al ve Gümrüğe (BusinessLayer) gönder
                string aranan = textBox1.Text;

                // 2. Gelen veriyi direkt tabloya basmadan önce bir "aramaSonucu" değişkenine alıyoruz
                var aramaSonucu = MusteriManager.MusteriAraBL(aranan);

                // 3. YENİ KONTROL: Eğer SQL'den dönen listenin içi BOŞSA (Rows.Count == 0)
                if (aramaSonucu.Rows.Count == 0)
                {
                    MessageBox.Show("Aradığınız kritere uygun bir müşteri bulunamadı.", "Kayıt Yok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 4. Uyarı versek bile dönen o "boş listeyi" tabloya zorla bağlıyoruz!
                // Neden? Çünkü ekranda bir önceki aramadan kalan eski kayıtlar varsa, tablo kendini temizlesin!
                dataGridView4.DataSource = aramaSonucu;
            }
            catch (Exception hata)
            {
                // Gümrük kurallarına (boş bırakma, tek harf girme) takılanlar buraya düşer
                MessageBox.Show(hata.Message, "Geçersiz İşlem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView4.DataSource = MusteriManager.MusteriListeleBL();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Müşteri listesi yüklenemedi: " + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonMusteriEkle_Click(object sender, EventArgs e)//Müşteriler Sayfası
        {
            try
            {
                // Artık 4. parametre olarak txtEmail'i de yolluyoruz
                bool sonuc = BusinessLayer.MusteriManager.MusteriEkleBL(
                    textBoxMusteriAdi.Text,
                    textBoxMusteriSoyadi.Text,
                    textBoxTelefon.Text,
                    textBoxMail.Text
                );

                if (sonuc)
                {
                    MessageBox.Show("Müşteri başarıyla sisteme kaydedildi!", "CRM İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Temizlik
                    textBoxMusteriAdi.Clear();
                    textBoxMusteriSoyadi.Clear();
                    textBoxTelefon.Clear();
                    textBoxMail.Clear();
                    textBox1.Clear(); // Aramayı da sıfırlayalım ki tam liste gelsin
                    // Listeyi yenile
                    dataGridView4.DataSource = BusinessLayer.MusteriManager.MusteriListeleBL();
                }
            }
            catch (System.Exception hata)
            {
                MessageBox.Show(hata.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)//Müşteriler Sayfası
        {
            try
            {
                string aranan = textBox1.Text.Trim();

                if (string.IsNullOrWhiteSpace(aranan))
                    dataGridView4.DataSource = BusinessLayer.MusteriManager.MusteriListeleBL();
                else if (aranan.Length < 2)
                    return;
                else
                    dataGridView4.DataSource = BusinessLayer.MusteriManager.MusteriAraBL(aranan);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Arama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            #region HoşGeldin Ve Yetki Kontrolü
            try
            {
                if (KullaniciSession.Calisan_ID <= 0 || string.IsNullOrWhiteSpace(KullaniciSession.AdSoyad))
                {
                    MessageBox.Show("Oturum bulunamadı. Lütfen tekrar giriş yapınız.", "Güvenlik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                lblKarsilama.Text = "Hoş Geldin, " + EntityLayer.KullaniciSession.AdSoyad;

                // YETKİ KONTROLÜ (Departman_ID üzerinden)
                int rutbe = KullaniciSession.Departman_ID;

                // EKRANDAKİ BÜTÜN SEKMELERİ GİZLE
                tabControl1.TabPages.Remove(tabPageCalisanlar);
                tabControl1.TabPages.Remove(tabPageDashboard);
                tabControl1.TabPages.Remove(tabPageDepartmanlar);
                tabControl1.TabPages.Remove(tabPageIadeIslemleri);
                tabControl1.TabPages.Remove(tabPageKasa);
                tabControl1.TabPages.Remove(tabPageMusteriler);
                tabControl1.TabPages.Remove(tabPageUrunler);
                tabControl1.TabPages.Remove(tabPageProfil);
                tabControl1.TabPages.Remove(tabPageSatinAlimlar);
                tabControl1.TabPages.Remove(tabPageSiparisGecmisi);
                tabControl1.TabPages.Remove(tabPageZRaporu);
                tabControl1.TabPages.Remove(tabPageFinans);
                buttonIsCikisi.Enabled = false;
                // Yetki durumuna göre sekmeleri aç
                switch (rutbe)
                {
                    case 1: // YÖNETİM (KAPTAN)
                            // Patron her şeyi görür! Bütün butonları aktif et.
                        tabControl1.TabPages.Add(tabPageCalisanlar);
                        tabControl1.TabPages.Add(tabPageDashboard);
                        tabControl1.TabPages.Add(tabPageDepartmanlar);
                        tabControl1.TabPages.Add(tabPageIadeIslemleri);
                        tabControl1.TabPages.Add(tabPageKasa);
                        tabControl1.TabPages.Add(tabPageMusteriler);
                        tabControl1.TabPages.Add(tabPageUrunler);
                        tabControl1.TabPages.Add(tabPageProfil);
                        tabControl1.TabPages.Add(tabPageSatinAlimlar);
                        tabControl1.TabPages.Add(tabPageSiparisGecmisi);
                        tabControl1.TabPages.Add(tabPageZRaporu);
                        tabControl1.TabPages.Add(tabPageFinans);

                        buttonIsCikisi.Enabled = true; // Patron istediği zaman çıkış yaptırabilsin
                        break;

                    case 2: // KASA / SATIŞ
                        tabControl1.TabPages.Add(tabPageProfil);
                        tabControl1.TabPages.Add(tabPageDashboard);
                        tabControl1.TabPages.Add(tabPageKasa);
                        tabControl1.TabPages.Add(tabPageSiparisGecmisi);
                        tabControl1.TabPages.Add(tabPageIadeIslemleri);
                        tabControl1.TabPages.Add(tabPageMusteriler);

                        break;

                    case 3: // DEPO / LOJİSTİK
                        tabControl1.TabPages.Add(tabPageProfil);
                        tabControl1.TabPages.Add(tabPageDashboard);
                        tabControl1.TabPages.Add(tabPageUrunler);
                        tabControl1.TabPages.Add(tabPageSiparisGecmisi);
                        tabControl1.TabPages.Add(tabPageSatinAlimlar);

                        break;

                    case 4: // MUHASEBE VE FİNANS
                        tabControl1.TabPages.Add(tabPageDashboard);
                        tabControl1.TabPages.Add(tabPageUrunler);
                        tabControl1.TabPages.Add(tabPageSiparisGecmisi);
                        tabControl1.TabPages.Add(tabPageSatinAlimlar);
                        tabControl1.TabPages.Add(tabPageCalisanlar);
                        tabControl1.TabPages.Add(tabPageIadeIslemleri);
                        tabControl1.TabPages.Add(tabPageFinans);
                        button10.Visible = false; // Muhasebe departmanı çalışanları yeni personel ekleyemezler, bu yüzden bu butonu gizliyoruz.

                        break;

                    case 5: // İNSAN KAYNAKLARI
                        tabControl1.TabPages.Add(tabPageDashboard);
                        tabControl1.TabPages.Add(tabPageCalisanlar);
                        tabControl1.TabPages.Add(tabPageMusteriler);
                        break;

                    default:
                        MessageBox.Show("Geçersiz bir departman yetkisi tespit edildi!", "Güvenlik Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }

                lblToplamMusteri.Text = "Toplam Müşteri: " + IstatistikManager.ToplamMusteriBL();
                lblToplamCalisan.Text = "Toplam Çalışan: " + IstatistikManager.ToplamCalisanBL();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sistem verileri yüklenemedi: " + hata.Message);
            }
            #endregion
            //Siparis geçmişi sekmesine tıklandığında sipariş geçmişi tablosunu yenile
            SiparisGecmisiYenile();
            #region ComboBox'a Departmanları Getirme
            // 1. Veritabanındaki tüm departmanları çekip ComboBox'ın içine fırlatıyoruz!
            comboBox1.DisplayMember = "Departman_Adi";
            comboBox1.ValueMember = "Departman_ID";
            comboBox1.DataSource = BusinessLayer.DepartmanManager.DepartmanListeleBL();
            #endregion

            #region ComboBox'a Kategorileri Getirme
            BindingSource bsKategori = new BindingSource();
            bsKategori.DataSource = BusinessLayer.KategoriManager.KategoriListeleBL();

            comboBoxKategori.DisplayMember = "Kategori_Adi";
            comboBoxKategori.ValueMember = "Kategori_ID";
            comboBoxKategori.DataSource = bsKategori; // Tabloyu değil, Tercümanı veriyoruz!
            #endregion

            #region ComboBox'a Ürünleri Getirme
            BindingSource bsUrun = new BindingSource();
            bsUrun.DataSource = BusinessLayer.UrunManager.UrunListeleTabloBL();

            comboBoxUrun.DisplayMember = "Urun_Adi";
            comboBoxUrun.ValueMember = "Urun_ID";
            comboBoxUrun.DataSource = bsUrun;
            #endregion

            #region ComboBox'a Bedenleri Getirme
            BindingSource bsBeden = new BindingSource();
            bsBeden.DataSource = BusinessLayer.BedenManager.BedenListeleBL();

            comboBoxBeden.DisplayMember = "Beden_Ad";
            comboBoxBeden.ValueMember = "Beden_ID";
            comboBoxBeden.DataSource = bsBeden;
            #endregion

            // Program açıldığında View'daki hazır listeyi ekrana bas
            dataGridView2.DataSource = BusinessLayer.EnvanterManager.EnvanterListeleBL();
            //Satın Alımlar sayfasını yenile
            SayfayiYenile();

            #region Sanal Sepet Tablosu Oluşturma
            // SANAL SEPETİMİZİN SÜTUNLARINI OLUŞTURUYORUZ
            sepetTablosu.Columns.Add("Envanter_ID", typeof(int));
            sepetTablosu.Columns.Add("Barkod", typeof(string));
            sepetTablosu.Columns.Add("Urun_Adi", typeof(string));
            sepetTablosu.Columns.Add("Miktar", typeof(int));
            sepetTablosu.Columns.Add("Birim_Fiyati", typeof(decimal));
            sepetTablosu.Columns.Add("Ara_Toplam", typeof(decimal));

            // Sepet Grid'ine bu boş sanal tabloyu bağlıyoruz
            dataGridViewSepet.DataSource = sepetTablosu;
            #endregion

            #region ComboBox'a Müşteri Getirme (Satış ekranında)
            System.Data.SqlClient.SqlParameter[] bosParametre = new System.Data.SqlClient.SqlParameter[0];
            string sorguMusteri = "SELECT Musteri_ID, (Ad + ' ' + Soyad) AS Ad_Soyad FROM Musteriler";
            System.Data.DataTable dtMusteriler = SQLBaglantisi.SorguCalistirTablo(sorguMusteri, bosParametre);

            // Herkes kayıtlı müşteri değildir! En başa "Perakende Müşteri" diye sahte bir satır ekliyoruz.
            System.Data.DataRow sahteSatir = dtMusteriler.NewRow();
            sahteSatir["Musteri_ID"] = 0; // 0 demek, aslında kimse yok demek
            sahteSatir["Ad_Soyad"] = "--- Perakende Müşteri ---";
            dtMusteriler.Rows.InsertAt(sahteSatir, 0); // Listenin en tepesine (0. index) yerleştir

            comboBoxMusteri.DataSource = dtMusteriler;
            comboBoxMusteri.DisplayMember = "Ad_Soyad"; // Kasiyer ismi görecek
            comboBoxMusteri.ValueMember = "Musteri_ID";   // Arka planda ID tutulacak
            #endregion

            #region Özet Ekranına İstatistikleri Getirme
            try
            {
                //Ciroları Hesaplama
                decimal toplamCiro = IstatistikManager.GunlukCiroBL();
                decimal nakitCiro = IstatistikManager.GunlukCiroBL("Nakit");
                decimal kartCiro = IstatistikManager.GunlukCiroBL("Kredi Kartı");
                // Label'lara yazdırma
                labelToplamCiro.Text = "Toplam Ciro: " + toplamCiro.ToString("C2");
                labelNakitCiro.Text = "Nakit Ciro: " + nakitCiro.ToString("C2");
                labelKrediCiro.Text = "Kredi Kartı Ciro: " + kartCiro.ToString("C2");

                //Kritik stokları çekme ve kırmızya boyama
                dataGridViewKritikStok.DataSource = IstatistikManager.KritikStoklariGetirBL();
                dataGridViewKritikStok.BackgroundColor = System.Drawing.Color.Red;
            }
            catch (Exception hata)
            {
                MessageBox.Show("İstatistikler yüklenirken hata oluştu: " + hata.Message);
            }
            #endregion

            #region Müşterileri Listeleme (Müşteriler sekmesi açıldığında)
            dataGridView4.DataSource = BusinessLayer.MusteriManager.MusteriListeleBL();
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            #endregion
            #region ComboBoxa'a İşlem Türü Getirme (Kasa sekmesi)
            // Form açıldığında ComboBox'ı dolduralım (Eğer özelliklerden yapmadıysan)
            cmbIslemTuru.Items.Add("Ek Gelir");
            cmbIslemTuru.Items.Add("Ek Gider");
            cmbIslemTuru.SelectedIndex = 0; // Varsayılan olarak Ek Gelir seçili gelsin
            #endregion
            KasaDurumunuGetir();
        }

        private void KasaDurumunuGetir()
        {
            string sorgu = "SELECT Islem_Tipi AS [İşlem Türü], Tutar, Aciklama AS [Açıklama], Islem_Tarihi AS [Tarih] FROM Kasa_Hareketleri ORDER BY Islem_Tarihi DESC";
            try
            {
                // 1. TABLOYU DOLDURMA İŞLEMİ
                using (SqlConnection baglanti = SQLBaglantisi.BaglantiGetir())
                {
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvKasa.DataSource = dt;

                    decimal toplamGelir = 0;
                    decimal toplamGider = 0;

                    // DataTable içindeki tüm satırları dönüp paraları topluyoruz
                    foreach (DataRow satir in dt.Rows)
                    {
                        decimal tutar = Convert.ToDecimal(satir["Tutar"]);

                        if (tutar > 0)
                            toplamGelir += tutar; // Artıysa Gelirdir
                        else
                            toplamGider += tutar; // Eksi (-) ise Giderdir
                    }

                    // Net kasayı hesaplıyoruz (Gider zaten eksi olduğu için direkt toplayabiliriz)
                    decimal netKasa = toplamGelir + toplamGider;

                    // 3. EKRANDAKİ LABELLARA YAZDIRMA
                    lblToplamGelir.Text = "Toplam Gelir: " + toplamGelir.ToString("C2");
                    lblToplamGider.Text = "Toplam Gider: " + Math.Abs(toplamGider).ToString("C2"); // Eksiyi ekranda göstermemek için Mutlak Değer (Abs) aldık
                    lblNetKasa.Text = "Net Bakiye: " + netKasa.ToString("C2");

                    // Renklendirme Tüyosu: Kasa eksideyse kırmızı, artıdaysa yeşil yansın!
                    lblNetKasa.ForeColor = netKasa < 0 ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hazine dairesine ulaşılamadı: " + ex.Message, "Sistem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)//Şifre Güncelle
        {
            try
            {
                // 1. Session'daki (Yaka Kartındaki) ID'yi alıyoruz
                int aktifKullaniciID = EntityLayer.KullaniciSession.Calisan_ID;

                // 2. Kutulardaki yeni şifreleri alıp Gümrüğe gönderiyoruz
                int sonuc = BusinessLayer.CalisanManager.SifreGuncelleBL(aktifKullaniciID, textBoxYeniSifre.Text, textBoxYeniSifreTekrar.Text);

                if (sonuc > 0)
                {
                    MessageBox.Show("Şifreniz başarıyla güncellendi! Lütfen bir sonraki girişinizde yeni şifrenizi kullanın.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBoxYeniSifre.Text = "";
                    textBoxYeniSifreTekrar.Text = "";
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void GenelToplamGuncelle()
        {
            decimal toplam = 0;
            foreach (System.Data.DataRow satir in sepetTablosu.Rows)
            {
                toplam += Convert.ToDecimal(satir["Ara_Toplam"]);
            }
            label9.Text = toplam.ToString("C2");
        }
        private void SiparisGecmisiYenile()
        {
            try
            {
                dataGridViewSiparisGecmisi.DataSource = BusinessLayer.SiparisManager.SiparisGecmisiniGetirBL();
                dataGridViewSiparisGecmisi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş geçmişi yüklenirken hata: " + hata.Message);
            }
        }

        private void buttonListeyiYenile_Click(object sender, EventArgs e)
        {
            SiparisGecmisiYenile();
        }
        private void SayfayiYenile()
        {
            try
            {
                //Tabloyu yenile
                dataGridViewAlimGecmisi.DataSource = AlimDAL.AlimGecmisiGetir();
                //Tedarikçi combox'ı
                comboBoxTedarikci.DataSource = AlimDAL.TedarikcileriGetir();
                comboBoxTedarikci.DisplayMember = "Firma_Adi";
                comboBoxTedarikci.ValueMember = "Tedarikci_ID";
                //Ürün combox'ı
                comboBoxUrunMal.DataSource = AlimDAL.UrunleriGetir();
                comboBoxUrunMal.DisplayMember = "Urun_Adi";
                comboBoxUrunMal.ValueMember = "Urun_ID";
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSatinAl_Click(object sender, EventArgs e)
        {
            try
            {
                int tedarikciID = Convert.ToInt32(comboBoxTedarikci.SelectedValue);
                int urunID = Convert.ToInt32(comboBoxUrunMal.SelectedValue);
                int miktar = Convert.ToInt32(numericUpMiktar.Value);
                decimal alisFiyati = Convert.ToDecimal(numericUpDownAlisUcreti.Value);
                if (miktar <= 0)
                {
                    MessageBox.Show("Lütfen 0'dan büyük bir adet giriniz!");
                    return;
                }

                if (alisFiyati <= 0)
                {
                    MessageBox.Show("Alış fiyatı 0 veya eksi olamaz!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DataAccessLayer.AlimDAL.SatinAlimYap(urunID, tedarikciID, miktar, alisFiyati))
                {
                    MessageBox.Show("Satın alma işlemi tamamlandı! Mal depoya girdi ve sistem güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ekranı temizle ve listeyi yenile
                    numericUpMiktar.Value = 0;
                    numericUpDownAlisUcreti.Value = 0;
                    SayfayiYenile(); // Yeni aldığın mal anında sağdaki Grid'de en üste düşecek!
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
            }
        }

        private void buttonYenile_Click(object sender, EventArgs e)
        {
            SayfayiYenileZ();
        }

        private void SayfayiYenileZ()
        {
            try
            {
                // 1. Geçmiş raporları tabloya doldur
                dataGridViewGecmisZ.DataSource = DataAccessLayer.RaporDAL.GecmisRaporlariGetir();

                // 2. Bugünün özetini çek ve Label'lara yaz
                System.Data.DataTable ozet = DataAccessLayer.RaporDAL.BugununOzetiniGetir();

                if (ozet.Rows.Count > 0)
                {
                    int fisSayisi = Convert.ToInt32(ozet.Rows[0]["FisSayisi"]);
                    decimal ciro = Convert.ToDecimal(ozet.Rows[0]["Ciro"]);
                    decimal nakit = Convert.ToDecimal(ozet.Rows[0]["NakitToplam"]);
                    decimal kart = Convert.ToDecimal(ozet.Rows[0]["KartToplam"]);

                    labelFisSayisi.Text = fisSayisi.ToString() + " Adet";
                    labelBugunCiro.Text = ciro.ToString("C2");
                    labelBugunNakit.Text = nakit.ToString("C2");
                    labelBugunKart.Text = kart.ToString("C2");

                    // Eğer hiç satış yoksa butonu devre dışı bırak ki boşuna kapatmasın
                    buttonGunuKapat.Enabled = fisSayisi > 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Raporlar yüklenirken hata: " + ex.Message);
            }
        }

        private void buttonGunuKapat_Click(object sender, EventArgs e)
        {
            // Ciddi bir işlem olduğu için önce patrona emin misin diye soralım
            DialogResult cevap = System.Windows.Forms.MessageBox.Show("Bugün için kasayı kapatmak ve Z raporu almak istediğinize emin misiniz? Bu işlem geri alınamaz.", "Kasa Kapatma Onayı", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    // Label'lardaki metinleri tekrar sayıya çevirmek risklidir, o yüzden veritabanından son bir kez daha çekip öyle kaydedelim:
                    System.Data.DataTable ozet = DataAccessLayer.RaporDAL.BugununOzetiniGetir();
                    int fis = Convert.ToInt32(ozet.Rows[0]["FisSayisi"]);
                    decimal ciro = Convert.ToDecimal(ozet.Rows[0]["Ciro"]);
                    decimal nakit = Convert.ToDecimal(ozet.Rows[0]["NakitToplam"]);
                    decimal kart = Convert.ToDecimal(ozet.Rows[0]["KartToplam"]);

                    if (DataAccessLayer.RaporDAL.Z_RaporuKaydet(fis, ciro, nakit, kart))
                    {
                        System.Windows.Forms.MessageBox.Show("Z Raporu başarıyla arşive eklendi! Kasa kapatıldı.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SayfayiYenileZ();
                    }
                }
                catch (Exception hata)
                {
                    System.Windows.Forms.MessageBox.Show("Kapatma sırasında hata: " + hata.Message);
                }
            }
        }

        private void buttonFisAra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFisNo.Text))
            {
                MessageBox.Show("Lütfen fiş numarası giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(textBoxFisNo.Text.Trim(), out int siparisId))
            {
                System.Data.DataTable dt = DataAccessLayer.IadeDAL.FisiGetir(siparisId);

                if (dt.Rows.Count > 0)
                {
                    dataGridViewFisDetaylari.DataSource = dt;

                    // Kullanıcı kafası karışmasın diye arka planda çalışan ID sütunlarını gizliyoruz
                    dataGridViewFisDetaylari.Columns["Siparis_Detay_ID"].Visible = false;
                    dataGridViewFisDetaylari.Columns["Siparis_ID"].Visible = false;
                    dataGridViewFisDetaylari.Columns["Envanter_ID"].Visible = false;
                }
                else
                {
                    MessageBox.Show("Bu numaraya ait bir sipariş bulunamadı!", "Uyarı");
                }
            }
            else
            {
                MessageBox.Show("Fiş numarası yalnızca rakamlardan oluşmalıdır!", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonIadeAl_Click(object sender, EventArgs e)
        {
            if (dataGridViewFisDetaylari.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen tablodan iade edilecek ürünü seçin!");
                return;
            }

            if (comboBoxIadeNedeni.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir iade nedeni seçiniz!");
                return;
            }

            // Grid'den gizli ve açık verileri çekiyoruz
            int siparisDetayId = Convert.ToInt32(dataGridViewFisDetaylari.SelectedRows[0].Cells["Siparis_Detay_ID"].Value);
            int siparisId = Convert.ToInt32(dataGridViewFisDetaylari.SelectedRows[0].Cells["Siparis_ID"].Value);
            int envanterId = Convert.ToInt32(dataGridViewFisDetaylari.SelectedRows[0].Cells["Envanter_ID"].Value);

            int miktar = Convert.ToInt32(dataGridViewFisDetaylari.SelectedRows[0].Cells["Satılan Adet"].Value);
            decimal iadeTutari = Convert.ToDecimal(dataGridViewFisDetaylari.SelectedRows[0].Cells["Toplam Tutar"].Value);
            string iadeNedeni = comboBoxIadeNedeni.SelectedItem.ToString();

            DialogResult onay = MessageBox.Show($"Bu ürünü iade alıp, kasadan {iadeTutari:C2} ödeme yapmayı onaylıyor musunuz?", "İade Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (onay == DialogResult.Yes)
            {
                if (DataAccessLayer.IadeDAL.IadeIsleminiYap(siparisDetayId, siparisId, envanterId, miktar, iadeTutari, iadeNedeni))
                {
                    MessageBox.Show("İade işlemi başarıyla tamamlandı. Stok güncellendi ve para kasadan düşüldü.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ekranı temizle ve listeyi yenile
                    dataGridViewFisDetaylari.DataSource = null;
                    textBoxFisNo.Clear();
                    comboBoxIadeNedeni.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("İade işlemi sırasında veritabanında bir hata oluştu!");
                }
            }
        }

        private void buttonMaasGuncelle_Click(object sender, EventArgs e)
        {
            string girilenTC = textBoxTC.Text.Trim();
            decimal girilenMaas = numericUpDownMaas.Value;

            // TC Alanı boş bırakılmış mı kontrolü
            if (string.IsNullOrEmpty(girilenTC) || girilenTC.Length != 11)
            {
                MessageBox.Show("Lütfen 11 haneli geçerli bir TC Kimlik Numarası giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (girilenMaas <= 0)
            {
                MessageBox.Show("Maaş tutarı 0 veya daha düşük olamaz!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult cevap = MessageBox.Show($"{girilenTC} TC numaralı personelin maaşını {girilenMaas:C2} olarak güncellemek istiyor musunuz?\n\n(Bu işlem sistem tarafından loglanacaktır.)", "Maaş Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    // DAL sınıfımızdaki metodu çağırıyoruz
                    bool basarili = CalisanManager.MaasGuncelleBL(girilenTC, girilenMaas);

                    if (basarili)
                    {
                        MessageBox.Show("Maaş başarıyla güncellendi ve loglara kaydedildi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Formu temizle
                        textBoxTC.Clear();
                        numericUpDownMaas.Value = 0; // Veya asgari ücret değeri yazabilirsin

                        // 1. İş Katmanından (BusinessLayer) çalışan listesini istiyoruz
                        var calisanListesi = CalisanManager.CalisanListeleBL();

                        // 2. Gelen listeyi Çalışanlar sekmesindeki DataGridView'e (tabloya) bağlıyoruz.
                        // DİKKAT: Senin tablonun adı dataGridView2 değilse, buradaki ismi kendi tablonun adıyla değiştir.
                        dataGridView3.DataSource = calisanListesi;
                    }
                    else
                    {
                        MessageBox.Show("Bu TC Kimlik numarasına ait bir personel bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message);
                }
            }
        }

        private void buttonIsCikisi_Click(object sender, EventArgs e)
        {
            string girilenTC = textBoxTC.Text.Trim();

            if (string.IsNullOrEmpty(girilenTC) || girilenTC.Length != 11)
            {
                MessageBox.Show("Lütfen işine son verilecek personelin 11 haneli TC Kimlik Numarasını giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kendi kendini kovmasını engelle! (Patron TC'si 11111111111 ise)
            if (girilenTC == "11111111111")
            {
                MessageBox.Show("Yönetici İşten Atılamaz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult cevap = MessageBox.Show($"{girilenTC} TC numaralı personelin işine son vermek (sistemden erişimini kesmek) istediğinize emin misiniz?", "İşten Çıkarma Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    bool basarili = CalisanManager.IstenCikarBL(girilenTC);

                    if (basarili)
                    {
                        MessageBox.Show("Personelin işine son verildi. Artık sisteme giriş yapamaz!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        textBoxTC.Clear();
                        // Ekrandaki çalışanlar listesini yenile (Ama yenilerken sadece Aktif olanları getirmeyi unutma!)
                    }
                    else
                    {
                        MessageBox.Show("Bu TC Kimlik numarasına ait bir personel bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında hata oluştu: " + ex.Message);
                }
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            KasaDurumunuGetir();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // 1. Güvenlik Kontrolleri (Validation)
            if (string.IsNullOrWhiteSpace(txtTutar.Text) || string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                MessageBox.Show("Lütfen tutar ve açıklama alanlarını doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtTutar.Text, out decimal girilenTutar) || girilenTutar <= 0)
            {
                MessageBox.Show("Lütfen geçerli ve 0'dan büyük bir tutar girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. İşlem Mantığı (Giderse tutarı eksi yap)
            string islemTipi = cmbIslemTuru.SelectedItem.ToString();
            decimal islemTutari = (islemTipi == "Ek Gider") ? (girilenTutar * -1) : girilenTutar;

            // 3. Veritabanına Kayıt (SQL Injection'a karşı parametreli sorgu!)
            string sorgu = "INSERT INTO Kasa_Hareketleri (Islem_Tipi, Tutar, Aciklama) VALUES (@tip, @tutar, @aciklama)";

            try
            {
                using (SqlConnection baglanti = SQLBaglantisi.BaglantiGetir())
                {
                    SqlCommand komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@tip", islemTipi.ToUpper());
                    komut.Parameters.AddWithValue("@tutar", islemTutari);
                    komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);

                    komut.ExecuteNonQuery();
                }

                MessageBox.Show("İşlem başarıyla kasaya yansıtıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu temizle ve tabloyu güncelle
                txtTutar.Clear();
                txtAciklama.Clear();
                KasaDurumunuGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında bir hata oluştu: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
