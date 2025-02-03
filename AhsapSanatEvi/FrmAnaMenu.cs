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
using AhsapSanatEvi.Models;


namespace AhsapSanatEvi
{
    public partial class FrmAnaMenu : Form
    {
        bool ekleExpand = false;
        private bool isFullScreen = false;
        private Rectangle originalBounds;
        public static DateTime lastCheckedTimeCerceve = DateTime.MinValue;
        public DateTime lastCheckedTimeFirma = DateTime.MinValue;
        public static DateTime lastCheckedTimeMusteri = DateTime.MinValue;
        private List<SepetItem> sepetListesi = new List<SepetItem>();
        private FrmSepet sepetForm;




        public FrmAnaMenu()
        {
            InitializeComponent();
            originalBounds = this.Bounds;
        }
        private void FrmAnaMenu_Load(object sender, EventArgs e)
        {
            BtnAnaMenu_Click(sender, e); // 📌 Ana menüyü aç
        }



        private DateTime GetLastCercevelerDatabaseChangeTime()
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
        public static DateTime GetLastMusterilerDatabaseChangeTime()
        {
            DateTime lastChangeTimeMusteriler = DateTime.MinValue;

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = "SELECT MAX(SONGUNCELLEME) FROM TBLMUSTERILER";
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    object result = komut.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        lastChangeTimeMusteriler = Convert.ToDateTime(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteriler veritabanı değişiklik kontrolü sırasında bir hata oluştu: " + ex.Message);
            }

            return lastChangeTimeMusteriler;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            // 📌 Açık olan sepet formunu kontrol et
            FrmSepet sepetForm = Application.OpenForms.OfType<FrmSepet>().FirstOrDefault();

            if (sepetForm != null)
            {
                List<SepetItem> sepetListesi = sepetForm.GetSepetListesi();

                if (sepetListesi.Count > 0) // 📌 Eğer sepette ürün varsa uyarı ver
                {
                    DialogResult result = MessageBox.Show(
                        "SEPETTE ÜRÜN VAR, YİNE DE KAPATMAK İSTİYOR MUSUNUZ?",
                        "Çıkış Onayı",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result != DialogResult.Yes) // 📌 Kullanıcı "Hayır" derse işlemi iptal et
                    {
                        return;
                    }
                }
            }

            Application.Exit(); // 📌 Eğer sepette ürün yoksa veya kullanıcı "Evet" derse uygulamayı kapat
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

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            FrmAnaSayfa anaForm = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();

            if (anaForm == null) // Eğer FrmAnaSayfa açık değilse yeni bir form oluştur
            {
                anaForm = new FrmAnaSayfa();
                anaForm.TopLevel = false;
                anaForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(anaForm);
                anaForm.Show();
            }
            else
            {
                // Eğer form zaten açıksa, sadece öne getir
                anaForm.BringToFront();
            }

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
            CheckAndUpdateMusteriler(); // **Sadece müşteri verilerini kontrol et**

            FrmMusteriler musteriform = Application.OpenForms.OfType<FrmMusteriler>().FirstOrDefault();
            if (musteriform == null)
            {
                musteriform = new FrmMusteriler();
                musteriform.TopLevel = false;
                musteriform.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(musteriform);
                musteriform.Show();
            }

            musteriform.BringToFront();
        }




        private void CheckAndUpdateMusteriler()
        {
            DateTime currentChangeTimeMusteri = GetLastMusterilerDatabaseChangeTime();

            // Eğer SONGUNCELLEME değişmemişse, yenileme yapma
            if (currentChangeTimeMusteri <= lastCheckedTimeMusteri)
            {
                return;
            }

            // Eğer müşteri güncellenmişse satış formundaki listeyi yenile
            FrmCerceveSatis satisForm = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            

            // 📌 Güncelleme zamanını kaydet
            lastCheckedTimeMusteri = currentChangeTimeMusteri;
        }


