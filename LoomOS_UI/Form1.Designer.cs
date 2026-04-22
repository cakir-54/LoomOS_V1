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
            tabPage5 = new TabPage();
            lblToplamCalisan = new Label();
            lblToplamMusteri = new Label();
            tabPage1 = new TabPage();
            button8 = new Button();
            button7 = new Button();
            textBox2 = new TextBox();
            tabPage2 = new TabPage();
            dataGridView3 = new DataGridView();
            button4 = new Button();
            button3 = new Button();
            tabPage3 = new TabPage();
            dataGridView2 = new DataGridView();
            button2 = new Button();
            tabPage4 = new TabPage();
            button6 = new Button();
            textBox1 = new TextBox();
            dataGridView4 = new DataGridView();
            button5 = new Button();
            button9 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
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
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(780, 440);
            tabControl1.TabIndex = 3;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(lblToplamCalisan);
            tabPage5.Controls.Add(lblToplamMusteri);
            tabPage5.Location = new Point(4, 29);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(772, 407);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Özet";
            tabPage5.UseVisualStyleBackColor = true;
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
            // tabPage1
            // 
            tabPage1.Controls.Add(button9);
            tabPage1.Controls.Add(button8);
            tabPage1.Controls.Add(button7);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(772, 407);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Departmanlar";
            tabPage1.UseVisualStyleBackColor = true;
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
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView3);
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(button3);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(772, 407);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Çalışanlar";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(0, 50);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new Size(780, 350);
            dataGridView3.TabIndex = 2;
            dataGridView3.CellContentClick += dataGridView3_CellContentClick;
            // 
            // button4
            // 
            button4.Location = new Point(8, 6);
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
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridView2);
            tabPage3.Controls.Add(button2);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(772, 407);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Ürünler";
            tabPage3.UseVisualStyleBackColor = true;
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
            // tabPage4
            // 
            tabPage4.Controls.Add(button6);
            tabPage4.Controls.Add(textBox1);
            tabPage4.Controls.Add(dataGridView4);
            tabPage4.Controls.Add(button5);
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(772, 407);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Müşteriler";
            tabPage4.UseVisualStyleBackColor = true;
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
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private DataGridView dataGridView1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button button2;
        private DataGridView dataGridView2;
        private Button button4;
        private Button button3;
        private DataGridView dataGridView3;
        private TabPage tabPage4;
        private Button button5;
        private Button button6;
        private TextBox textBox1;
        private DataGridView dataGridView4;
        private TabPage tabPage5;
        private Label lblToplamMusteri;
        private Label lblToplamCalisan;
        private Button button7;
        private TextBox textBox2;
        private Button button8;
        private Button button9;
    }
}
