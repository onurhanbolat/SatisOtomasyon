﻿using System;
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
    public partial class FrmEkle : Form
    {
        public FrmEkle()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form_Resize);
            // TxtBxFirmaAra için placeholder metni ekleyin
            TextBoxPlaceholder(TxtBxFirmaAra, "Firma Ara..");
            TextBoxPlaceholder(TxtBxKodAra, "Kod Ara..");
            TextBoxPlaceholder(TxtBxKodEkle, "Kod Ekle..");
            FirmaGetir();
            KodGetir();

            // TxtBxFirmaAra TextChanged olayını bağlayın
            TxtBxFirmaAra.TextChanged += new EventHandler(TxtBxFirmaAra_TextChanged_1);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            CenterGroupBox();
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
        public void KodAra()
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
                PictureBoxImage.Image = new Bitmap(ofd.FileName);
                MessageBox.Show(ofd.FileName);
            }
        }

        private void TextBoxPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    FirmaGetir();
                    KodGetir();
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    FirmaGetir();
                    KodGetir();
                }
            };

        }

        private void BtnKodEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string firmaAd = TxtBxKodEkle.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(firmaAd))
                {
                    MessageBox.Show("Alan Boş Bırakılamaz!");
                }
                else
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand kayit = new SqlCommand("INSERT INTO TBLCERCEVEKODLARI (CERCEVEKOD) VALUES (@p1)", connection);
                        kayit.Parameters.AddWithValue("@p1", firmaAd);
                        kayit.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ekleme İşlemi Başarılı");
                    TextBoxPlaceholder(TxtBxKodEkle, "Kod Ekle..");
                    // Yeni eklenen firmayı göstermek için listeyi güncelle
                    KodGetir();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }


        private void TxtBxFirmaAra_TextChanged_1(object sender, EventArgs e)
        {
            FirmaAra();
        }

        private void TxtBxKodAra_TextChanged(object sender, EventArgs e)
        {
            KodAra();
        }
    }
    }

