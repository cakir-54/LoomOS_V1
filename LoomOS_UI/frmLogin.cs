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
            try
            {
                Calisan girenKisi = BusinessLayer.CalisanManager.GirisKontrolBL(txtTC.Text, txtSifre.Text);

                if (girenKisi != null) // Yani eğer şifre doğruysa ve sistem birini bulduysa
                {
                    //Yaka kartına yazıyoruz
                    EntityLayer.KullaniciSession.Calisan_ID = girenKisi.Calisan_ID;
                    EntityLayer.KullaniciSession.Departman_ID = girenKisi.Departman_ID;
                    EntityLayer.KullaniciSession.AdSoyad = girenKisi.Calisan_Ad + " " + girenKisi.Calisan_Soyad;
                    //Ana sayfaya yönlendiriyoruz
                    Form1 anaSayfa = new Form1();
                    anaSayfa.Show();
                    //Giriş formunu gizliyoruz
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı TC Kimlik No veya Şifre girdiniz!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
