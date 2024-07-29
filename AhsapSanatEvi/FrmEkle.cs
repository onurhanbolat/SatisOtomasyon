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
using System.IO;

namespace AhsapSanatEvi
{
    public partial class FrmEkle : Form
    {
        private bool isTextBoxEmpty;
        private DateTime lastCheckedTime = DateTime.MinValue;

        public FrmEkle()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form_Resize);

            TxtBxBirimSatisFiyat.Text = "0".ToString();
            // Placeholder metni ekleyin
            TextBoxPlaceholder(TxtBxFirmaAra, "Firma Ara..");
            TextBoxPlaceholder(TxtBxKodAra, "Kod Ara..");
            TextBoxPlaceholder(TxtBxKodEkle, "Kod Ekle..");

            // Firma ve Kodları Getir
            FirmaGetir();
            FirmayaGöreKodGetir();
            KodGetir();

            // TxtBxFirmaAra ve TxtBxKodAra olaylarını bağlayın
            TxtBxKodAra.TextChanged += new EventHandler(TxtBxKodAra_TextChanged);

            TxtBxKarMarji.TextChanged += new EventHandler(TxtBxKarMarjiOrBirimFiyat_TextChanged);
            TxtBxBirimFiyat.TextChanged += new EventHandler(TxtBxKarMarjiOrBirimFiyat_TextChanged);

