namespace LoomOS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            dataGridView1 = new DataGridView();
            tabControl1 = new TabControl();
            tabPageDashboard = new TabPage();
            lblKarsilama = new Label();
            lblToplamCalisan = new Label();
            lblToplamMusteri = new Label();
            tabPageDepartmanlar = new TabPage();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            textBox2 = new TextBox();
            tabPageCalisanlar = new TabPage();
            label5 = new Label();
            textBoxTelNO = new TextBox();
            label4 = new Label();
            textBoxTCNO = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button10 = new Button();
            comboBox1 = new ComboBox();
            textBoxCalisanSoyad = new TextBox();
            textBoxCalisanAd = new TextBox();
            dataGridView3 = new DataGridView();
            button4 = new Button();
            button3 = new Button();
            tabPageUrunler = new TabPage();
            dataGridView2 = new DataGridView();
            button2 = new Button();
            tabPageMusteriler = new TabPage();
            button6 = new Button();
            textBox1 = new TextBox();
            dataGridView4 = new DataGridView();
            button5 = new Button();
            tabPageProfil = new TabPage();
            button11 = new Button();
            label7 = new Label();
            label6 = new Label();
            textBoxYeniSifreTekrar = new TextBox();
            textBoxYeniSifre = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPageDashboard.SuspendLayout();
            tabPageDepartmanlar.SuspendLayout();
            tabPageCalisanlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            tabPageUrunler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPageMusteriler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            tabPageProfil.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(8, 6);
            button1.Name = "button1";
            button1.Size = new Size(180, 40);
            button1.TabIndex = 0;
            button1.Text = "Departmanları Getir\r\n";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 50);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(770, 320);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageDashboard);
            tabControl1.Controls.Add(tabPageDepartmanlar);
            tabControl1.Controls.Add(tabPageCalisanlar);
            tabControl1.Controls.Add(tabPageUrunler);
            tabControl1.Controls.Add(tabPageMusteriler);
            tabControl1.Controls.Add(tabPageProfil);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(780, 440);
            tabControl1.TabIndex = 3;
            // 
            // tabPageDashboard
            // 
            tabPageDashboard.Controls.Add(lblKarsilama);
            tabPageDashboard.Controls.Add(lblToplamCalisan);
            tabPageDashboard.Controls.Add(lblToplamMusteri);
            tabPageDashboard.Location = new Point(4, 29);
            tabPageDashboard.Name = "tabPageDashboard";
            tabPageDashboard.Padding = new Padding(3);
            tabPageDashboard.Size = new Size(772, 407);
            tabPageDashboard.TabIndex = 4;
            tabPageDashboard.Text = "Özet";
            tabPageDashboard.UseVisualStyleBackColor = true;
            // 
            // lblKarsilama
            // 
            lblKarsilama.AutoSize = true;
            lblKarsilama.Location = new Point(478, 48);
            lblKarsilama.Name = "lblKarsilama";
            lblKarsilama.Size = new Size(50, 20);
            lblKarsilama.TabIndex = 2;
            lblKarsilama.Text = "label1";
            // 
            // lblToplamCalisan
            // 
            lblToplamCalisan.AutoSize = true;
            lblToplamCalisan.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblToplamCalisan.Location = new Point(405, 101);
            lblToplamCalisan.Name = "lblToplamCalisan";
            lblToplamCalisan.Size = new Size(50, 38);
            lblToplamCalisan.TabIndex = 1;
            lblToplamCalisan.Text = "---";
            // 
            // lblToplamMusteri
            // 
            lblToplamMusteri.AutoSize = true;
            lblToplamMusteri.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblToplamMusteri.Location = new Point(174, 89);
            lblToplamMusteri.Name = "lblToplamMusteri";
            lblToplamMusteri.Size = new Size(50, 38);
            lblToplamMusteri.TabIndex = 0;
            lblToplamMusteri.Text = "---";
            // 
            // tabPageDepartmanlar
            // 
            tabPageDepartmanlar.Controls.Add(button9);
            tabPageDepartmanlar.Controls.Add(button8);
            tabPageDepartmanlar.Controls.Add(button7);
            tabPageDepartmanlar.Controls.Add(textBox2);
            tabPageDepartmanlar.Controls.Add(button1);
            tabPageDepartmanlar.Controls.Add(dataGridView1);
            tabPageDepartmanlar.Location = new Point(4, 29);
            tabPageDepartmanlar.Name = "tabPageDepartmanlar";
            tabPageDepartmanlar.Padding = new Padding(3);
            tabPageDepartmanlar.Size = new Size(772, 407);
            tabPageDepartmanlar.TabIndex = 0;
            tabPageDepartmanlar.Text = "Departmanlar";
            tabPageDepartmanlar.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(150, 371);
            button9.Name = "button9";
            button9.Size = new Size(77, 30);
            button9.TabIndex = 5;
            button9.Text = "Güncelle";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(79, 372);
            button8.Name = "button8";
            button8.Size = new Size(65, 30);
            button8.TabIndex = 4;
            button8.Text = "Sil";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Location = new Point(8, 372);
            button7.Name = "button7";
            button7.Size = new Size(65, 30);
            button7.TabIndex = 3;
            button7.Text = "Ekle";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(242, 374);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(180, 27);
            textBox2.TabIndex = 2;
            // 
            // tabPageCalisanlar
            // 
            tabPageCalisanlar.Controls.Add(label5);
            tabPageCalisanlar.Controls.Add(textBoxTelNO);
            tabPageCalisanlar.Controls.Add(label4);
            tabPageCalisanlar.Controls.Add(textBoxTCNO);
            tabPageCalisanlar.Controls.Add(label3);
            tabPageCalisanlar.Controls.Add(label2);
            tabPageCalisanlar.Controls.Add(label1);
            tabPageCalisanlar.Controls.Add(button10);
            tabPageCalisanlar.Controls.Add(comboBox1);
            tabPageCalisanlar.Controls.Add(textBoxCalisanSoyad);
            tabPageCalisanlar.Controls.Add(textBoxCalisanAd);
            tabPageCalisanlar.Controls.Add(dataGridView3);
            tabPageCalisanlar.Controls.Add(button4);
            tabPageCalisanlar.Controls.Add(button3);
            tabPageCalisanlar.Location = new Point(4, 29);
            tabPageCalisanlar.Name = "tabPageCalisanlar";
            tabPageCalisanlar.Padding = new Padding(3);
            tabPageCalisanlar.Size = new Size(772, 407);
            tabPageCalisanlar.TabIndex = 1;
            tabPageCalisanlar.Text = "Çalışanlar";
            tabPageCalisanlar.UseVisualStyleBackColor = true;
            tabPageCalisanlar.Click += tabPageCalisanlar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(402, 217);
            label5.Name = "label5";
            label5.Size = new Size(125, 20);
            label5.TabIndex = 13;
            label5.Text = "Telefon Numarası";
            // 
            // textBoxTelNO
            // 
            textBoxTelNO.Location = new Point(402, 240);
            textBoxTelNO.Name = "textBoxTelNO";
            textBoxTelNO.Size = new Size(151, 27);
            textBoxTelNO.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(402, 159);
            label4.Name = "label4";
            label4.Size = new Size(144, 20);
            label4.TabIndex = 11;
            label4.Text = "T.C. Kimlik Numarası";
            label4.Click += label4_Click;
            // 
            // textBoxTCNO
            // 
            textBoxTCNO.Location = new Point(402, 182);
            textBoxTCNO.Name = "textBoxTCNO";
            textBoxTCNO.Size = new Size(151, 27);
            textBoxTCNO.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(402, 279);
            label3.Name = "label3";
            label3.Size = new Size(141, 20);
            label3.TabIndex = 9;
            label3.Text = "Çalıştığı Departman";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(402, 96);
            label2.Name = "label2";
            label2.Size = new Size(105, 20);
            label2.TabIndex = 8;
            label2.Text = "Çalışan Soyadı";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(402, 36);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 7;
            label1.Text = "Çalışan Adı";
            // 
            // button10
            // 
            button10.Location = new Point(402, 357);
            button10.Name = "button10";
            button10.Size = new Size(151, 29);
            button10.TabIndex = 6;
            button10.Text = "Personel Ekle";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(402, 302);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 5;
            // 
            // textBoxCalisanSoyad
            // 
            textBoxCalisanSoyad.Location = new Point(402, 119);
            textBoxCalisanSoyad.Name = "textBoxCalisanSoyad";
            textBoxCalisanSoyad.Size = new Size(151, 27);
            textBoxCalisanSoyad.TabIndex = 4;
            // 
            // textBoxCalisanAd
            // 
            textBoxCalisanAd.Location = new Point(402, 59);
            textBoxCalisanAd.Name = "textBoxCalisanAd";
            textBoxCalisanAd.Size = new Size(151, 27);
            textBoxCalisanAd.TabIndex = 3;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(0, 50);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new Size(396, 350);
            dataGridView3.TabIndex = 2;
            dataGridView3.CellContentClick += dataGridView3_CellContentClick;
            // 
            // button4
            // 
            button4.Location = new Point(6, 4);
            button4.Name = "button4";
            button4.Size = new Size(180, 40);
            button4.TabIndex = 1;
            button4.Text = "Çalışanları Getir";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(169, 59);
            button3.Name = "button3";
            button3.Size = new Size(8, 8);
            button3.TabIndex = 0;
            button3.Text = "z";
            button3.UseVisualStyleBackColor = true;
            // 
            // tabPageUrunler
            // 
            tabPageUrunler.Controls.Add(dataGridView2);
            tabPageUrunler.Controls.Add(button2);
            tabPageUrunler.Location = new Point(4, 29);
            tabPageUrunler.Name = "tabPageUrunler";
            tabPageUrunler.Padding = new Padding(3);
            tabPageUrunler.Size = new Size(772, 407);
            tabPageUrunler.TabIndex = 2;
            tabPageUrunler.Text = "Ürünler";
            tabPageUrunler.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(0, 50);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(780, 350);
            dataGridView2.TabIndex = 1;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // button2
            // 
            button2.Location = new Point(8, 6);
            button2.Name = "button2";
            button2.Size = new Size(180, 40);
            button2.TabIndex = 0;
            button2.Text = "Ürünleri Getir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabPageMusteriler
            // 
            tabPageMusteriler.Controls.Add(button6);
            tabPageMusteriler.Controls.Add(textBox1);
            tabPageMusteriler.Controls.Add(dataGridView4);
            tabPageMusteriler.Controls.Add(button5);
            tabPageMusteriler.Location = new Point(4, 29);
            tabPageMusteriler.Name = "tabPageMusteriler";
            tabPageMusteriler.Padding = new Padding(3);
            tabPageMusteriler.Size = new Size(772, 407);
            tabPageMusteriler.TabIndex = 3;
            tabPageMusteriler.Text = "Müşteriler";
            tabPageMusteriler.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(711, 10);
            button6.Name = "button6";
            button6.Size = new Size(55, 30);
            button6.TabIndex = 3;
            button6.Text = "ARA";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(541, 13);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(162, 27);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(0, 50);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 51;
            dataGridView4.Size = new Size(780, 350);
            dataGridView4.TabIndex = 1;
            dataGridView4.CellContentClick += dataGridView4_CellContentClick;
            // 
            // button5
            // 
            button5.Location = new Point(8, 6);
            button5.Name = "button5";
            button5.Size = new Size(190, 40);
            button5.TabIndex = 0;
            button5.Text = "Müşterileri Getir";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // tabPageProfil
            // 
            tabPageProfil.Controls.Add(button11);
            tabPageProfil.Controls.Add(label7);
            tabPageProfil.Controls.Add(label6);
            tabPageProfil.Controls.Add(textBoxYeniSifreTekrar);
            tabPageProfil.Controls.Add(textBoxYeniSifre);
            tabPageProfil.Location = new Point(4, 29);
            tabPageProfil.Name = "tabPageProfil";
            tabPageProfil.Padding = new Padding(3);
            tabPageProfil.Size = new Size(772, 407);
            tabPageProfil.TabIndex = 5;
            tabPageProfil.Text = "Profilim";
            tabPageProfil.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Location = new Point(108, 211);
            button11.Name = "button11";
            button11.Size = new Size(149, 30);
            button11.TabIndex = 4;
            button11.Text = "Şifremi Güncelle";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(108, 122);
            label7.Name = "label7";
            label7.Size = new Size(114, 20);
            label7.TabIndex = 3;
            label7.Text = "Yeni Şifre Tekrar";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(108, 61);
            label6.Name = "label6";
            label6.Size = new Size(70, 20);
            label6.TabIndex = 2;
            label6.Text = "Yeni Şifre";
            label6.Click += label6_Click;
            // 
            // textBoxYeniSifreTekrar
            // 
            textBoxYeniSifreTekrar.Location = new Point(108, 145);
            textBoxYeniSifreTekrar.Name = "textBoxYeniSifreTekrar";
            textBoxYeniSifreTekrar.PasswordChar = '*';
            textBoxYeniSifreTekrar.Size = new Size(149, 27);
            textBoxYeniSifreTekrar.TabIndex = 1;
            // 
            // textBoxYeniSifre
            // 
            textBoxYeniSifre.Location = new Point(108, 84);
            textBoxYeniSifre.Name = "textBoxYeniSifre";
            textBoxYeniSifre.PasswordChar = '*';
            textBoxYeniSifre.Size = new Size(149, 27);
            textBoxYeniSifre.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPageDashboard.ResumeLayout(false);
            tabPageDashboard.PerformLayout();
            tabPageDepartmanlar.ResumeLayout(false);
            tabPageDepartmanlar.PerformLayout();
            tabPageCalisanlar.ResumeLayout(false);
            tabPageCalisanlar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            tabPageUrunler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPageMusteriler.ResumeLayout(false);
            tabPageMusteriler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            tabPageProfil.ResumeLayout(false);
            tabPageProfil.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private DataGridView dataGridView1;
        private TabControl tabControl1;
        private TabPage tabPageDepartmanlar;
        private TabPage tabPageCalisanlar;
        private TabPage tabPageUrunler;
        private Button button2;
        private DataGridView dataGridView2;
        private Button button4;
        private Button button3;
        private DataGridView dataGridView3;
        private TabPage tabPageMusteriler;
        private Button button5;
        private Button button6;
        private TextBox textBox1;
        private DataGridView dataGridView4;
        private TabPage tabPageDashboard;
        private Label lblToplamMusteri;
        private Label lblToplamCalisan;
        private Button button7;
        private TextBox textBox2;
        private Button button8;
        private Button button9;
        private Label lblKarsilama;
        private Button button10;
        private ComboBox comboBox1;
        private TextBox textBoxCalisanSoyad;
        private TextBox textBoxCalisanAd;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox textBoxTCNO;
        private Label label5;
        private TextBox textBoxTelNO;
        private TabPage tabPageProfil;
        private Button button11;
        private Label label7;
        private Label label6;
        private TextBox textBoxYeniSifreTekrar;
        private TextBox textBoxYeniSifre;
    }
}
