using EntityLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace LoomOS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                string tc = txtTC.Text.Trim();
                string sifre = txtSifre.Text;

                if (string.IsNullOrWhiteSpace(tc) || string.IsNullOrWhiteSpace(sifre))
                {
                    MessageBox.Show("TC Kimlik No ve şifre alanları boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (tc.Length != 11)
                {
                    MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır!", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable sonuc = DataAccessLayer.LoginDAL.KullaniciGirisiniKontrolEt(tc, sifre);

                if (sonuc.Rows.Count > 0)
                {
                    KullaniciSession.AdSoyad = sonuc.Rows[0]["Ad"].ToString() + " " + sonuc.Rows[0]["Soyad"].ToString();
                    KullaniciSession.Departman_ID = Convert.ToInt32(sonuc.Rows[0]["Departman_ID"]);
                    KullaniciSession.Calisan_ID = Convert.ToInt32(sonuc.Rows[0]["Calisan_ID"]);

                    Form1 anaEkran = new Form1();
                    anaEkran.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı TC Kimlik Numarası, şifre veya pasif hesap!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veritabanına bağlanılamadı veya giriş işlemi başarısız:\n" + hata.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }
    }
}
