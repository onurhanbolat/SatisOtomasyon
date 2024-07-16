using System;
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
            FrmCerceveler cerceveForm = Application.OpenForms.OfType<FrmCerceveler>().FirstOrDefault();
            if (cerceveForm == null)
            {
                cerceveForm = new FrmCerceveler();
                cerceveForm.TopLevel = false;
                cerceveForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(cerceveForm);
                cerceveForm.Show();
                cerceveForm.BringToFront(); // Formu ön plana getirir
            }
            else
            {
                cerceveForm.BringToFront(); // Formu ön plana getirir
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ekleExpand)
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
            FrmEkle ekleForm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            if (ekleForm != null)
            {
                ekleForm.Close(); // Formu kapat
            }

            ekleForm = new FrmEkle();
            ekleForm.TopLevel = false;
            ekleForm.Dock = DockStyle.Fill;
            this.AnaMenuArkaPanel.Controls.Add(ekleForm);
            ekleForm.Show();
            ekleForm.BringToFront(); // Formu ön plana getir
        }

        private void BtnEkleUrun_Click(object sender, EventArgs e)
        {
            // Ürün ekleme işlemleri buraya eklenecek
        }
    }
}