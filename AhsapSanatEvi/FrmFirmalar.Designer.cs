
namespace AhsapSanatEvi
{
    partial class FrmFirmalar
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtBxFirmaAdı = new System.Windows.Forms.TextBox();
            this.BtnFirmaGuncelle = new System.Windows.Forms.Button();
            this.LblFirmaAd = new System.Windows.Forms.Label();
            this.BtnFirmaSil = new System.Windows.Forms.Button();
            this.BtnFirmaEkle = new System.Windows.Forms.Button();
            this.TxtBxFirmaID = new System.Windows.Forms.TextBox();
            this.LblFirmaID = new System.Windows.Forms.Label();
            this.FirmaListePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtBxFirmaAdı);
            this.groupBox1.Controls.Add(this.BtnFirmaGuncelle);
            this.groupBox1.Controls.Add(this.LblFirmaAd);
            this.groupBox1.Controls.Add(this.BtnFirmaSil);
            this.groupBox1.Controls.Add(this.BtnFirmaEkle);
            this.groupBox1.Controls.Add(this.TxtBxFirmaID);
            this.groupBox1.Controls.Add(this.LblFirmaID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.groupBox1.Location = new System.Drawing.Point(572, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 537);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // TxtBxFirmaAdı
            // 
            this.TxtBxFirmaAdı.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TxtBxFirmaAdı.Location = new System.Drawing.Point(18, 258);
            this.TxtBxFirmaAdı.Name = "TxtBxFirmaAdı";
            this.TxtBxFirmaAdı.Size = new System.Drawing.Size(276, 33);
            this.TxtBxFirmaAdı.TabIndex = 7;
            // 
            // BtnFirmaGuncelle
            // 
            this.BtnFirmaGuncelle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnFirmaGuncelle.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnFirmaGuncelle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BtnFirmaGuncelle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(226)))), ((int)(((byte)(242)))));
            this.BtnFirmaGuncelle.FlatAppearance.BorderSize = 3;
            this.BtnFirmaGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirmaGuncelle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnFirmaGuncelle.Location = new System.Drawing.Point(100, 373);
            this.BtnFirmaGuncelle.Name = "BtnFirmaGuncelle";
            this.BtnFirmaGuncelle.Size = new System.Drawing.Size(117, 42);
            this.BtnFirmaGuncelle.TabIndex = 4;
            this.BtnFirmaGuncelle.Text = "Güncelle";
            this.BtnFirmaGuncelle.UseVisualStyleBackColor = false;
            this.BtnFirmaGuncelle.Click += new System.EventHandler(this.BtnFirmaGuncelle_Click);
            // 
            // LblFirmaAd
            // 
            this.LblFirmaAd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblFirmaAd.AutoSize = true;
            this.LblFirmaAd.Location = new System.Drawing.Point(109, 220);
            this.LblFirmaAd.Name = "LblFirmaAd";
            this.LblFirmaAd.Size = new System.Drawing.Size(95, 25);
            this.LblFirmaAd.TabIndex = 6;
            this.LblFirmaAd.Text = "Firma Adı";
            // 
            // BtnFirmaSil
            // 
            this.BtnFirmaSil.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnFirmaSil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(79)))), ((int)(((byte)(95)))));
            this.BtnFirmaSil.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BtnFirmaSil.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(136)))), ((int)(((byte)(148)))));
            this.BtnFirmaSil.FlatAppearance.BorderSize = 3;
            this.BtnFirmaSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirmaSil.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnFirmaSil.Location = new System.Drawing.Point(175, 319);
            this.BtnFirmaSil.Name = "BtnFirmaSil";
            this.BtnFirmaSil.Size = new System.Drawing.Size(77, 42);
            this.BtnFirmaSil.TabIndex = 5;
            this.BtnFirmaSil.Text = "Sil";
            this.BtnFirmaSil.UseVisualStyleBackColor = false;
            this.BtnFirmaSil.Click += new System.EventHandler(this.BtnFirmaSil_Click);
            // 
            // BtnFirmaEkle
            // 
            this.BtnFirmaEkle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnFirmaEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(223)))), ((int)(((byte)(80)))));
            this.BtnFirmaEkle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.BtnFirmaEkle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(242)))), ((int)(((byte)(137)))));
            this.BtnFirmaEkle.FlatAppearance.BorderSize = 3;
            this.BtnFirmaEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirmaEkle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnFirmaEkle.Location = new System.Drawing.Point(66, 319);
            this.BtnFirmaEkle.Name = "BtnFirmaEkle";
            this.BtnFirmaEkle.Size = new System.Drawing.Size(77, 42);
            this.BtnFirmaEkle.TabIndex = 3;
            this.BtnFirmaEkle.Text = "Ekle";
            this.BtnFirmaEkle.UseVisualStyleBackColor = false;
            this.BtnFirmaEkle.Click += new System.EventHandler(this.BtnFirmaEkle_Click);
            // 
            // TxtBxFirmaID
            // 
            this.TxtBxFirmaID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TxtBxFirmaID.Cursor = System.Windows.Forms.Cursors.No;
            this.TxtBxFirmaID.Enabled = false;
            this.TxtBxFirmaID.Location = new System.Drawing.Point(18, 178);
            this.TxtBxFirmaID.Name = "TxtBxFirmaID";
            this.TxtBxFirmaID.ReadOnly = true;
            this.TxtBxFirmaID.Size = new System.Drawing.Size(276, 33);
            this.TxtBxFirmaID.TabIndex = 1;
            // 
            // LblFirmaID
            // 
            this.LblFirmaID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblFirmaID.AutoSize = true;
            this.LblFirmaID.Location = new System.Drawing.Point(115, 140);
            this.LblFirmaID.Name = "LblFirmaID";
            this.LblFirmaID.Size = new System.Drawing.Size(86, 25);
            this.LblFirmaID.TabIndex = 0;
            this.LblFirmaID.Text = "Firma ID";
            // 
            // FirmaListePanel
            // 
            this.FirmaListePanel.AutoScroll = true;
            this.FirmaListePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FirmaListePanel.Location = new System.Drawing.Point(0, 0);
            this.FirmaListePanel.Name = "FirmaListePanel";
            this.FirmaListePanel.Size = new System.Drawing.Size(572, 537);
            this.FirmaListePanel.TabIndex = 1;
            // 
            // FrmFirmalar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 537);
            this.Controls.Add(this.FirmaListePanel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FrmFirmalar";
            this.Text = "FrmFirmalar";
            this.Load += new System.EventHandler(this.FrmFirmalar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LblFirmaID;
        private System.Windows.Forms.Button BtnFirmaEkle;
        private System.Windows.Forms.Button BtnFirmaSil;
        private System.Windows.Forms.Label LblFirmaAd;
        private System.Windows.Forms.Button BtnFirmaGuncelle;
        private System.Windows.Forms.FlowLayoutPanel FirmaListePanel;
        public System.Windows.Forms.TextBox TxtBxFirmaID;
        public System.Windows.Forms.TextBox TxtBxFirmaAdı;
    }
}