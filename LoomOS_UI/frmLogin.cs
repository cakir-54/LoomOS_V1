using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            string tc = txtTC.Text;
            string sifre = txtSifre.Text;

            System.Data.DataTable sonuc = DataAccessLayer.LoginDAL.KullaniciGirisiniKontrolEt(tc, sifre);

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
                MessageBox.Show("Hatalı TC Kimlik Numarası veya Şifre!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
