namespace LoomOS
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtTC = new TextBox();
            txtSifre = new TextBox();
            login = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtTC
            // 
            txtTC.Location = new Point(278, 141);
            txtTC.Name = "txtTC";
            txtTC.Size = new Size(210, 27);
            txtTC.TabIndex = 0;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(278, 208);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '*';
            txtSifre.Size = new Size(210, 27);
            txtSifre.TabIndex = 1;
            // 
            // login
            // 
            login.Location = new Point(326, 258);
            login.Name = "login";
            login.Size = new Size(94, 29);
            login.TabIndex = 2;
            login.Text = "Giriş Yap";
            login.UseVisualStyleBackColor = true;
            login.Click += login_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(278, 118);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 3;
            label1.Text = "TC Kimlik NO";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 185);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 4;
            label2.Text = "Parola";
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(login);
            Controls.Add(txtSifre);
            Controls.Add(txtTC);
            Name = "frmLogin";
            Text = "frmLogin";
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTC;
        private TextBox txtSifre;
        private Button login;
        private Label label1;
        private Label label2;
    }
}