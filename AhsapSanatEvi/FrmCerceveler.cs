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
    public partial class formCerceveler : Form
    {
        public formCerceveler()
        {
            InitializeComponent();
            InitializePlaceholderText();
            CerceveGetir();
        }

        public void CerceveGetir()
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
                    c.BIRIMSATISFIYATI AS BirimSatisFiyati
                FROM 
                    TBLCERCEVELER c
                INNER JOIN 
                    TBLFIRMALAR f ON c.FIRMAADID = f.FIRMAID
                INNER JOIN 
                    TBLCERCEVEKODLARI k ON c.CERCEVEKODID = k.KODID";

                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            CerceveListesi arac = new CerceveListesi
                            {
                                LblCerceveKod = { Text = "Kod: " + oku["CerceveKod"].ToString() },
                                LblFirmaAd = { Text = "Firma Adı: " + oku["FirmaAd"].ToString() },
                                LblAciklama = { Text = "Açıklama: " + oku["Aciklama"].ToString() },
                                LblBirimSatisFiyat = { Text = "Birim Satış Fiyatı: " + oku["BirimSatisFiyati"].ToString() }
                            };
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
