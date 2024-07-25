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
            InitializePlaceholderText();
            CerceveGetir();
            AramaZamanlayıcısı();
        }
        private void AramaZamanlayıcısı()
        {
            aramaZaman = new Timer();
            aramaZaman.Interval = 500; // 500 ms gecikme
            aramaZaman.Tick += Arama;
        }

        private void Arama(object sender, EventArgs e)
        {
            aramaZaman.Stop(); // Timer'ı durdur
            string firmaAd = TxtBxFirmaAraCrc.Text != "Firma Adı Girin.." ? TxtBxFirmaAraCrc.Text : "";
            string cerceveKod = TxtBxKodAraCrc.Text != "Kod Girin.." ? TxtBxKodAraCrc.Text : "";
            string aciklama = TxtBxAnahtarKelimeAraCrc.Text != "Anahtar Kelime Girin.." ? TxtBxAnahtarKelimeAraCrc.Text : "";
            CerceveGetir(firmaAd, cerceveKod, aciklama);
        }


        public void CerceveGetir(string firmaAd = "", string cerceveKod = "", string aciklama = "")
        {
            try
            {
                CerceveListePanel.Controls.Clear();
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
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
                        (@Aciklama = '' OR c.CERCEVEACIKLAMA LIKE '%' + @Aciklama + '%')";

                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@FirmaAd", firmaAd);
                    komut.Parameters.AddWithValue("@CerceveKod", cerceveKod);
                    komut.Parameters.AddWithValue("@Aciklama", aciklama);

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
                                LblBirimSatisFiyat = { Text = oku["BirimSatisFiyati"].ToString() + "₺" },
                                LblCerceveID = { Text = "ID: " + oku["CerceveId"].ToString()}
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
                                    MessageBox.Show("Resim dönüştürme sırasında bir hata oluştu: " + ex.Message);
                                }
                            }

                            // Yeni CerceveListesi nesnesini panelin kontrol listesine ekle
                            CerceveListePanel.Controls.Add(arac);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çerçeve listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void InitializePlaceholderText()
        {
            SetPlaceholder(TxtBxFirmaAraCrc, "Firma Adı Girin..");
            SetPlaceholder(TxtBxKodAraCrc, "Kod Girin..");
            SetPlaceholder(TxtBxAnahtarKelimeAraCrc, "Anahtar Kelime Girin..");
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = placeholderText;
                textBox.ForeColor = Color.Gray;
            }

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;
                }
            };

            textBox.TextChanged += (s, e) =>
            {
                if (textBox.Text != placeholderText)
                {
                    aramaZaman.Start(); // Zamanlayıcıyı başlat
                }
            };
        }

        private void TxtBxFirmaAraCrc_TextChanged(object sender, EventArgs e)
        {
        }

        private void TxtBxKodAraCrc_TextChanged(object sender, EventArgs e)
        {
        }

        private void TxtBxAnahtarKelimeAraCrc_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