            TxtBxKalinlik.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            TxtBxBirimFiyat.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            TxtBxKarMarji.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            TxtBxAciklama.KeyDown += new KeyEventHandler(TextBox_KeyDown);

        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşu ile yeni satır eklemeyi engelle
                MoveToNextControl((TextBox)sender);
            }
        }
        private void MoveToNextControl(TextBox currentTextBox)
        {
            var controls = new List<TextBox> { TxtBxKalinlik, TxtBxBirimFiyat, TxtBxKarMarji, TxtBxAciklama };
            int index = controls.IndexOf(currentTextBox);

            // Eğer index geçerli bir TextBox ise ve bir sonraki TextBox varsa
            if (index >= 0 && index < controls.Count - 1)
            {
                controls[index + 1].Focus();
            }
            // Eğer son TextBox'taysak, herhangi bir işlem yapma
            else if (index == controls.Count - 1)
            {
                // Son TextBox'ta Enter tuşuna basıldığında herhangi bir şey yapmıyoruz.
                // Buraya, son TextBox'ta Enter tuşuna basıldığında yapılacak başka bir işlem ekleyebilirsiniz.
            }
        }



        private void Form_Resize(object sender, EventArgs e)
        {
            CenterGroupBox();
        }

        private void TxtBxKarMarjiOrBirimFiyat_TextChanged(object sender, EventArgs e)
        {
            UpdateBirimSatisFiyat();
        }

        private void UpdateBirimSatisFiyat()
        {
            if (int.TryParse(TxtBxKarMarji.Text, out int karMarji) && int.TryParse(TxtBxBirimFiyat.Text, out int birimFiyat))
            {
                int birimSatisFiyat = karMarji * birimFiyat;
                TxtBxBirimSatisFiyat.Text = birimSatisFiyat.ToString();
            }
            else
            {
                TxtBxBirimSatisFiyat.Text = "0";
            }
        }

        public void FirmaAra()
        {
            string sorgu = "SELECT * FROM TBLFIRMALAR WHERE FIRMAAD LIKE @firmaad ORDER BY FIRMAAD ASC";
            EkleFirmaListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@firmaad", "%" + TxtBxFirmaAra.Text + "%");
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        EkleFirmaListesi arac = new EkleFirmaListesi
                        {
                            EkleFirmaListeLabel = { Text = oku["FIRMAAD"].ToString() }
                        };
                        EkleFirmaListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        public void FirmaGetir()
        {
            string sorgu = "SELECT * FROM TBLFIRMALAR ORDER BY FIRMAAD ASC";
            EkleFirmaListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        EkleFirmaListesi arac = new EkleFirmaListesi
                        {
                            EkleFirmaListeLabel = { Text = oku["FIRMAAD"].ToString() }
                        };
                        EkleFirmaListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }
        public void GenelKodAra()
        {
            string sorgu = "SELECT * FROM TBLCERCEVEKODLARI WHERE CERCEVEKOD LIKE @cercevekod ORDER BY CERCEVEKOD ASC";
            EkleKodListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@cercevekod", "%" + TxtBxKodAra.Text + "%");
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        EkleKodListesi arac = new EkleKodListesi
                        {
                            EkleKodListeLabel = { Text = oku["CERCEVEKOD"].ToString() }
                        };
                        EkleKodListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }
        public void KodAra()
        {
            string sorgu = "SELECT * FROM TBLCERCEVEKODLARI WHERE CERCEVEKOD LIKE @cercevekod AND FIRMAADID = @FirmaID ORDER BY CERCEVEKOD ASC";
            EkleKodListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@cercevekod", "%" + TxtBxKodAra.Text + "%");
                    komut.Parameters.AddWithValue("@FirmaID", EkleFirmaListesi.selectedFirmaID);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        EkleKodListesi arac = new EkleKodListesi
                        {
                            EkleKodListeLabel = { Text = oku["CERCEVEKOD"].ToString() }
                        };
                        EkleKodListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        public void KodGetir()
        {
            string sorgu = "SELECT * FROM TBLCERCEVEKODLARI ORDER BY CERCEVEKOD ASC";
            EkleKodListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        EkleKodListesi arac = new EkleKodListesi
                        {
                            EkleKodListeLabel = { Text = oku["CERCEVEKOD"].ToString() }
                        };
                        EkleKodListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }
        public void FirmayaGöreKodGetir()
        {
            if (EkleFirmaListesi.selectedFirmaID <= 0)
            {
                KodGetir();
            }

            string sorgu = "SELECT * FROM TBLCERCEVEKODLARI WHERE FIRMAADID = @FirmaID ORDER BY CERCEVEKOD ASC";
            EkleKodListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@FirmaID", EkleFirmaListesi.selectedFirmaID);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        EkleKodListesi arac = new EkleKodListesi
                        {
                            EkleKodListeLabel = { Text = oku["CERCEVEKOD"].ToString() }
                        };
                        EkleKodListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void CenterGroupBox()
        {
            if (GrpBoxGenel != null)
            {
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;
                int groupBoxWidth = GrpBoxGenel.Width;
                int groupBoxHeight = GrpBoxGenel.Height;
                int startX = (formWidth - groupBoxWidth) / 2;
                int startY = (formHeight - groupBoxHeight) / 2 - 27;
                GrpBoxGenel.Location = new Point(startX, startY);
            }
        }

        private void BtnYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "RESIM SEÇME EKRANI",
                Filter = "PNG | *.png | JPG-JPEG | *.jpg;*.jpeg | All Files | *.*",
                FilterIndex = 3
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PictureBoxImage.Image = new Bitmap(ofd.FileName);
                    MessageBox.Show(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void TextBoxPlaceholder(TextBox textBox, string placeholder)
        {
            bool isPlaceholderActive = true;

            // Placeholder başlat
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (isPlaceholderActive)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    isPlaceholderActive = false;
                }
            };

            textBox.Leave += (s, e) =>
            {

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    isPlaceholderActive = true;

                    // Placeholder aktif olduğunda yapılacak işlemler
                    if (textBox == TxtBxFirmaAra)
                    {
                        FirmaGetir();
                    }
                    else if (textBox == TxtBxKodAra)
                    {
                        if (EkleFirmaListesi.selectedFirmaID == 0)
                        {
                            KodGetir();
                        }
                        else
                        {
                            FirmayaGöreKodGetir();
                        }
                    }
                }
            };
        }

        private void BtnKodEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string kodAd = TxtBxKodEkle.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(kodAd) || TxtBxKodEkle.Text == "Kod Ekle..")
                {
                    MessageBox.Show("Alan Boş Bırakılamaz!");
                }
                else if (EkleFirmaListesi.selectedFirmaID == 0)
                {
                    MessageBox.Show("Lütfen önce bir firma seçin!");
                }
                else
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand kayit = new SqlCommand("INSERT INTO TBLCERCEVEKODLARI (CERCEVEKOD, FIRMAADID) VALUES (@p1, @p2)", connection);
                        kayit.Parameters.AddWithValue("@p1", kodAd);
                        kayit.Parameters.AddWithValue("@p2", EkleFirmaListesi.selectedFirmaID); // Seçilen firma ID'sini kullan

                        kayit.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ekleme İşlemi Başarılı");
                    TextBoxPlaceholder(TxtBxKodEkle, "Kod Ekle..");
                    // Yeni eklenen firmayı göstermek için listeyi güncelle
                    FirmayaGöreKodGetir();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void TxtBxFirmaAra_TextChanged(object sender, EventArgs e)
        {
            isTextBoxEmpty = string.IsNullOrEmpty(TxtBxFirmaAra.Text) || TxtBxFirmaAra.Text == "Firma Ara..";

            if (isTextBoxEmpty)
            {
                FirmaGetir();
            }
            else
            {
                FirmaAra();
            }
            EkleFirmaListesi.ResetSelectedFirmaID();
        }

        private void TxtBxKodAra_TextChanged(object sender, EventArgs e)
        {
            isTextBoxEmpty = string.IsNullOrEmpty(TxtBxKodAra.Text) || TxtBxKodAra.Text == "Kod Ara..";

            if (isTextBoxEmpty)
            {
                if (EkleFirmaListesi.selectedFirmaID == 0)
                {
                    KodGetir();
                }
                else
                {
                    FirmayaGöreKodGetir();
                }
            }
            else
            {
                if (EkleFirmaListesi.selectedFirmaID == 0)
                {
                    GenelKodAra();
                }
                else
                {
                    KodAra();
                }
            }
        }

        private void BtnFirmaEkle_Click(object sender, EventArgs e)
        {
            // TextBox'lardan veri al
            string aciklama = TxtBxAciklama.Text.Trim().ToUpper();
            string kalinlikStr = TxtBxKalinlik.Text.Trim();
            string birimFiyatStr = TxtBxBirimFiyat.Text.Trim();
            string karMarjiStr = TxtBxKarMarji.Text.Trim();
            string birimSatisFiyatStr = TxtBxBirimSatisFiyat.Text.Trim();

            // Sayısal değerleri kontrol et ve dönüştür
            int kalinlik, birimFiyat, karMarji, birimSatisFiyat;
            bool isKalinlikValid = int.TryParse(kalinlikStr, out kalinlik);
            bool isBirimFiyatValid = int.TryParse(birimFiyatStr, out birimFiyat);
            bool isKarMarjiValid = int.TryParse(karMarjiStr, out karMarji);
            bool isBirimSatisFiyatValid = int.TryParse(birimSatisFiyatStr, out birimSatisFiyat);

            // Değerlerin doğruluğunu kontrol et
            if (string.IsNullOrWhiteSpace(aciklama) ||
                !isKalinlikValid ||
                !isBirimFiyatValid ||
                !isKarMarjiValid ||
                !isBirimSatisFiyatValid ||
                EkleFirmaListesi.selectedFirmaID <= 0 ||
                EkleKodListesi.selectedKodID <= 0)
            {
                MessageBox.Show("Lütfen tüm alanları doğru bir şekilde doldurduğunuzdan emin olun ve bir firma ile kod seçtiğinizden emin olun.");
                return;
            }

            // Resmi base64 string'e dönüştür
            string resimBase64 = null;
            if (PictureBoxImage.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    PictureBoxImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] resimData = ms.ToArray();
                    resimBase64 = Convert.ToBase64String(resimData);
                }
            }

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();

                    // SQL sorgusu oluştur
                    string sorgu = "INSERT INTO TBLCERCEVELER (CERCEVEACIKLAMA, CERCEVEKALINLIK, CERCEVEBIRIMFIYAT, CERCEVEKARMARJI, BIRIMSATISFIYATI, FIRMAADID, CERCEVEKODID, URUNRESMI) " +
                                   "VALUES (@aciklama, @kalinlik, @birimFiyat, @karMarji, @birimSatisFiyat, @firmaID, @kodID, @urunResmi)";

                    // SQL komutu oluştur ve parametreleri ekle
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@aciklama", aciklama);
                    komut.Parameters.AddWithValue("@kalinlik", kalinlik);
                    komut.Parameters.AddWithValue("@birimFiyat", birimFiyat);
                    komut.Parameters.AddWithValue("@karMarji", karMarji);
                    komut.Parameters.AddWithValue("@birimSatisFiyat", birimSatisFiyat);
                    komut.Parameters.AddWithValue("@firmaID", EkleFirmaListesi.selectedFirmaID);
                    komut.Parameters.AddWithValue("@kodID", EkleKodListesi.selectedKodID);
                    komut.Parameters.AddWithValue("@urunResmi", (object)resimBase64 ?? DBNull.Value);

                    // Komutu çalıştır
                    komut.ExecuteNonQuery();
                }

                MessageBox.Show("Ekleme işlemi başarılı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void TxtBxKalinlik_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TxtBxBirimFiyat_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TxtBxKarMarji_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TxtBxAciklama_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BtnFirmaGuncelle_Click(object sender, EventArgs e)
        {
            if (CerceveListesi.selectedCerceveID > 0)
            {
                string sorgu = "UPDATE TBLCERCEVELER SET CERCEVEKALINLIK = @kalinlik, CERCEVEBIRIMFIYAT = @birimfiyat, CERCEVEKARMARJI = @karmarji, BIRIMSATISFIYATI = @birimsatisfiyat, CERCEVEACIKLAMA = @aciklama, URUNRESMI = @urunResmi WHERE CERCEVEID = @cerceveID";

                string resimBase64 = null;
                if (PictureBoxImage.Image != null)
                {
                    try
                    {
                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            PictureBoxImage.Image.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg); // veya ImageFormat.Png
                            byte[] resimData = ms2.ToArray();
                            resimBase64 = Convert.ToBase64String(resimData);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Resim kaydedilirken bir hata oluştu: {ex.Message}");
                    }
                }

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    try
                    {
                        connection.Open();
                        SqlCommand komut = new SqlCommand(sorgu, connection);
                        komut.Parameters.AddWithValue("@kalinlik", TxtBxKalinlik.Text);
                        komut.Parameters.AddWithValue("@birimfiyat", TxtBxBirimFiyat.Text);
                        komut.Parameters.AddWithValue("@karmarji", TxtBxKarMarji.Text);
                        komut.Parameters.AddWithValue("@birimsatisfiyat", TxtBxBirimSatisFiyat.Text);
                        komut.Parameters.AddWithValue("@aciklama", TxtBxAciklama.Text);
                        komut.Parameters.AddWithValue("@urunResmi", (object)resimBase64 ?? DBNull.Value);
                        komut.Parameters.AddWithValue("@cerceveID", CerceveListesi.selectedCerceveID);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Çerçeve başarıyla güncellendi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Kod güncellenirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Güncellenecek bir kod seçiniz.");
            }
        }

    }
}