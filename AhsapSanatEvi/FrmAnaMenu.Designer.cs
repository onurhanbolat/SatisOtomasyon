﻿
namespace AhsapSanatEvi
{
    partial class FrmAnaMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnaMenu));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.FullScreenButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.CompanyText = new System.Windows.Forms.Label();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.BtnAyarlar = new System.Windows.Forms.Button();
            this.BtnBilanco = new System.Windows.Forms.Button();
            this.BtnMusteriler = new System.Windows.Forms.Button();
            this.BtnFirmalar = new System.Windows.Forms.Button();
            this.EkleContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnEkle = new System.Windows.Forms.Button();
            this.BtnEkleCerceve = new System.Windows.Forms.Button();
            this.BtnEkleUrun = new System.Windows.Forms.Button();
            this.BtnSepet = new System.Windows.Forms.Button();
            this.BtnUrunler = new System.Windows.Forms.Button();
            this.BtnCerceveler = new System.Windows.Forms.Button();
            this.BtnSatis = new System.Windows.Forms.Button();
            this.BtnAnaMenu = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.AnaMenuArkaPanel = new System.Windows.Forms.Panel();
            this.virtualServerModeSource1 = new DevExpress.Data.VirtualServerModeSource(this.components);
            this.jDragControl1 = new JDragControl.JDragControl(this.components);
            this.TopPanel.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.EkleContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.virtualServerModeSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.TopPanel.Controls.Add(this.MinimizeButton);
            this.TopPanel.Controls.Add(this.FullScreenButton);
            this.TopPanel.Controls.Add(this.ExitButton);
            this.TopPanel.Controls.Add(this.CompanyText);
            this.TopPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1230, 34);
            this.TopPanel.TabIndex = 0;
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.MinimizeButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MinimizeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.MinimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.MinimizeButton.FlatAppearance.BorderSize = 3;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.MinimizeButton.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeButton.Image")));
            this.MinimizeButton.Location = new System.Drawing.Point(1092, 0);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(46, 34);
            this.MinimizeButton.TabIndex = 3;
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // FullScreenButton
            // 
            this.FullScreenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.FullScreenButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FullScreenButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.FullScreenButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(209)))), ((int)(((byte)(209)))));
            this.FullScreenButton.FlatAppearance.BorderSize = 3;
            this.FullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FullScreenButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FullScreenButton.Image = ((System.Drawing.Image)(resources.GetObject("FullScreenButton.Image")));
            this.FullScreenButton.Location = new System.Drawing.Point(1138, 0);
            this.FullScreenButton.Name = "FullScreenButton";
            this.FullScreenButton.Size = new System.Drawing.Size(46, 34);
            this.FullScreenButton.TabIndex = 2;
            this.FullScreenButton.UseVisualStyleBackColor = false;
            this.FullScreenButton.Click += new System.EventHandler(this.FullScreenButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(79)))), ((int)(((byte)(95)))));
            this.ExitButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ExitButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExitButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(136)))), ((int)(((byte)(148)))));
            this.ExitButton.FlatAppearance.BorderSize = 3;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ExitButton.Image = ((System.Drawing.Image)(resources.GetObject("ExitButton.Image")));
            this.ExitButton.Location = new System.Drawing.Point(1184, 0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(46, 34);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // CompanyText
            // 
            this.CompanyText.AutoSize = true;
            this.CompanyText.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.CompanyText.ForeColor = System.Drawing.Color.White;
            this.CompanyText.Location = new System.Drawing.Point(7, 8);
            this.CompanyText.Name = "CompanyText";
            this.CompanyText.Size = new System.Drawing.Size(94, 16);
            this.CompanyText.TabIndex = 0;
            this.CompanyText.Text = "Ahşap Sanat Evi";
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.LeftPanel.Controls.Add(this.BtnAyarlar);
            this.LeftPanel.Controls.Add(this.BtnBilanco);
            this.LeftPanel.Controls.Add(this.BtnMusteriler);
            this.LeftPanel.Controls.Add(this.BtnFirmalar);
            this.LeftPanel.Controls.Add(this.EkleContainer);
            this.LeftPanel.Controls.Add(this.BtnSepet);
            this.LeftPanel.Controls.Add(this.BtnUrunler);
            this.LeftPanel.Controls.Add(this.BtnCerceveler);
            this.LeftPanel.Controls.Add(this.BtnSatis);
            this.LeftPanel.Controls.Add(this.BtnAnaMenu);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 34);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(209, 689);
            this.LeftPanel.TabIndex = 1;
            // 
            // BtnAyarlar
            // 
            this.BtnAyarlar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnAyarlar.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnAyarlar.FlatAppearance.BorderSize = 0;
            this.BtnAyarlar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAyarlar.ForeColor = System.Drawing.Color.White;
            this.BtnAyarlar.Location = new System.Drawing.Point(0, 513);
            this.BtnAyarlar.Name = "BtnAyarlar";
            this.BtnAyarlar.Size = new System.Drawing.Size(209, 57);
            this.BtnAyarlar.TabIndex = 14;
            this.BtnAyarlar.Text = "Ayarlar";
            this.BtnAyarlar.UseVisualStyleBackColor = false;
            // 
            // BtnBilanco
            // 
            this.BtnBilanco.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnBilanco.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnBilanco.FlatAppearance.BorderSize = 0;
            this.BtnBilanco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBilanco.ForeColor = System.Drawing.Color.White;
            this.BtnBilanco.Location = new System.Drawing.Point(0, 456);
            this.BtnBilanco.Name = "BtnBilanco";
            this.BtnBilanco.Size = new System.Drawing.Size(209, 57);
            this.BtnBilanco.TabIndex = 12;
            this.BtnBilanco.Text = "Bilanço";
            this.BtnBilanco.UseVisualStyleBackColor = false;
            // 
            // BtnMusteriler
            // 
            this.BtnMusteriler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnMusteriler.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnMusteriler.FlatAppearance.BorderSize = 0;
            this.BtnMusteriler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMusteriler.ForeColor = System.Drawing.Color.White;
            this.BtnMusteriler.Location = new System.Drawing.Point(0, 399);
            this.BtnMusteriler.Name = "BtnMusteriler";
            this.BtnMusteriler.Size = new System.Drawing.Size(209, 57);
            this.BtnMusteriler.TabIndex = 13;
            this.BtnMusteriler.Text = "Müşteriler";
            this.BtnMusteriler.UseVisualStyleBackColor = false;
            this.BtnMusteriler.Click += new System.EventHandler(this.BtnMusteriler_Click);
            // 
            // BtnFirmalar
            // 
            this.BtnFirmalar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnFirmalar.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnFirmalar.FlatAppearance.BorderSize = 0;
            this.BtnFirmalar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirmalar.ForeColor = System.Drawing.Color.White;
            this.BtnFirmalar.Location = new System.Drawing.Point(0, 342);
            this.BtnFirmalar.Name = "BtnFirmalar";
            this.BtnFirmalar.Size = new System.Drawing.Size(209, 57);
            this.BtnFirmalar.TabIndex = 11;
            this.BtnFirmalar.Text = "Firmalar";
            this.BtnFirmalar.UseVisualStyleBackColor = false;
            this.BtnFirmalar.Click += new System.EventHandler(this.BtnFirmalar_Click);
            // 
            // EkleContainer
            // 
            this.EkleContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(82)))), ((int)(((byte)(84)))));
            this.EkleContainer.Controls.Add(this.BtnEkle);
            this.EkleContainer.Controls.Add(this.BtnEkleCerceve);
            this.EkleContainer.Controls.Add(this.BtnEkleUrun);
            this.EkleContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.EkleContainer.Location = new System.Drawing.Point(0, 285);
            this.EkleContainer.Name = "EkleContainer";
            this.EkleContainer.Size = new System.Drawing.Size(209, 57);
            this.EkleContainer.TabIndex = 3;
            // 
            // BtnEkle
            // 
            this.BtnEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnEkle.FlatAppearance.BorderSize = 0;
            this.BtnEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEkle.ForeColor = System.Drawing.Color.White;
            this.BtnEkle.Location = new System.Drawing.Point(0, 0);
            this.BtnEkle.Margin = new System.Windows.Forms.Padding(0);
            this.BtnEkle.Name = "BtnEkle";
            this.BtnEkle.Size = new System.Drawing.Size(209, 57);
            this.BtnEkle.TabIndex = 1;
            this.BtnEkle.Text = "Ekle";
            this.BtnEkle.UseVisualStyleBackColor = false;
            this.BtnEkle.Click += new System.EventHandler(this.BtnEkle_Click);
            // 
            // BtnEkleCerceve
            // 
            this.BtnEkleCerceve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(82)))), ((int)(((byte)(84)))));
            this.BtnEkleCerceve.FlatAppearance.BorderSize = 0;
            this.BtnEkleCerceve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEkleCerceve.ForeColor = System.Drawing.Color.White;
            this.BtnEkleCerceve.Location = new System.Drawing.Point(0, 57);
            this.BtnEkleCerceve.Margin = new System.Windows.Forms.Padding(0);
            this.BtnEkleCerceve.Name = "BtnEkleCerceve";
            this.BtnEkleCerceve.Size = new System.Drawing.Size(209, 57);
            this.BtnEkleCerceve.TabIndex = 2;
            this.BtnEkleCerceve.Text = "Çerçeve";
            this.BtnEkleCerceve.UseVisualStyleBackColor = false;
            this.BtnEkleCerceve.Click += new System.EventHandler(this.BtnEkleCerceve_Click);
            // 
            // BtnEkleUrun
            // 
            this.BtnEkleUrun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(82)))), ((int)(((byte)(84)))));
            this.BtnEkleUrun.FlatAppearance.BorderSize = 0;
            this.BtnEkleUrun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEkleUrun.ForeColor = System.Drawing.Color.White;
            this.BtnEkleUrun.Location = new System.Drawing.Point(0, 114);
            this.BtnEkleUrun.Margin = new System.Windows.Forms.Padding(0);
            this.BtnEkleUrun.Name = "BtnEkleUrun";
            this.BtnEkleUrun.Size = new System.Drawing.Size(209, 67);
            this.BtnEkleUrun.TabIndex = 3;
            this.BtnEkleUrun.Text = "Ürün";
            this.BtnEkleUrun.UseVisualStyleBackColor = false;
            // 
            // BtnSepet
            // 
            this.BtnSepet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnSepet.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnSepet.FlatAppearance.BorderSize = 0;
            this.BtnSepet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSepet.ForeColor = System.Drawing.Color.White;
            this.BtnSepet.Location = new System.Drawing.Point(0, 228);
            this.BtnSepet.Name = "BtnSepet";
            this.BtnSepet.Size = new System.Drawing.Size(209, 57);
            this.BtnSepet.TabIndex = 4;
            this.BtnSepet.Text = "Sepet";
            this.BtnSepet.UseVisualStyleBackColor = false;
            this.BtnSepet.Click += new System.EventHandler(this.BtnSepet_Click);
            // 
            // BtnUrunler
            // 
            this.BtnUrunler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnUrunler.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnUrunler.FlatAppearance.BorderSize = 0;
            this.BtnUrunler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUrunler.ForeColor = System.Drawing.Color.White;
            this.BtnUrunler.Location = new System.Drawing.Point(0, 171);
            this.BtnUrunler.Name = "BtnUrunler";
            this.BtnUrunler.Size = new System.Drawing.Size(209, 57);
            this.BtnUrunler.TabIndex = 10;
            this.BtnUrunler.Text = "Ürünler";
            this.BtnUrunler.UseVisualStyleBackColor = false;
            // 
            // BtnCerceveler
            // 
            this.BtnCerceveler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnCerceveler.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCerceveler.FlatAppearance.BorderSize = 0;
            this.BtnCerceveler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCerceveler.ForeColor = System.Drawing.Color.White;
            this.BtnCerceveler.Location = new System.Drawing.Point(0, 114);
            this.BtnCerceveler.Name = "BtnCerceveler";
            this.BtnCerceveler.Size = new System.Drawing.Size(209, 57);
            this.BtnCerceveler.TabIndex = 7;
            this.BtnCerceveler.Text = "Çerçeveler";
            this.BtnCerceveler.UseVisualStyleBackColor = false;
            this.BtnCerceveler.Click += new System.EventHandler(this.BtnCerceveler_Click);
            // 
            // BtnSatis
            // 
            this.BtnSatis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnSatis.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnSatis.FlatAppearance.BorderSize = 0;
            this.BtnSatis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSatis.ForeColor = System.Drawing.Color.White;
            this.BtnSatis.Location = new System.Drawing.Point(0, 57);
            this.BtnSatis.Name = "BtnSatis";
            this.BtnSatis.Size = new System.Drawing.Size(209, 57);
            this.BtnSatis.TabIndex = 6;
            this.BtnSatis.Text = "Satış";
            this.BtnSatis.UseVisualStyleBackColor = false;
            this.BtnSatis.Click += new System.EventHandler(this.BtnSatis_Click);
            // 
            // BtnAnaMenu
            // 
            this.BtnAnaMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnAnaMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnAnaMenu.FlatAppearance.BorderSize = 0;
            this.BtnAnaMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAnaMenu.ForeColor = System.Drawing.Color.White;
            this.BtnAnaMenu.Location = new System.Drawing.Point(0, 0);
            this.BtnAnaMenu.Name = "BtnAnaMenu";
            this.BtnAnaMenu.Size = new System.Drawing.Size(209, 57);
            this.BtnAnaMenu.TabIndex = 5;
            this.BtnAnaMenu.Text = "Ana Menü";
            this.BtnAnaMenu.UseVisualStyleBackColor = false;
            this.BtnAnaMenu.Click += new System.EventHandler(this.BtnAnaMenu_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AnaMenuArkaPanel
            // 
            this.AnaMenuArkaPanel.BackColor = System.Drawing.Color.Silver;
            this.AnaMenuArkaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnaMenuArkaPanel.Location = new System.Drawing.Point(209, 34);
            this.AnaMenuArkaPanel.Name = "AnaMenuArkaPanel";
            this.AnaMenuArkaPanel.Size = new System.Drawing.Size(1021, 689);
            this.AnaMenuArkaPanel.TabIndex = 2;
            // 
            // jDragControl1
            // 
            this.jDragControl1.GetForm = this;
            this.jDragControl1.TargetControl = this.TopPanel;
            // 
            // FrmAnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1230, 723);
            this.Controls.Add(this.AnaMenuArkaPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FrmAnaMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ahşap Sanat Evi";
            this.Load += new System.EventHandler(this.FrmAnaMenu_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.EkleContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.virtualServerModeSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label CompanyText;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button FullScreenButton;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Button BtnCerceveler;
        private System.Windows.Forms.Button BtnSatis;
        private System.Windows.Forms.Button BtnAnaMenu;
        private System.Windows.Forms.Button BtnSepet;
        private System.Windows.Forms.Button BtnUrunler;
        private System.Windows.Forms.FlowLayoutPanel EkleContainer;
        private System.Windows.Forms.Button BtnEkle;
        private System.Windows.Forms.Button BtnEkleCerceve;
        private System.Windows.Forms.Button BtnEkleUrun;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button BtnFirmalar;
        private System.Windows.Forms.Button BtnAyarlar;
        private System.Windows.Forms.Button BtnBilanco;
        private System.Windows.Forms.Button BtnMusteriler;
        public System.Windows.Forms.Panel AnaMenuArkaPanel;
        private System.Windows.Forms.Button MinimizeButton;
        private DevExpress.Data.VirtualServerModeSource virtualServerModeSource1;
        private JDragControl.JDragControl jDragControl1;
    }
}