        private void CheckAndUpdateCerceveler()
        {
            // 📌 Veritabanındaki son güncelleme zamanlarını al
            DateTime currentChangeTimeCerceve = GetLastCercevelerDatabaseChangeTime();
            DateTime currentChangeTimeFirma = GetLastFirmalarDatabaseChangeTime();

            // 📌 En son değişeni al
            DateTime lastChangeTime = new[] { currentChangeTimeCerceve, currentChangeTimeFirma }.Max();

            // 📌 Eğer veritabanında güncelleme olmuşsa tüm ilgili formları yenile
            if (lastChangeTime > lastCheckedTimeCerceve)
            {
                Console.WriteLine("[CheckAndUpdateForms] Veriler değişti! Formları güncelliyorum...");

                // 📌 Çerçeve Formu Yenileme
                formCerceveler cerceveForm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();
                if (cerceveForm != null)
                {
                    cerceveForm.Close();
                    cerceveForm = new formCerceveler();
                    cerceveForm.TopLevel = false;
                    cerceveForm.Dock = DockStyle.Fill;
                    this.AnaMenuArkaPanel.Controls.Add(cerceveForm);
                    cerceveForm.Show();
                    cerceveForm.BringToFront();
                }

                // 📌 Satış Formu Yenileme
                FrmCerceveSatis satisForm = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
                if (satisForm != null)
                {
                    satisForm.CerceveGetir(); // 📌 Çerçeve listesini güncelle
                }

                // 📌 Global değişkeni güncelle
                lastCheckedTimeCerceve = lastChangeTime;
            }
        }




        private void BtnCerceveler_Click(object sender, EventArgs e)
        {
            CheckAndUpdateCerceveler(); // 📌 Merkezi güncelleme metodunu çağır

            formCerceveler cerceveForm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();
            if (cerceveForm == null)
            {
                cerceveForm = new formCerceveler();
                cerceveForm.TopLevel = false;
                cerceveForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(cerceveForm);
                cerceveForm.Show();
            }
            cerceveForm.BringToFront();
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

        private void BtnSatis_Click(object sender, EventArgs e)
        {
            CheckAndUpdateCerceveler();
            CheckAndUpdateMusteriler();

            FrmCerceveSatis satisForm = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            if (satisForm == null)
            {
                satisForm = new FrmCerceveSatis();
                satisForm.TopLevel = false;
                satisForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(satisForm);
                satisForm.Show();
            }
            satisForm.BringToFront();
        }



        private void BtnSepet_Click(object sender, EventArgs e)
        {
            FrmCerceveSatis satisForm = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();

            if (satisForm == null)
            {
                satisForm = new FrmCerceveSatis();
                satisForm.TopLevel = false;
                satisForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(satisForm);
                satisForm.Show();
            }

            // 📌 **Mevcut sepet listesini al**
            List<SepetItem> mevcutSepetListesi = satisForm.GetSepetListesi();

            FrmSepet sepetForm = Application.OpenForms.OfType<FrmSepet>().FirstOrDefault();
            if (sepetForm == null)
            {
                // 📌 **Mevcut sepet listesini göndererek FrmSepet oluştur**
                sepetForm = new FrmSepet(mevcutSepetListesi);
                sepetForm.TopLevel = false;
                sepetForm.Dock = DockStyle.Fill;
                this.AnaMenuArkaPanel.Controls.Add(sepetForm);
                sepetForm.Show();
            }
            else
            {
                // 📌 **Eğer açık olan FrmSepet varsa, sadece sepet listesini güncelle**
                sepetForm.GuncelleSepet(mevcutSepetListesi);
            }

            sepetForm.BringToFront();

            // 📌 **Müşteri listesini güncelle (Eğer yeni müşteri eklenmişse)**
            DateTime currentChangeTimeMusteri = GetLastMusterilerDatabaseChangeTime();
            if (currentChangeTimeMusteri > lastCheckedTimeMusteri)
            {
                sepetForm.MusteriGetir();
                lastCheckedTimeMusteri = currentChangeTimeMusteri;
            }
        }


    }
}