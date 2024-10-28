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
    public partial class FrmCerceveSatis : Form
    {
        bool isRadioButtonCamChecked = false;

        public FrmCerceveSatis()
        {
            InitializeComponent();
        }

        private void FrmCerceveSatis_Load(object sender, EventArgs e)
        {
            try
            {
                LoadMusteriVeCerceveListesi();
                TxtBxCamFiyat.Text = "0";
                TxtBxCerceveFiyat.Text = "0";
                TxtBxToplamFiyat.Text = "0";
                TxtBxAdet.Text = "1";

                // RadioButton'ların eventlerini bağla
                radioButtonCam.CheckedChanged += RadioButtonCam_CheckedChanged;
                radioButtonNakit.CheckedChanged += RadioButtonNakitKart_CheckedChanged;
                radioButtonKart.CheckedChanged += RadioButtonNakitKart_CheckedChanged;

                // Kart RadioButton'u varsayılan olarak seçili olsun
                radioButtonKart.Checked = true;
                radioButtonCam.Checked = true;


                // TextBox'lara event handler'ları bağla
                TxtBxYukseklik.TextChanged += TxtBxYukseklik_TextChanged;
                TxtBxGenislik.TextChanged += TxtBxGenislik_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }
        private void radioButtonCam_Click(object sender, EventArgs e)
        {
            if (radioButtonCam.Checked && isRadioButtonCamChecked)
            {
                // RadioButton zaten seçiliyse, seçimini kaldır
                radioButtonCam.Checked = false;
                isRadioButtonCamChecked = false;
            }
            else
            {
                // RadioButton seçili değilse seçimini koru
                radioButtonCam.Checked = true;
                isRadioButtonCamChecked = true;
            }
        }

        // Yükseklik TextBox'ı değiştiğinde hesaplama yap
        private void RadioButtonCam_CheckedChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        // Nakit ve Kart RadioButton değiştirildiğinde yapılacak işlemler
        private void RadioButtonNakitKart_CheckedChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        // Yükseklik TextBox'ı değiştiğinde hesaplama yap
        private void TxtBxYukseklik_TextChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        // Genişlik TextBox'ı değiştiğinde hesaplama yap
        private void TxtBxGenislik_TextChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
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

            decimal yukseklik, genislik, adet = 1;  // Adet varsayılan olarak 1
            decimal cammetrekarefiyat;

            // TxtBxCamFiyat.Text içindeki '₺' karakterini kaldırarak sayıyı dönüştür
            if (!decimal.TryParse(frmAnaSayfa.TxtBxCamFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out cammetrekarefiyat))
            {
                MessageBox.Show("Geçerli bir cam fiyatı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Yükseklik ve genişlik çarpımı TxtBxCerceveFiyat'a yazdırılsın
                decimal cerceveFiyat = (((yukseklik + genislik) * 2 + CerceveListesiSatis.selectedCerceveKalınlık * 8) * CerceveListesiSatis.selectedCerceveBirimFiyat) / 50;

                // Çerçeve fiyatını adet ile çarp
                cerceveFiyat *= adet;
                TxtBxCerceveFiyat.Text = cerceveFiyat.ToString();

                // Cam RadioButton'u aktif mi?
                decimal camFiyat = 0;
                if (radioButtonCam.Checked)
                {
                    camFiyat = cammetrekarefiyat * yukseklik * genislik / 10000;

                    // Cam fiyatını adet ile çarp
                    camFiyat *= adet;
                    TxtBxCamFiyat.Text = camFiyat.ToString();
                }
                else
                {
                    TxtBxCamFiyat.Text = "0"; // Cam fiyatı devre dışı ise 0 olsun
                }

                // Toplam fiyat hesaplaması
                decimal toplamFiyat = cerceveFiyat + camFiyat;

                decimal nakitIndirimOrani = 0;
                // Nakit oranını FrmAnaSayfa formundaki TxtBxNakitOran'dan al
                if (decimal.TryParse(frmAnaSayfa.TxtBxNakitOran.Text.Replace("%", "").Trim(), out nakitIndirimOrani))
                {
                    nakitIndirimOrani /= 100;  // Oranı 100'e böl
                }
                // Eğer Nakit seçiliyse, %10 indirim uygula
                if (radioButtonNakit.Checked)
                {
                    toplamFiyat -= toplamFiyat * nakitIndirimOrani; // %10 indirim
                }

                TxtBxToplamFiyat.Text = toplamFiyat.ToString();
            }
            else
            {
                // Eğer sayı değeri girilmemişse, hesaplama yapılmasın ve alanlar sıfırlansın
                TxtBxCerceveFiyat.Text = "0";
                TxtBxCamFiyat.Text = "0";
                TxtBxToplamFiyat.Text = "0";
            }
        }


        public void LoadMusteriVeCerceveListesi()
        {
            try
            {
                MusteriSatisListePanel.Controls.Clear();
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERILER", connection);
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            MusteriListesiSatis arac = new MusteriListesiSatis
                            {
                                LblListeSatisMusteriAd = { Text = oku["MUSTERIADSOYAD"].ToString() },
                                LblListeSatisMusteriId = { Text = "ID: " + oku["MUSTERIID"].ToString() }
                            };
                            MusteriSatisListePanel.Controls.Add(arac);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
            CerceveGetir();
        }

        public void CerceveGetir(string firmaAd = "", string cerceveKod = "", string aciklama = "")
        {
            try
            {
                // Paneli temizle
                CerceveSatisListePanel.Controls.Clear();

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();

                    // SQL sorgusu
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

                    // SQL Komutu oluştur
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.Add("@FirmaAd", SqlDbType.NVarChar).Value = firmaAd;
                    komut.Parameters.Add("@CerceveKod", SqlDbType.NVarChar).Value = cerceveKod;
                    komut.Parameters.Add("@Aciklama", SqlDbType.NVarChar).Value = aciklama;

                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            // Yeni CerceveListesi nesnesi oluştur
                            CerceveListesiSatis arac = new CerceveListesiSatis
                            {
                                LblListeSatisCerceveKod = { Text = oku["CerceveKod"].ToString() },
                                LblListeSatisCerceveFirmaAd = { Text = oku["FirmaAd"].ToString() },
                                LblListeSatisCerceveAciklama = { Text = oku["Aciklama"].ToString() },
                                LblListeSatisCerceveId = { Text = "ID: " + oku["CerceveId"].ToString() }
                            };

                            // Resmi base64 string'inden bitmap'e dönüştür
                            string base64Resim = oku["UrunResmi"].ToString();
                            if (!string.IsNullOrWhiteSpace(base64Resim))
                            {
                                try
                                {
                                    byte[] resimData = Convert.FromBase64String(base64Resim);
                                    using (MemoryStream ms = new MemoryStream(resimData))
                                    {
                                        Bitmap bitmap = new Bitmap(ms);
                                        arac.PictureBoxİmageCerceve.Image = bitmap;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Resim yükleme sırasında bir hata oluştu: " + ex.Message);
                                }
                            }
                            // Yeni CerceveListesi nesnesini panelin kontrol listesine ekle
                            CerceveSatisListePanel.Controls.Add(arac);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Veritabanına bağlanırken bir hata oluştu: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çerçeve listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void BtnCerceveSatisEkle_Click(object sender, EventArgs e)
        {
            FrmAnaSayfa frmAnaSayfa = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();


            // TextBox'lardan veri al
            string aciklama = TxtBxAciklama.Text.Trim();
            decimal cerceveFiyat;
            decimal camSatisFiyat;
            decimal camFiyat;
            decimal genislik;
            decimal yukseklik;
            int adet;
            decimal toplamFiyat;

            // Gerekli verilerin doğru bir şekilde alındığından emin ol
            if (!decimal.TryParse(TxtBxCerceveFiyat.Text.Trim(), out cerceveFiyat) ||
               !decimal.TryParse(TxtBxCamFiyat.Text.Trim().Replace("₺", "").Trim(), out camSatisFiyat) || // Burayı güncelledik
               !decimal.TryParse(frmAnaSayfa.TxtBxCamFiyat.Text.Trim().Replace("₺", "").Trim(), out camFiyat) || // Burayı da güncelledik
               !decimal.TryParse(TxtBxGenislik.Text.Trim(), out genislik) ||
               !decimal.TryParse(TxtBxYukseklik.Text.Trim(), out yukseklik) ||
               !int.TryParse(TxtBxAdet.Text.Trim(), out adet) ||
               !decimal.TryParse(TxtBxToplamFiyat.Text.Trim(), out toplamFiyat))
            {
                MessageBox.Show("Lütfen tüm alanları doğru bir şekilde doldurun.");
                return;
            }

            int musteriAdSoyadID = MusteriListesiSatis.selectedMusteriID; // Müşteri Adı Soyadı ID'si
            string urunResmiID = CerceveListesiSatis.selectedUrunResmiId.Trim(); // Ürün Resmi ID'si
            int cerceveAdID = CerceveListesiSatis.selectedFirmaAdId; // Çerçeve Adı ID'si
            int cerceveKodID = CerceveListesiSatis.selectedCerceveKodId; // Çerçeve Kodu ID'si
            int birimSatisFiyatID = CerceveListesiSatis.selectedBirimSatisFiyatId; // Birim Satış Fiyatı ID'si
            decimal indirim = 0;

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();

                    // SQL sorgusu oluştur
                    string sorgu = "INSERT INTO TBLCERCEVESATIS (MUSTERIADSOYADID, URUNRESMIID, CERCEVEADID, CERCEVEKODID, BIRIMSATISFIYATID, CAMFIYAT, CERCEVEEN, CERCEVEBOY, CAMSATISFIYAT, ACIKLAMA, CERCEVESATISFIYAT, CERCEVEADET, TOPLAMADETFIYAT, GENELTOPLAM, INDIRIM) " +
                                   "VALUES (@musteriID, @urunResmiID, @cerceveAdID, @kodID, @birimSatisFiyatID, @camFiyat, @cerceveEn, @cerceveBoy, @camSatisFiyat, @Aciklama, @cerceveSatisFiyat, @cerceveAdet, @toplamAdetFiyat, @genelToplam, @indirim)";

                    // SQL komutu oluştur ve parametreleri ekle
                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@musteriID", musteriAdSoyadID);
                        komut.Parameters.AddWithValue("@urunResmiID", urunResmiID);
                        komut.Parameters.AddWithValue("@cerceveAdID", cerceveAdID);
                        komut.Parameters.AddWithValue("@kodID", cerceveKodID);
                        komut.Parameters.AddWithValue("@birimSatisFiyatID", birimSatisFiyatID);
                        komut.Parameters.AddWithValue("@camFiyat", camFiyat);
                        komut.Parameters.AddWithValue("@cerceveEn", genislik);
                        komut.Parameters.AddWithValue("@cerceveBoy", yukseklik);
                        komut.Parameters.AddWithValue("@camSatisFiyat", camSatisFiyat);
                        komut.Parameters.AddWithValue("@Aciklama", aciklama);
                        komut.Parameters.AddWithValue("@cerceveSatisFiyat", cerceveFiyat);
                        komut.Parameters.AddWithValue("@cerceveAdet", adet);
                        komut.Parameters.AddWithValue("@toplamAdetFiyat", toplamFiyat / adet);
                        komut.Parameters.AddWithValue("@genelToplam", toplamFiyat); // Genel toplam hesaplama
                        komut.Parameters.AddWithValue("@indirim", indirim);

                        // Komutu çalıştır
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

    }
}