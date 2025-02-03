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

                CerceveGetir();
                TxtBxCamFiyat.Text = "0";
                TxtBxYukseklik.Text = "0";
                TxtBxGenislik.Text = "0";
                TxtBxPaspartuFiyat.Text = "0";
                TxtBxKutuFiyat.Text = "0";
                TxtBxCerceveFiyat.Text = "0";
                TxtBxToplamFiyat.Text = "0";
                TxtBxAdet.Text = "1";


                // 📌 RadioButton'ların eventlerini bağla
                radioButtonCam.CheckedChanged += RadioButtonCam_CheckedChanged;
                radioButtonPaspartu.CheckedChanged += RadioButtonPaspartu_CheckedChanged;
                radioButtonKutu.CheckedChanged += RadioButtonKutu_CheckedChanged;

                radioButtonCam.AutoCheck = false;
                radioButtonPaspartu.AutoCheck = false;
                radioButtonKutu.AutoCheck = false;

                // 📌 Kart RadioButton'u varsayılan olarak seçili olsun
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

        private void TxtBxYukseklik_TextChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }

        private void TxtBxGenislik_TextChanged(object sender, EventArgs e)
        {
            HesaplaVeYazdir();
        }


        // Yükseklik TextBox'ı değiştiğinde hesaplama yap
        private void TxtBxYukseklik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBxGenislik.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtBxGenislik_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBxAciklama.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void TxtBxAciklama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBxAdet.Focus();
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

            decimal yukseklik, genislik, adet = 1;
            decimal cammetrekarefiyat, paspartumetrekarefiyat, kutuFiyat;

            if (!decimal.TryParse(frmAnaSayfa.TxtBxCamFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out cammetrekarefiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxPaspartuFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out paspartumetrekarefiyat) ||
                !decimal.TryParse(frmAnaSayfa.TxtBxKutuFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out kutuFiyat))
            {
                MessageBox.Show("Geçerli fiyat bilgileri giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(TxtBxAdet.Text))
            {
                if (!decimal.TryParse(TxtBxAdet.Text.Trim(), out adet) || adet <= 0)
                {
                    MessageBox.Show("Geçerli bir adet sayısı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (decimal.TryParse(TxtBxYukseklik.Text.Trim(), out yukseklik) &&
                decimal.TryParse(TxtBxGenislik.Text.Trim(), out genislik))
            {
                decimal cerceveFiyat = (((yukseklik + genislik) * 2 + CerceveListesiSatis.selectedCerceveKalınlık * 8) * CerceveListesiSatis.selectedCerceveBirimFiyat) / 100;
                cerceveFiyat *= adet;
                TxtBxCerceveFiyat.Text = cerceveFiyat.ToString("0.00");

                decimal camFiyat = radioButtonCam.Checked ? cammetrekarefiyat * yukseklik * genislik / 10000 * adet : 0;
                TxtBxCamFiyat.Text = camFiyat.ToString("0.00");

                decimal paspartuFiyat = radioButtonPaspartu.Checked ? paspartumetrekarefiyat * yukseklik * genislik / 10000 * adet : 0;
                TxtBxPaspartuFiyat.Text = paspartuFiyat.ToString("0.00");

                decimal toplamKutuFiyat = radioButtonKutu.Checked ? kutuFiyat * yukseklik * genislik / 10000 * adet : 0;
                TxtBxKutuFiyat.Text = toplamKutuFiyat.ToString("0.00");

                decimal toplamFiyat = cerceveFiyat + camFiyat + paspartuFiyat + toplamKutuFiyat;
                TxtBxToplamFiyat.Text = toplamFiyat.ToString("0.00");
            }
            else
            {
                TxtBxCerceveFiyat.Text = "0.00";
                TxtBxCamFiyat.Text = "0.00";
                TxtBxPaspartuFiyat.Text = "0.00";
                TxtBxKutuFiyat.Text = "0.00";
                TxtBxToplamFiyat.Text = "0.00";
            }
        }

        public void CerceveGetir(string firmaAd = "", string cerceveKod = "", string aciklama = "")
        {
            try
            {
                CerceveSatisListePanel.Controls.Clear(); // 📌 Önce listeyi temizle

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = @"
                SELECT 
                    f.FIRMAAD AS FirmaAd, 
                    k.CERCEVEKOD AS CerceveKod, 
                    c.CERCEVEACIKLAMA AS Aciklama, 
                    c.CERCEVEID AS CerceveId,
                    c.URUNRESMI AS UrunResmi,
                    c.BIRIMSATISFIYATI AS BirimSatisFiyat
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
                                CerceveListesiSatis cerceveItem = new CerceveListesiSatis();

                                cerceveItem.LblListeSatisCerceveKod.Text = $"Kod: {oku["CerceveKod"].ToString()}";
                                cerceveItem.LblListeSatisCerceveFirmaAd.Text = $"Firma: {oku["FirmaAd"].ToString()}";
                                cerceveItem.LblListeSatisCerceveAciklama.Text = $"Açıklama: {oku["Aciklama"].ToString()}";
                                cerceveItem.LblListeSatisCerceveId.Text = $"ID: {oku["CerceveId"].ToString()}";
                                cerceveItem.LblListeSatisCerceveBirimFiyat.Text = $"{Convert.ToDecimal(oku["BirimSatisFiyat"]):C}"; // 📌 **TL formatında gösteriliyor**

                                // 📌 **Resmi yükleme işlemi**
                                string base64Resim = oku["UrunResmi"].ToString();
                                if (!string.IsNullOrWhiteSpace(base64Resim))
                                {
                                    try
                                    {
                                        byte[] resimData = Convert.FromBase64String(base64Resim);
                                        using (MemoryStream ms = new MemoryStream(resimData))
                                        {
                                            cerceveItem.PictureBoxİmageCerceve.Image = new Bitmap(ms);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Resim yükleme sırasında bir hata oluştu: " + ex.Message);
                                    }
                                }

                                CerceveSatisListePanel.Controls.Add(cerceveItem);
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
       
        private void BtnSepetEkle_Click(object sender, EventArgs e)
        {
            if (CerceveListesiSatis.selectedFirmaAdId == 0)
            {
                MessageBox.Show("Lütfen bir firma seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(TxtBxCerceveFiyat.Text.Trim(), out decimal cerceveFiyat) ||
                 !decimal.TryParse(TxtBxCamFiyat.Text.Trim(), out decimal camFiyat) ||
                 !decimal.TryParse(TxtBxPaspartuFiyat.Text.Trim(), out decimal paspartuFiyat) ||
                 !decimal.TryParse(TxtBxKutuFiyat.Text.Trim(), out decimal kutuFiyat) ||
                 !decimal.TryParse(TxtBxGenislik.Text.Trim(), out decimal genislik) ||
                 !decimal.TryParse(TxtBxYukseklik.Text.Trim(), out decimal yukseklik) ||
                 !int.TryParse(TxtBxAdet.Text.Trim(), out int adet) ||
                 !decimal.TryParse(TxtBxToplamFiyat.Text.Trim(), out decimal toplamFiyat))
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
                Aciklama = TxtBxAciklama.Text.Trim(),
                UrunResmiBase64 = urunResmiBase64,

                // 📌 Burada adları çekiyoruz
                CerceveKodAdi = GetCerceveKodAdi(CerceveListesiSatis.selectedCerceveKodId),
                FirmaAdi = GetFirmaAdi(CerceveListesiSatis.selectedFirmaAdId),
            };

            sepetListesi.Add(yeniUrun);
            MessageBox.Show("Ürün sepete eklendi!");
        }

    }

}