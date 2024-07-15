﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AhsapSanatEvi
{
    public partial class FrmAnaMenu : Form
    {
        bool ekleExpand = false;
        private bool isFullScreen = false;
        private Rectangle originalBounds;
        public FrmAnaMenu()
        {
            InitializeComponent();
            originalBounds = this.Bounds;
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True");

        //Babamın Db Kodu: Data Source=BOLAT\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True 
        //Benim Db Kodum: Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True


        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FullScreenButton_Click(object sender, EventArgs e)
        {
            if (isFullScreen)
            {
                // Tam ekrandan normal boyuta geçiş
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                isFullScreen = false;
            }
            else
            {
                // Normal boyuttan tam ekrana geçiş
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                isFullScreen = true;
            }
        }
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            timer1.Start();
          
        }
        private void BtnFirmalar_Click(object sender, EventArgs e)
        {
            FrmFirmalar firmaForm = Application.OpenForms.OfType<FrmFirmalar>().FirstOrDefault();

            if (firmaForm != null)
            {
                firmaForm.Close(); // Formu kapat
            }

            firmaForm = new FrmFirmalar();
            firmaForm.TopLevel = false;
            firmaForm.Dock = DockStyle.Fill;
            this.AnaMenuArkaPanel.Controls.Add(firmaForm);
            firmaForm.Show();
            firmaForm.BringToFront(); // Formu ön plana getir
        }

        private void BtnCerceveler_Click(object sender, EventArgs e)
        {
            FrmCerceveler firmaForm = Application.OpenForms.OfType<FrmCerceveler>().FirstOrDefault();
            if (firmaForm == null)
            {
                firmaForm = new FrmCerceveler();
                firmaForm.TopLevel = false;
                firmaForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(firmaForm);
                firmaForm.Show();
                firmaForm.BringToFront(); // Formu ön plana getirir
            }
            else
            {
                firmaForm.BringToFront(); // Formu ön plana getirir
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ekleExpand == false)
            {
                EkleContainer.Height += 10;
                if (EkleContainer.Height >= 168)
                {
                    timer1.Stop();
                    ekleExpand = true;
                }
            }
            else
            {
                EkleContainer.Height -= 10;
                if (EkleContainer.Height <= 57)
                {
                    timer1.Stop();
                    ekleExpand = false;
                }
            }
        }

        private void BtnEkleCerceve_Click(object sender, EventArgs e)
        {
            FrmEkle firmaForm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            if (firmaForm != null)
            {
                firmaForm.Close(); // Formu kapat
            }

            firmaForm = new FrmEkle();
            firmaForm.TopLevel = false;
            firmaForm.Dock = DockStyle.Fill;
            this.AnaMenuArkaPanel.Controls.Add(firmaForm);
            firmaForm.Show();
            firmaForm.BringToFront(); // Formu ön plana getir
        }
        private void BtnEkleUrun_Click(object sender, EventArgs e)
        {

        }

       
    }
}



