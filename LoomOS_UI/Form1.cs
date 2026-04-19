using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using DataAccessLayer;
using BusinessLayer;
namespace LoomOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. İş Katmanına (Business) gidip "Bana departmanları getir" diyoruz.
                var departmanListesi = DepartmanManager.DepartmanListele();

                // 2. Gelen listeyi ekrandaki tablomuza (DataGridView) bağlıyoruz.
                dataGridView1.DataSource = departmanListesi;

                MessageBox.Show("Veriler N-Tier Mimarisiyle Başarıyla Çekildi!", "Operasyon Tamam", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veri çekilirken bir hata oluştu: \n\n" + hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
