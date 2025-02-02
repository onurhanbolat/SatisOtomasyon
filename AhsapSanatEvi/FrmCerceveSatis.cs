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
using AhsapSanatEvi.Models;


namespace AhsapSanatEvi
{
    public partial class FrmCerceveSatis : Form
    {
        bool isRadioButtonCamChecked = false;
        bool isRadioButtonPaspartuChecked = false;
        bool isRadioButtonKutuChecked = false;
        private decimal hafizadakiToplamFiyat = 0;
        private List<SepetItem> sepetListesi = new List<SepetItem>();

        public FrmCerceveSatis()
        {
            InitializeComponent();
        }
        public List<SepetItem> GetSepetListesi()
        {
            return sepetListesi;
        }


        private void FrmCerceveSatis_Load(object sender, EventArgs e)
        {
            try
            {
                TxtBxKodAra.KeyDown += TxtBxKodAra_KeyDown;
                TxtBxFirmaAra.KeyDown += TxtBxFirmaAra_KeyDown;
                TxtBxAnahtarKelimeAra.KeyDown += TxtBxAnahtarKelimeAra_KeyDown;
                TxtBxMusteriAra.KeyDown += TxtBxMusteriAra_KeyDown;

                MusteriGetir();
                CerceveGetir(); // 📌 Çerçeve listesini ilk yükleme
                TxtBxCamFiyat.Text = "0";
                TxtBxYukseklik.Text = "0";
                TxtBxGenislik.Text = "0";
                TxtBxPaspartuFiyat.Text = "0";
                TxtBxKutuFiyat.Text = "0";
                TxtBxCerceveFiyat.Text = "0";
                TxtBxToplamFiyat.Text = "0";
                TxtBxAdet.Text = "1";

                TxtBxToplamFiyat.KeyDown -= TxtBxToplamFiyat_KeyDown;
                TxtBxToplamFiyat.KeyDown += TxtBxToplamFiyat_KeyDown;

                // 📌 RadioButton'ların eventlerini bağla
                radioButtonCam.CheckedChanged += RadioButtonCam_CheckedChanged;
                radioButtonPaspartu.CheckedChanged += RadioButtonPaspartu_CheckedChanged;
                radioButtonKutu.CheckedChanged += RadioButtonKutu_CheckedChanged;
                radioButtonNakit.CheckedChanged += RadioButtonNakitKart_CheckedChanged;
                radioButtonKart.CheckedChanged += RadioButtonNakitKart_CheckedChanged;

                radioButtonCam.AutoCheck = false;
                radioButtonPaspartu.AutoCheck = false;
                radioButtonKutu.AutoCheck = false;

                // 📌 Kart RadioButton'u varsayılan olarak seçili olsun
                radioButtonKart.Checked = true;
                radioButtonCam.Checked = true;
                radioButtonPaspartu.Checked = false;
                radioButtonKutu.Checked = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }


        private void TxtBxYukseklik_Enter(object sender, EventArgs e)
        {
            if (TxtBxYukseklik.Text == "0")
            {
                TxtBxYukseklik.Text = "";
            }
        }

        private void TxtBxYukseklik_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBxYukseklik.Text))
            {
                TxtBxYukseklik.Text = "0";
            }
        }

        private void TxtBxGenislik_Enter(object sender, EventArgs e)
        {
            if (TxtBxGenislik.Text == "0")
            {
                TxtBxGenislik.Text = "";
            }
        }

        private void TxtBxGenislik_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBxGenislik.Text))
            {
                TxtBxGenislik.Text = "0";
            }
        }

        private void TxtBxKodAra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CerceveGetir(TxtBxFirmaAra.Text.Trim(), TxtBxKodAra.Text.Trim(), TxtBxAnahtarKelimeAra.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtBxAnahtarKelimeAra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CerceveGetir(TxtBxFirmaAra.Text.Trim(), TxtBxKodAra.Text.Trim(), TxtBxAnahtarKelimeAra.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtBxFirmaAra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CerceveGetir(TxtBxFirmaAra.Text.Trim(), TxtBxKodAra.Text.Trim(), TxtBxAnahtarKelimeAra.Text.Trim());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtBxMusteriAra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MusteriGetir(TxtBxMusteriAra.Text.Trim(), "");  // Müşteri ismini parametre olarak geçiriyoruz
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void radioButtonCam_Click(object sender, EventArgs e)
        {
            if (radioButtonCam.Checked && isRadioButtonCamChecked)
            {
                radioButtonCam.Checked = false;
                isRadioButtonCamChecked = false;
            }
            else
            {
                radioButtonCam.Checked = true;
                isRadioButtonCamChecked = true;
            }
        }

        private void radioButtonPaspartu_Click(object sender, EventArgs e)
        {
            if (radioButtonPaspartu.Checked && isRadioButtonPaspartuChecked)
            {
                radioButtonPaspartu.Checked = false;
                isRadioButtonPaspartuChecked = false;
            }
            else
            {
                radioButtonPaspartu.Checked = true;
                isRadioButtonPaspartuChecked = true;
            }
        }

        private void radioButtonKutu_Click(object sender, EventArgs e)
        {
            if (radioButtonKutu.Checked && isRadioButtonKutuChecked)
            {
                radioButtonKutu.Checked = false;
                isRadioButtonKutuChecked = false;
            }
            else
            {
                radioButtonKutu.Checked = true;
                isRadioButtonKutuChecked = true;
            }
        }

        // Yükseklik TextBox'ı değiştiğinde hesaplama yap
        private void RadioButtonCam_CheckedChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        private void RadioButtonPaspartu_CheckedChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        private void RadioButtonKutu_CheckedChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }


        // Nakit ve Kart RadioButton değiştirildiğinde yapılacak işlemler
        private void RadioButtonNakitKart_CheckedChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        // Yükseklik TextBox'ı değiştiğinde hesaplama yap
        private void TxtBxYukseklik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HesaplaVeYazdir();
                e.Handled = true;
                e.SuppressKeyPress = true; // Enter tuşunun başka bir işlem yapmasını engeller
            }
        }

        private void TxtBxToplamFiyat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    decimal manuelToplamFiyat;

                    // Eğer toplam fiyat TextBox'ı boşsa indirimi sıfırla ve eski fiyatı geri yükle
                    if (string.IsNullOrWhiteSpace(TxtBxToplamFiyat.Text))
                    {
                        TxtBxToplamFiyat.Text = hafizadakiToplamFiyat.ToString("0.00");
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        return;
                    }

                    // Manuel olarak girilen toplam fiyatı al
                    if (!decimal.TryParse(TxtBxToplamFiyat.Text.Trim(), out manuelToplamFiyat))
                    {
                        MessageBox.Show("Geçerli bir toplam fiyat giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Mevcut indirimi al
                    decimal mevcutIndirim = 0;
                    if (!decimal.TryParse(TxtBxIndirim.Text.Trim(), out mevcutIndirim))
                    {
                        mevcutIndirim = 0; // Eğer hata varsa indirimi 0 kabul et
                    }

                    // Manuel toplam fiyatı hafızadaki toplam fiyata göre karşılaştırarak indirim farkını hesapla
                    decimal indirimFarki = (hafizadakiToplamFiyat - manuelToplamFiyat);

                    // İndirimi güncelle (eğer indirim pozitifse indirim, negatifse fark olarak hesaplanır)
                    TxtBxIndirim.Text = (indirimFarki + mevcutIndirim).ToString("0.00");

                    // Yeni hafızadaki toplam fiyatı güncelle
                    hafizadakiToplamFiyat = manuelToplamFiyat;

                    e.Handled = true;
                    e.SuppressKeyPress = true; // Enter tuşunun başka bir işlem yapmasını engelle
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        // Genişlik TextBox'ı değiştiğinde hesaplama yap
        private void TxtBxGenislik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HesaplaVeYazdir();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void TxtBxAdet_TextChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir(); // Adet değiştiğinde fiyatı yeniden hesapla
        }
        private void TxtBxAdet_Leave(object sender, EventArgs e)
        {
            // Eğer TextBox boş bırakıldıysa, 1 olarak ata
            if (string.IsNullOrWhiteSpace(TxtBxAdet.Text))
            {
                TxtBxAdet.Text = "1";
            }
        }
        // Yükseklik ve Genişlik değerleriyle hesaplama yap ve sonuçları ilgili TextBox'lara yazdır
        public void HesaplaVeYazdir()
        {
            FrmAnaSayfa frmAnaSayfa = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();

            if (frmAnaSayfa == null)
            {
                MessageBox.Show("Ana Sayfa formu bulunamadı.");
                return;
            }

            decimal yukseklik, genislik, adet = 1; // Adet varsayılan olarak 1
            decimal cammetrekarefiyat, paspartumetrekarefiyat, kutuFiyat;

            // TxtBxCamFiyat.Text içindeki '₺' karakterini kaldırarak sayıyı dönüştür
            if (!decimal.TryParse(frmAnaSayfa.TxtBxCamFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out cammetrekarefiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxPaspartuFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out paspartumetrekarefiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxKutuFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out kutuFiyat))
            {
                MessageBox.Show("Geçerli fiyat bilgileri giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Adet bilgisini al (eğer boşsa, varsayılan 1 olarak kabul edilir)
            if (!string.IsNullOrWhiteSpace(TxtBxAdet.Text))
            {
                if (!decimal.TryParse(TxtBxAdet.Text.Trim(), out adet) || adet <= 0)
                {
                    MessageBox.Show("Geçerli bir adet sayısı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Yükseklik ve genişlik değerlerini al
            if (decimal.TryParse(TxtBxYukseklik.Text.Trim(), out yukseklik) &&
                decimal.TryParse(TxtBxGenislik.Text.Trim(), out genislik))
            {
                // Çerçeve fiyatını hesapla
                decimal cerceveFiyat = (((yukseklik + genislik) * 2 + CerceveListesiSatis.selectedCerceveKalınlık * 8) * CerceveListesiSatis.selectedCerceveBirimFiyat) / 100;
                cerceveFiyat *= adet;
                TxtBxCerceveFiyat.Text = cerceveFiyat.ToString();

                // Cam fiyatını hesapla
                decimal camFiyat = 0;
                if (radioButtonCam.Checked)
                {
                    camFiyat = cammetrekarefiyat * yukseklik * genislik / 10000;
                    camFiyat *= adet;
                    TxtBxCamFiyat.Text = camFiyat.ToString();
                }
                else
                {
                    TxtBxCamFiyat.Text = "0";
                }

                // Paspartu fiyatını hesapla
                decimal paspartuFiyat = 0;
                if (radioButtonPaspartu.Checked)
                {
                    paspartuFiyat = paspartumetrekarefiyat * yukseklik * genislik / 10000;
                    paspartuFiyat *= adet;
                    TxtBxPaspartuFiyat.Text = paspartuFiyat.ToString();
                }
                else
                {
                    TxtBxPaspartuFiyat.Text = "0";
                }

                // Kutu fiyatını hesapla
                decimal toplamKutuFiyat = 0;
                if (radioButtonKutu.Checked)
                {
                    toplamKutuFiyat = kutuFiyat * yukseklik * genislik / 10000;
                    toplamKutuFiyat *= adet;
                    TxtBxKutuFiyat.Text = toplamKutuFiyat.ToString();
                }
                else
                {
                    TxtBxKutuFiyat.Text = "0";
                }

                // Toplam fiyat hesaplaması
                decimal toplamFiyat = cerceveFiyat + camFiyat + paspartuFiyat + toplamKutuFiyat;

                decimal nakitIndirimOrani = 0;
                if (decimal.TryParse(frmAnaSayfa.TxtBxNakitOran.Text.Replace("%", "").Trim(), out nakitIndirimOrani))
                {
                    nakitIndirimOrani /= 100;
                }

                decimal indirimMiktari = 0;
                // Eğer Nakit seçiliyse, indirim uygula
                if (radioButtonNakit.Checked)
                {
                    indirimMiktari = toplamFiyat * nakitIndirimOrani;
                    toplamFiyat -= indirimMiktari;
                }

                // İndirim miktarını güncelle
                TxtBxIndirim.Text = indirimMiktari.ToString();

                // Hafızadaki toplam fiyatı güncelle
                hafizadakiToplamFiyat = toplamFiyat;

                TxtBxToplamFiyat.Text = toplamFiyat.ToString();
            }
            else
            {
                // Eğer değerler geçerli değilse sıfırla
                TxtBxCerceveFiyat.Text = "0";
                TxtBxCamFiyat.Text = "0";
                TxtBxPaspartuFiyat.Text = "0";
                TxtBxKutuFiyat.Text = "0";
                TxtBxToplamFiyat.Text = "0";
                TxtBxIndirim.Text = "0";
                hafizadakiToplamFiyat = 0; // Hafızayı da sıfırla
            }
        }


        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Yeni toplam fiyatı hesapla ve yazdır
            HesaplaVeYazdir();

            // Hesaplanan toplam fiyatı hafızadaki toplam fiyatla eşitle
            if (decimal.TryParse(TxtBxToplamFiyat.Text, out decimal yeniToplamFiyat))
            {
                hafizadakiToplamFiyat = yeniToplamFiyat;
            }

            // İndirim kutusunu sıfırla
            TxtBxIndirim.Text = "0";
        }

        public void MusteriGetir(string musteriAd = "", string musteriId = "")
        {
            try
            {
                MusteriSatisListePanel.Controls.Clear(); // Önce listeyi temizle

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = @"
                SELECT 
                    MUSTERIADSOYAD AS MusteriAdSoyad, 
                    MUSTERIID AS MusteriId
                FROM 
                    TBLMUSTERILER
                WHERE 
                    (@MusteriAd = '' OR MUSTERIADSOYAD LIKE '%' + @MusteriAd + '%') AND
                    (@MusteriId = '' OR CAST(MUSTERIID AS NVARCHAR) LIKE '%' + @MusteriId + '%')
                ORDER BY 
                    MUSTERIADSOYAD ASC";

                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.Add("@MusteriAd", SqlDbType.NVarChar).Value = musteriAd;
                        komut.Parameters.Add("@MusteriId", SqlDbType.NVarChar).Value = musteriId;

                        using (SqlDataReader oku = komut.ExecuteReader())
                        {
                            while (oku.Read())
                            {
                                MusteriListesiSatis arac = new MusteriListesiSatis
                                {
                                    LblListeSatisMusteriAd = { Text = oku["MusteriAdSoyad"].ToString() },
                                    LblListeSatisMusteriId = { Text = "ID: " + oku["MusteriId"].ToString() }
                                };

                                MusteriSatisListePanel.Controls.Add(arac);
                            }
                        }
                    }
                }

                // 📌 **Eğer müşteri değiştiyse, müşteri panelini güncelle**
                DateTime currentChangeTimeMusteri = FrmAnaMenu.GetLastMusterilerDatabaseChangeTime();
                if (currentChangeTimeMusteri > FrmAnaMenu.lastCheckedTimeMusteri)
                {
                    FrmMusteriler musteriform = Application.OpenForms.OfType<FrmMusteriler>().FirstOrDefault();
                    if (musteriform != null)
                    {
                        musteriform.LoadMusteriListesi(); // 📌 **Müşteri formundaki listeyi yenile**
                    }

                    // 📌 Güncelleme zamanını kaydet
                    FrmAnaMenu.lastCheckedTimeMusteri = currentChangeTimeMusteri;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }



        public void CerceveGetir(string firmaAd = "", string cerceveKod = "", string aciklama = "")
        {
            try
            {
                CerceveSatisListePanel.Controls.Clear(); // Önce listeyi temizle

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = @"
                SELECT 
                    f.FIRMAAD AS FirmaAd, 
                    k.CERCEVEKOD AS CerceveKod, 
                    c.CERCEVEACIKLAMA AS Aciklama, 
                    c.CERCEVEID AS CerceveId,
                    c.URUNRESMI AS UrunResmi
                FROM 
                    TBLCERCEVELER c
                INNER JOIN 
                    TBLFIRMALAR f ON c.FIRMAADID = f.FIRMAID
                INNER JOIN 
                    TBLCERCEVEKODLARI k ON c.CERCEVEKODID = k.KODID
                WHERE 
                    (@FirmaAd = '' OR f.FIRMAAD LIKE '%' + @FirmaAd + '%') AND
                    (@CerceveKod = '' OR k.CERCEVEKOD LIKE '%' + @CerceveKod + '%') AND
                    (@Aciklama = '' OR c.CERCEVEACIKLAMA LIKE '%' + @Aciklama + '%')
                ORDER BY 
                    f.FIRMAAD ASC";

                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.Add("@FirmaAd", SqlDbType.NVarChar).Value = firmaAd;
                        komut.Parameters.Add("@CerceveKod", SqlDbType.NVarChar).Value = cerceveKod;
                        komut.Parameters.Add("@Aciklama", SqlDbType.NVarChar).Value = aciklama;

                        using (SqlDataReader oku = komut.ExecuteReader())
                        {
                            while (oku.Read())
                            {
                                CerceveListesiSatis arac = new CerceveListesiSatis
                                {
                                    LblListeSatisCerceveKod = { Text = oku["CerceveKod"].ToString() },
                                    LblListeSatisCerceveFirmaAd = { Text = oku["FirmaAd"].ToString() },
                                    LblListeSatisCerceveAciklama = { Text = oku["Aciklama"].ToString() },
                                    LblListeSatisCerceveId = { Text = "ID: " + oku["CerceveId"].ToString() }
                                };

                                // Resmi yükleme
                                string base64Resim = oku["UrunResmi"].ToString();
                                if (!string.IsNullOrWhiteSpace(base64Resim))
                                {
                                    try
                                    {
                                        byte[] resimData = Convert.FromBase64String(base64Resim);
                                        using (MemoryStream ms = new MemoryStream(resimData))
                                        {
                                            arac.PictureBoxİmageCerceve.Image = new Bitmap(ms);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Resim yükleme sırasında bir hata oluştu: " + ex.Message);
                                    }
                                }

                                CerceveSatisListePanel.Controls.Add(arac);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çerçeve listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }


        private void BtnCerceveSatisEkle_Click(object sender, EventArgs e)
        {
            // Müşteri ve Firma seçilip seçilmediğini kontrol et
            if (MusteriListesiSatis.selectedMusteriID == 0)
            {
                MessageBox.Show("Lütfen bir müşteri seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CerceveListesiSatis.selectedFirmaAdId == 0)
            {
                MessageBox.Show("Lütfen bir firma seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Satışı onaylıyor musunuz?", "Satış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kullanıcı "Hayır" derse işlemi iptal et
            if (result != DialogResult.Yes)
            {
                return;
            }

            FrmAnaSayfa frmAnaSayfa = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();

            // TextBox'lardan veri al
            string aciklama = TxtBxAciklama.Text.Trim();
            decimal cerceveFiyat, camSatisFiyat, paspartuSatisFiyat, kutuSatisFiyat;
            decimal camFiyat, paspartuFiyat, kutuFiyat, indirim;
            decimal genislik, yukseklik, toplamFiyat;
            int adet;
            int odemeTuru; // Ödeme Türü: 0 = Nakit, 1 = Kart

            // Ödeme Türü Belirleme
            if (radioButtonNakit.Checked)
                odemeTuru = 1; // Nakit
            else if (radioButtonKart.Checked)
                odemeTuru = 0; // Kart
            else
            {
                MessageBox.Show("Lütfen bir ödeme türü seçin.");
                return;
            }

            if (!decimal.TryParse(TxtBxCerceveFiyat.Text.Trim(), out cerceveFiyat) ||
                !decimal.TryParse(TxtBxCamFiyat.Text.Trim().Replace("₺", "").Trim(), out camSatisFiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxCamFiyat.Text.Trim().Replace("₺", "").Trim(), out camFiyat) ||
                !decimal.TryParse(TxtBxPaspartuFiyat.Text.Trim().Replace("₺", "").Trim(), out paspartuSatisFiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxPaspartuFiyat.Text.Trim().Replace("₺", "").Trim(), out paspartuFiyat) ||
                !decimal.TryParse(TxtBxKutuFiyat.Text.Trim().Replace("₺", "").Trim(), out kutuSatisFiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxKutuFiyat.Text.Trim().Replace("₺", "").Trim(), out kutuFiyat) ||
                !decimal.TryParse(TxtBxGenislik.Text.Trim(), out genislik) ||
                !decimal.TryParse(TxtBxYukseklik.Text.Trim(), out yukseklik) ||
                !int.TryParse(TxtBxAdet.Text.Trim(), out adet) ||
                !decimal.TryParse(TxtBxToplamFiyat.Text.Trim(), out toplamFiyat) ||
                !decimal.TryParse(TxtBxIndirim.Text.Trim(), out indirim)) // İndirim alanını doğrulama
            {
                MessageBox.Show("Lütfen tüm alanları doğru bir şekilde doldurun.");
                return;
            }


            int musteriAdSoyadID = MusteriListesiSatis.selectedMusteriID;
            string urunResmiID = CerceveListesiSatis.selectedUrunResmiId.Trim();
            int cerceveAdID = CerceveListesiSatis.selectedFirmaAdId;
            int cerceveKodID = CerceveListesiSatis.selectedCerceveKodId;
            int birimSatisFiyatID = CerceveListesiSatis.selectedBirimSatisFiyatId;

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();

                    string sorgu = @"
                INSERT INTO TBLCERCEVESATIS 
                (MUSTERIADSOYADID, URUNRESMIID, CERCEVEADID, CERCEVEKODID, BIRIMSATISFIYATID, 
                CAMFIYAT, PASPARTUFIYAT, KUTUFIYAT, CERCEVEEN, CERCEVEBOY, 
                CAMSATISFIYAT, PASPARTUSATISFIYAT, KUTUSATISFIYAT, ACIKLAMA, 
                CERCEVESATISFIYAT, CERCEVEADET, TOPLAMADETFIYAT, GENELTOPLAM, INDIRIM, ODEMETURU)
                VALUES 
                (@musteriID, @urunResmiID, @cerceveAdID, @kodID, @birimSatisFiyatID, 
                @camFiyat, @paspartuFiyat, @kutuFiyat, @cerceveEn, @cerceveBoy, 
                @camSatisFiyat, @paspartuSatisFiyat, @kutuSatisFiyat, @Aciklama, 
                @cerceveSatisFiyat, @cerceveAdet, @toplamAdetFiyat, @genelToplam, @indirim, @odemeTuru)";

                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@musteriID", musteriAdSoyadID);
                        komut.Parameters.AddWithValue("@urunResmiID", urunResmiID);
                        komut.Parameters.AddWithValue("@cerceveAdID", cerceveAdID);
                        komut.Parameters.AddWithValue("@kodID", cerceveKodID);
                        komut.Parameters.AddWithValue("@birimSatisFiyatID", birimSatisFiyatID);
                        komut.Parameters.AddWithValue("@camFiyat", camFiyat);
                        komut.Parameters.AddWithValue("@paspartuFiyat", paspartuFiyat);
                        komut.Parameters.AddWithValue("@kutuFiyat", kutuFiyat);
                        komut.Parameters.AddWithValue("@cerceveEn", genislik);
                        komut.Parameters.AddWithValue("@cerceveBoy", yukseklik);
                        komut.Parameters.AddWithValue("@camSatisFiyat", camSatisFiyat);
                        komut.Parameters.AddWithValue("@paspartuSatisFiyat", paspartuSatisFiyat);
                        komut.Parameters.AddWithValue("@kutuSatisFiyat", kutuSatisFiyat);
                        komut.Parameters.AddWithValue("@Aciklama", aciklama);
                        komut.Parameters.AddWithValue("@cerceveSatisFiyat", cerceveFiyat);
                        komut.Parameters.AddWithValue("@cerceveAdet", adet);
                        komut.Parameters.AddWithValue("@toplamAdetFiyat", toplamFiyat / adet);
                        komut.Parameters.AddWithValue("@genelToplam", toplamFiyat);
                        komut.Parameters.AddWithValue("@indirim", indirim);
                        komut.Parameters.AddWithValue("@odemeTuru", odemeTuru);

                        komut.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Ekleme işlemi başarılı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }
        private string GetCerceveKodAdi(int cerceveKodID)
        {
            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                connection.Open();
                string sorgu = "SELECT CERCEVEKOD FROM TBLCERCEVEKODLARI WHERE KODID = @KodID";

                using (SqlCommand komut = new SqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@KodID", cerceveKodID);
                    object result = komut.ExecuteScalar();
                    return result?.ToString() ?? "Bilinmeyen Kod";
                }
            }
        }

        private string GetFirmaAdi(int firmaID)
        {
            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                connection.Open();
                string sorgu = "SELECT FIRMAAD FROM TBLFIRMALAR WHERE FIRMAID = @FirmaID";

                using (SqlCommand komut = new SqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@FirmaID", firmaID);
                    object result = komut.ExecuteScalar();
                    return result?.ToString() ?? "Bilinmeyen Firma";
                }
            }
        }
        private string GetMusteriAdi(int musteriID)
        {
            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                connection.Open();
                string sorgu = "SELECT MUSTERIADSOYAD FROM TBLMUSTERILER WHERE MUSTERIID = @MusteriID";

                using (SqlCommand komut = new SqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@MusteriID", musteriID);
                    object result = komut.ExecuteScalar();
                    return result?.ToString() ?? "Bilinmeyen Müşteri";
                }
            }
        }


        private void BtnSepetEkle_Click(object sender, EventArgs e)
        {
            if (MusteriListesiSatis.selectedMusteriID == 0)
            {
                MessageBox.Show("Lütfen bir müşteri seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CerceveListesiSatis.selectedFirmaAdId == 0)
            {
                MessageBox.Show("Lütfen bir firma seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ödeme Türü belirleme
            int odemeTuru = radioButtonNakit.Checked ? 1 : 0;

            if (!decimal.TryParse(TxtBxCerceveFiyat.Text.Trim(), out decimal cerceveFiyat) ||
                !decimal.TryParse(TxtBxCamFiyat.Text.Trim(), out decimal camFiyat) ||
                !decimal.TryParse(TxtBxPaspartuFiyat.Text.Trim(), out decimal paspartuFiyat) ||
                !decimal.TryParse(TxtBxKutuFiyat.Text.Trim(), out decimal kutuFiyat) ||
                !decimal.TryParse(TxtBxGenislik.Text.Trim(), out decimal genislik) ||
                !decimal.TryParse(TxtBxYukseklik.Text.Trim(), out decimal yukseklik) ||
                !int.TryParse(TxtBxAdet.Text.Trim(), out int adet) ||
                !decimal.TryParse(TxtBxToplamFiyat.Text.Trim(), out decimal toplamFiyat) ||
                !decimal.TryParse(TxtBxIndirim.Text.Trim(), out decimal indirim))
            {
                MessageBox.Show("Lütfen tüm alanları doğru bir şekilde doldurun.");
                return;
            }

            // Resmi Base64 formatına çevir
            string urunResmiBase64 = "";
            if (!string.IsNullOrWhiteSpace(CerceveListesiSatis.selectedUrunResmiId))
            {
                urunResmiBase64 = CerceveListesiSatis.selectedUrunResmiId.Trim();
            }

            SepetItem yeniUrun = new SepetItem
            {
                MusteriID = MusteriListesiSatis.selectedMusteriID,
                CerceveAdID = CerceveListesiSatis.selectedFirmaAdId,
                CerceveKodID = CerceveListesiSatis.selectedCerceveKodId,
                BirimSatisFiyatID = CerceveListesiSatis.selectedBirimSatisFiyatId,
                CamFiyat = camFiyat,
                PaspartuFiyat = paspartuFiyat,
                KutuFiyat = kutuFiyat,
                Genislik = genislik,
                Yukseklik = yukseklik,
                CerceveSatisFiyat = cerceveFiyat,
                Adet = adet,
                ToplamFiyat = toplamFiyat,
                Indirim = indirim,
                OdemeTuru = odemeTuru,
                Aciklama = TxtBxAciklama.Text.Trim(),
                UrunResmiBase64 = urunResmiBase64,

                // 📌 Burada adları çekiyoruz
                CerceveKodAdi = GetCerceveKodAdi(CerceveListesiSatis.selectedCerceveKodId),
                FirmaAdi = GetFirmaAdi(CerceveListesiSatis.selectedFirmaAdId),
                MusteriAdi = GetMusteriAdi(MusteriListesiSatis.selectedMusteriID) // **Müşteri Adını Alıyoruz**
            };

            sepetListesi.Add(yeniUrun);
            MessageBox.Show("Ürün sepete eklendi!");


        }

    }

}