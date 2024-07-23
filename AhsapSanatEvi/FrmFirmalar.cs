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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
            SetPlaceholder(TxtBxFirmaAraFrm, "Firma Ara..");
            TxtBxFirmaAdı.KeyDown += TxtBxFirmaAdı_KeyDown;
        }


        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFirmaListesi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
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
                    LoadFirmaListesi();

                }

            };
        }

        public void FirmaAra()
        {
            string sorgu = "SELECT * FROM TBLFIRMALAR WHERE FIRMAAD LIKE @firmaad";
            FirmaListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@firmaad", "%" + TxtBxFirmaAraFrm.Text + "%");
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        FirmaListesi arac = new FirmaListesi
                        {
                            LblListeFirmaAd = { Text = oku["FIRMAAD"].ToString() },
                            LblListeFirmaId = { Text = "ID: " + oku["FIRMAID"].ToString() }
                        };
                        FirmaListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        public void LoadFirmaListesi()
        {
            try
            {
                FirmaListePanel.Controls.Clear();
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("SELECT * FROM TBLFIRMALAR", connection);
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            FirmaListesi arac = new FirmaListesi
                            {
                                LblListeFirmaAd = { Text = oku["FIRMAAD"].ToString() },
                                LblListeFirmaId = { Text = "ID: " + oku["FIRMAID"].ToString() }
                            };
                            FirmaListePanel.Controls.Add(arac);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public void UpdateFirmaDetails(string firmaId, string firmaAdi)
        {
            TxtBxFirmaID.Text = firmaId;
            TxtBxFirmaAdı.Text = firmaAdi;
            BringToFront(); // Formu ön plana getirir
        }

        private bool CheckFirmaExists(string firmaId, string firmaAdi)
        {
            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM TBLFIRMALAR WHERE FIRMAID=@p1 AND FIRMAAD=@p2", connection);
                    checkCmd.Parameters.AddWithValue("@p1", firmaId);
                    checkCmd.Parameters.AddWithValue("@p2", firmaAdi);

                    int count = (int)checkCmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma kontrolü sırasında bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        private void BtnFirmaEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string firmaAd = TxtBxFirmaAdı.Text.ToUpper();

                if (string.IsNullOrWhiteSpace(firmaAd))
                {
                    MessageBox.Show("Alan Boş Bırakılamaz!");
                }
                else
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand kayit = new SqlCommand("INSERT INTO TBLFIRMALAR (FIRMAAD) VALUES (@p1)", connection);
                        kayit.Parameters.AddWithValue("@p1", firmaAd);
                        kayit.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ekleme İşlemi Başarılı");
                    TxtBxFirmaID.Text = " ";
                    // Yeni eklenen firmayı göstermek için listeyi güncelle
                    LoadFirmaListesi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void BtnFirmaSil_Click(object sender, EventArgs e)
        {
            FrmEkle frm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            try
            {
                string firmaId = TxtBxFirmaID.Text;
                string firmaAd = TxtBxFirmaAdı.Text;

                if (!CheckFirmaExists(firmaId, firmaAd))
                {
                    MessageBox.Show("ID ve Ad Eşleşmiyor Veya Böyle Bir Firma Yok!");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bu firmayı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection connection = DataBaseControl.GetConnection())
                        {
                            connection.Open();
                            SqlTransaction transaction = connection.BeginTransaction();

                            try
                            {
                                // TBLCERCEVEKODLARI tablosundan silme
                                SqlCommand silKodlar = new SqlCommand("DELETE FROM TBLCERCEVEKODLARI WHERE FIRMAADID=@p1", connection, transaction);
                                silKodlar.Parameters.AddWithValue("@p1", firmaId);
                                silKodlar.ExecuteNonQuery();

                                // TBLFIRMALAR tablosundan silme
                                SqlCommand silFirma = new SqlCommand("DELETE FROM TBLFIRMALAR WHERE FIRMAID=@p1 AND FIRMAAD=@p2", connection, transaction);
                                silFirma.Parameters.AddWithValue("@p1", firmaId);
                                silFirma.Parameters.AddWithValue("@p2", firmaAd);
                                silFirma.ExecuteNonQuery();

                                // Transaction başarılıysa işlemleri onayla
                                transaction.Commit();

                                MessageBox.Show("Kayıt Başarıyla Silinmiştir");
                                TxtBxFirmaID.Text = "";
                                LoadFirmaListesi();

                                // `FrmEkle` formu açık mı kontrol et
                                if (frm != null)
                                {
                                    frm.FirmaGetir(); // `FrmEkle` formu varsa `FirmaGetir` metodunu çağır
                                }
                            }
                            catch (Exception ex)
                            {
                                // Hata oluşursa işlemleri geri al
                                transaction.Rollback();
                                MessageBox.Show("Firma silme sırasında bir hata oluştu: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma silme sırasında bir hata oluştu: " + ex.Message);
            }
        }


        private void BtnFirmaGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtBxFirmaID.Text))
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand upd = new SqlCommand("UPDATE TBLFIRMALAR SET FIRMAAD=@p1 WHERE FIRMAID=@p2", connection);
                        upd.Parameters.AddWithValue("@p1", TxtBxFirmaAdı.Text.ToUpper());
                        upd.Parameters.AddWithValue("@p2", TxtBxFirmaID.Text.ToUpper());
                        upd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Kayıt Başarıyla Güncellenmiştir");
                    TxtBxFirmaID.Text = " ";
                    LoadFirmaListesi();
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Firma Seçiniz!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firma güncelleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void TxtBxFirmaAraFrm_TextChanged(object sender, EventArgs e)
        {
            FirmaAra();
        }
        private void TxtBxFirmaAdı_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşunun varsayılan davranışını engelle

                string firmaId = TxtBxFirmaID.Text.Trim();
                string firmaAdı = TxtBxFirmaAdı.Text.Trim();
                string placeholder = "Firma Adı";

                if (firmaAdı == placeholder)
                {
                    firmaAdı = string.Empty;
                }

                if (!string.IsNullOrEmpty(firmaId))
                {
                    // Güncelleme işlemi
                    if (!string.IsNullOrEmpty(firmaAdı))
                    {
                        BtnFirmaGuncelle.PerformClick(); // Güncelleme butonuna tıklama işlemi tetiklenir
                    }
                    else
                    {
                        MessageBox.Show("Firma adı boş olamaz!");
                    }
                }
                else
                {
                    // Ekleme işlemi
                    if (!string.IsNullOrEmpty(firmaAdı))
                    {
                        BtnFirmaEkle.PerformClick(); // Ekleme butonuna tıklama işlemi tetiklenir
                    }
                    else
                    {
                        MessageBox.Show("Firma adı boş olamaz!");
                    }
                }
            }
        }

        private void TxtBxFirmaAdı_KeyDown_1(object sender, KeyEventArgs e)
        {

        }
    }
}
