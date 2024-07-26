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
        private DateTime lastCheckedTimeCerceve = DateTime.MinValue;
        public DateTime lastCheckedTimeFirma = DateTime.MinValue;



        public FrmAnaMenu()
        {
            InitializeComponent();
            originalBounds = this.Bounds;
        }

        private DateTime GetLastDatabaseChangeTime()
        {
            DateTime lastChangeTimeCerceve = DateTime.MinValue;

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = "SELECT MAX(SONGUNCELLEME) FROM TBLCERCEVELER";
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    object result = komut.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        lastChangeTimeCerceve = Convert.ToDateTime(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı değişiklik kontrolü sırasında bir hata oluştu: " + ex.Message);
            }

            return lastChangeTimeCerceve;
        }
        private DateTime GetLastFirmalarDatabaseChangeTime()
        {
            DateTime lastChangeTimeFirma = DateTime.MinValue;

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = "SELECT MAX(SONGUNCELLEME) FROM TBLFIRMALAR";
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    object result = komut.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        lastChangeTimeFirma = Convert.ToDateTime(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firmalar veritabanı değişiklik kontrolü sırasında bir hata oluştu: " + ex.Message);
            }

            return lastChangeTimeFirma;
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
            DateTime currentChangeTime = GetLastFirmalarDatabaseChangeTime();

            // Firmalar formunun açık olup olmadığını kontrol et
            FrmFirmalar firmaForm = Application.OpenForms.OfType<FrmFirmalar>().FirstOrDefault();

            // Eğer veritabanında değişiklik olmuşsa veya form açık değilse, yeni bir form oluştur
            if (currentChangeTime > lastCheckedTimeFirma || firmaForm == null)
            {
                if (firmaForm != null)
                {
                    firmaForm.Close(); // Mevcut formu kapat
                }

                firmaForm = new FrmFirmalar();
                firmaForm.TopLevel = false;
                firmaForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(firmaForm);

                // Son kontrol zamanını güncelle
                lastCheckedTimeFirma = currentChangeTime;
            }

            // Formu ön plana getir
            firmaForm.Show();
            firmaForm.BringToFront();
        }
        private void BtnMusteriler_Click(object sender, EventArgs e)
        {

            // Musteriler formunun açık olup olmadığını kontrol et
            FrmMusteriler musteriform = Application.OpenForms.OfType<FrmMusteriler>().FirstOrDefault();

            if (musteriform != null)
            {
                musteriform.Close(); // Mevcut formu kapat
            }

            musteriform = new FrmMusteriler();
            musteriform.TopLevel = false;
            musteriform.Dock = DockStyle.Fill;
            this.AnaMenuArkaPanel.Controls.Add(musteriform);

            // Formu ön plana getir
            musteriform.Show();
            musteriform.BringToFront();
        }


        private void BtnCerceveler_Click(object sender, EventArgs e)
        {
            

            DateTime currentChangeTime = GetLastDatabaseChangeTime();
            DateTime currentChangeTimeFirma = GetLastFirmalarDatabaseChangeTime();

            DateTime lastChangeTime = new[] { currentChangeTime, currentChangeTimeFirma }.Max();

            formCerceveler cerceveForm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();
            FrmEkle ekleForm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();


            if (lastChangeTime > lastCheckedTimeCerceve)
            {
                // Eğer veritabanında değişiklik olmuşsa, formu kapat ve yeniden aç
                if (cerceveForm != null && ekleForm != null)
                {
                    cerceveForm.Close();
                    ekleForm.Close();
                }
                cerceveForm = new formCerceveler();
                cerceveForm.TopLevel = false;
                cerceveForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(cerceveForm);
                cerceveForm.Show();
                cerceveForm.BringToFront();
                ekleForm = new FrmEkle();
                ekleForm.TopLevel = false;
                ekleForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(ekleForm);
                ekleForm.Show();


                // Son kontrol zamanını güncelle
                lastCheckedTimeCerceve = lastChangeTime;
            }
            else
            {
                // Eğer değişiklik olmamışsa, formu sadece öne getir
                if (cerceveForm != null)
                {
                    cerceveForm.BringToFront();
                }
                else
                {
                    cerceveForm = new formCerceveler();
                    cerceveForm.TopLevel = false;
                    cerceveForm.Dock = DockStyle.Fill;
                    this.AnaMenuArkaPanel.Controls.Add(cerceveForm);
                    cerceveForm.Show();
                    cerceveForm.BringToFront();
                }
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

        public void BtnEkleCerceve_Click(object sender, EventArgs e)
        {
            // Sadece TBLFIRMALAR tablosundaki en son değişiklik zamanını al
            DateTime currentChangeTimeFirmalar = GetLastFirmalarDatabaseChangeTime();

            // Debug için konsola yazdır
            Console.WriteLine($"currentChangeTimeFirmalar: {currentChangeTimeFirmalar}");
            Console.WriteLine($"lastCheckedTimeFirma: {lastCheckedTimeFirma}");

            // Açık olan formu kontrol et
            FrmEkle ekleForm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();

            // Eğer TBLFIRMALAR tablosunda değişiklik olmuşsa veya form açık değilse, mevcut formu kapat ve yeni bir form oluştur
            if (currentChangeTimeFirmalar > lastCheckedTimeFirma)
            {
                // Eğer mevcut form açıksa kapat
                if (ekleForm != null)
                {
                    ekleForm.Close();
                }

                // Yeni form oluştur ve ekle
                ekleForm = new FrmEkle();
                ekleForm.TopLevel = false;
                ekleForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(ekleForm);

                // Son kontrol zamanını güncelle
                lastCheckedTimeFirma = currentChangeTimeFirmalar;

                // Debug için konsola yazdır
                Console.WriteLine($"Güncellenen lastCheckedTimeFirma: {lastCheckedTimeFirma}");
            }

            // Eğer ekleForm null ise yeniden oluştur
            if (ekleForm == null)
            {
                ekleForm = new FrmEkle();
                ekleForm.TopLevel = false;
                ekleForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(ekleForm);
            }

            // Formu öne getir
            ekleForm.groupBoxFirmalae.Enabled = true;
            ekleForm.groupBoxKodlar.Enabled = true;
            ekleForm.BtnFirmaGuncelle.Enabled = false;
            ekleForm.BtnFirmaEkle.Enabled = true;

            ekleForm.Show();
            ekleForm.BringToFront();
        }

    }
}