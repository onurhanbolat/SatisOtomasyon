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
    public partial class formCerceveler : Form
    {
        private Timer aramaZaman;


        public formCerceveler()
        {
            InitializeComponent();
            CerceveGetir();
        }
        private void AramaYap()
        {
            string firmaAd = TxtBxFirmaAraCrc.Text;
            string cerceveKod = TxtBxKodAraCrc.Text;
            string aciklama = TxtBxAnahtarKelimeAraCrc.Text;

            CerceveGetir(firmaAd, cerceveKod, aciklama);
        }

        private void formCerceveler_Load(object sender, EventArgs e)
        {
            TxtBxFirmaAraCrc.KeyDown += TxtBxFirmaAraCrc_KeyDown;
            TxtBxKodAraCrc.KeyDown += TxtBxKodAraCrc_KeyDown;
            TxtBxAnahtarKelimeAraCrc.KeyDown += TxtBxAnahtarKelimeAraCrc_KeyDown;
            TxtBxZam.KeyDown += TxtBxZam_KeyDown;
        }


        public void CerceveGetir(string firmaAd = "", string cerceveKod = "", string aciklama = "")
        {
            try
            {
                // Paneli temizle
                CerceveListePanel.Controls.Clear();

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
                c.BIRIMSATISFIYATI AS BirimSatisFiyati,
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
                            CerceveListesi arac = new CerceveListesi
                            {
                                LblCerceveKod = { Text = oku["CerceveKod"].ToString() },
                                LblFirmaAd = { Text = oku["FirmaAd"].ToString() },
                                LblAciklama = { Text = oku["Aciklama"].ToString() },
                                // Fiyat formatlama: decimal'e dönüştür ve para birimi formatı uygula
                                LblBirimSatisFiyat = { Text = Convert.ToDecimal(oku["BirimSatisFiyati"]).ToString("C2") },
                                LblCerceveID = { Text = "ID: " + oku["CerceveId"].ToString() }
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
                            CerceveListePanel.Controls.Add(arac);
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

      
        private void TxtBxFirmaAraCrc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AramaYap();
                e.SuppressKeyPress = true; // Enter tuşunun başka bir işlem yapmasını engelle
            }
        }

        private void TxtBxKodAraCrc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AramaYap();
                e.SuppressKeyPress = true;
            }
        }

        private void TxtBxAnahtarKelimeAraCrc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AramaYap();
                e.SuppressKeyPress = true;
            }
        }

        private void BtnZamOnayla_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(TxtBxZam.Text, out decimal zamOrani))
            {
                zamOrani = zamOrani / 100; // Yüzdeye çevirmek için 100'e böl

                // Kullanıcıya onay mesajı göster
                DialogResult result = MessageBox.Show(
                    "Tüm ürünlerin birim satış fiyatını güncellemek istediğinize emin misiniz?",
                    "Fiyat Güncelleme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes) // Eğer kullanıcı "Evet" dediyse işlemi yap
                {
                    try
                    {
                        using (SqlConnection connection = DataBaseControl.GetConnection())
                        {
                            connection.Open();
                            string sorgu = @"
                    UPDATE TBLCERCEVELER
                    SET CERCEVEBIRIMFIYAT = CERCEVEBIRIMFIYAT + (CERCEVEBIRIMFIYAT * @ZamOrani)";

                            using (SqlCommand komut = new SqlCommand(sorgu, connection))
                            {
                                komut.Parameters.Add("@ZamOrani", SqlDbType.Decimal).Value = zamOrani;
                                int etkilenenSatir = komut.ExecuteNonQuery();

                                MessageBox.Show($"Toplam {etkilenenSatir} ürünün fiyatı güncellendi.",
                                    "Bilgi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }

                        // Güncellenmiş verileri ekranda göstermek için tekrar çağır
                        CerceveGetir();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Fiyat güncellenirken bir hata oluştu: " + ex.Message,
                            "Hata",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir zam oranı girin.",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void TxtBxZam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnZamOnayla_Click(sender, e); // Enter tuşuna basıldığında zam onayla metodunu çağır
                e.SuppressKeyPress = true; // Enter'ın başka bir işlem yapmasını engelle
            }
        }

    }
}
