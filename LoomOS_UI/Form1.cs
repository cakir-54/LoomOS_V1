using BusinessLayer;
using DataAccessLayer;
using EntityLayer;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace LoomOS
{
    public partial class Form1 : Form
    {
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
                // DataGridView'de seçili olan satırın 0. hücresindeki (yani ID kısmındaki) değeri alıyoruz
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
                Departman guncellenecekDep = new Departman();

                // 1. Güncellenecek ID'yi tablodan (seçili satırdan) al
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Bussinnes Layer'a gidip "Bana ürünleri getir" diyoruz.
                var urunListesi = UrunManager.UrunListeleBL();
                //Gelen listeyi ekrandaki tablomuza bağlıyoruz.
                dataGridView2.DataSource = urunListesi;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veri çekilirken bir hata oluştu: \n\n" + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        
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
                    dataGridView3.DataSource = BusinessLayer.EnvanterManager.EnvanterListeleBL();
                    // Ekranı yeni stok girişine hazırla (Ürün adı kalsın, beden/renk temizlensin)
                    textBoxRenk.Clear(); numericUpDownStok.Value = 0; numericUpDownAlisFiyati.Value = 0; numericUpDownSatisFiyati.Value = 0; comboBoxBeden.SelectedIndex = -1;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Stok Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

                // 3. YENİ KONTROL: Eğer SQL'den dönen listenin içi BOŞSA (Count == 0)
                if (aramaSonucu.Count == 0)
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
            dataGridView4.DataSource = MusteriManager.MusteriListeleBL();
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. KİŞİYİ KARŞILA
                lblKarsilama.Text = "Hoş Geldin, " + EntityLayer.KullaniciSession.AdSoyad;

                // 2. YETKİ KONTROLÜ! (Role-Based Authorization)
                // Varsayım: Admin/Yönetici departmanının ID'si 1 olsun.
                if (EntityLayer.KullaniciSession.Departman_ID != 5)
                {
                    // Eğer giren kişi Admin değilse, Çalışanlar ve Departmanlar sekmesini gizle!
                    tabControl1.TabPages.Remove(tabPageCalisanlar);
                    tabControl1.TabPages.Remove(tabPageDepartmanlar);

                }

                lblToplamMusteri.Text = "Toplam Müşteri: " + IstatistikManager.ToplamMusteriBL();
                lblToplamCalisan.Text = "Toplam Çalışan: " + IstatistikManager.ToplamCalisanBL();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sistem verileri yüklenemedi: " + hata.Message);
            }
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
    }
    
}
