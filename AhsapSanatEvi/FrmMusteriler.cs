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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
            SetPlaceholder(TxtBxMusteriAraFrm, "Müşteri Ara..");
            TxtBxMusteriAdSoyad.KeyDown += TxtBxMusteriAdı_KeyDown;
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            try
            {
                LoadMusteriListesi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
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
                    LoadMusteriListesi();
                }
            };
        }

        public void LoadMusteriListesi()
        {
            try
            {
                MusteriListePanel.Controls.Clear();
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERILER", connection);
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            MusteriListesi arac = new MusteriListesi
                            {
                                LblListeMusteriAd = { Text = oku["MUSTERIADSOYAD"].ToString() },
                                LblListeMusteriId = { Text = "ID: " + oku["MUSTERIID"].ToString() },
                                LblListeTelefon = { Text = string.IsNullOrEmpty(oku["MUSTERITELNO"].ToString()) ? "GİRİLMEDİ" : oku["MUSTERITELNO"].ToString() },
                                LblListeMeslek = { Text = string.IsNullOrEmpty(oku["MUSTERIMESLEK"].ToString()) ? "GİRİLMEDİ" : oku["MUSTERIMESLEK"].ToString() },
                                LblListeAciklama = { Text = string.IsNullOrEmpty(oku["MUSTERIACIKLAMA"].ToString()) ? "GİRİLMEDİ" : oku["MUSTERIACIKLAMA"].ToString() }
                            };
                            MusteriListePanel.Controls.Add(arac);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public void MusteriAra()
        {
            string sorgu = "SELECT * FROM TBLMUSTERILER WHERE MUSTERIADSOYAD LIKE @musteriad";
            MusteriListePanel.Controls.Clear();

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@musteriad", "%" + TxtBxMusteriAraFrm.Text + "%");
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        MusteriListesi arac = new MusteriListesi
                        {
                            LblListeMusteriAd = { Text = oku["MUSTERIADSOYAD"].ToString() },
                            LblListeMusteriId = { Text = "ID: " + oku["MUSTERIID"].ToString() },
                            LblListeTelefon = { Text = string.IsNullOrEmpty(oku["MUSTERITELNO"].ToString()) ? "GİRİLMEDİ" : oku["MUSTERITELNO"].ToString() },
                            LblListeMeslek = { Text = string.IsNullOrEmpty(oku["MUSTERIMESLEK"].ToString()) ? "GİRİLMEDİ" : oku["MUSTERIMESLEK"].ToString() },
                            LblListeAciklama = { Text = string.IsNullOrEmpty(oku["MUSTERIACIKLAMA"].ToString()) ? "GİRİLMEDİ" : oku["MUSTERIACIKLAMA"].ToString() }
                        };
                        MusteriListePanel.Controls.Add(arac);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı bağlantısı sırasında bir hata oluştu: " + ex.Message);
                }
            }
        }

        public void UpdateMusteriDetails(string musteriId, string musteriAdi, string musteriTelNo, string musteriMeslek, string musteriAciklama)
        {
            TxtBxMusteriID.Text = musteriId;
            TxtBxMusteriAdSoyad.Text = musteriAdi;
            TxtBxMusteriTelNo.Text = musteriTelNo;
            TxtBxMusteriMeslek.Text = musteriMeslek;
            TxtBxMusteriAciklama.Text = musteriAciklama;
            BringToFront(); // Formu ön plana getirir
        }

        private bool CheckMusteriExists(string musteriId, string musteriAdi)
        {
            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM TBLMUSTERILER WHERE MUSTERIID=@p1 AND MUSTERIADSOYAD=@p2", connection);
                    checkCmd.Parameters.AddWithValue("@p1", musteriId);
                    checkCmd.Parameters.AddWithValue("@p2", musteriAdi);

                    int count = (int)checkCmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri kontrolü sırasında bir hata oluştu: " + ex.Message);
                return false;
            }
        }

        private void BtnMusteriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string musteriAdSoyad = TxtBxMusteriAdSoyad.Text.ToUpper();
                string musteriTelNo = TxtBxMusteriTelNo.Text;
                string musteriMeslek = TxtBxMusteriMeslek.Text;
                string musteriAciklama = TxtBxMusteriAciklama.Text;

                if (string.IsNullOrWhiteSpace(musteriAdSoyad))
                {
                    MessageBox.Show("Müşteri Adı Boş Bırakılamaz!");
                }
                else
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand kayit = new SqlCommand("INSERT INTO TBLMUSTERILER (MUSTERIADSOYAD, MUSTERITELNO, MUSTERIMESLEK, MUSTERIACIKLAMA) VALUES (@p1, @p2, @p3, @p4)", connection);
                        kayit.Parameters.AddWithValue("@p1", musteriAdSoyad);
                        kayit.Parameters.AddWithValue("@p2", musteriTelNo);
                        kayit.Parameters.AddWithValue("@p3", musteriMeslek);
                        kayit.Parameters.AddWithValue("@p4", musteriAciklama);
                        kayit.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ekleme İşlemi Başarılı");
                    TxtBxMusteriID.Text = " ";
                    // Yeni eklenen müşteriyi göstermek için listeyi güncelle
                    LoadMusteriListesi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void BtnMusteriSil_Click(object sender, EventArgs e)
        {
            FrmEkle frm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            try
            {
                string musteriId = TxtBxMusteriID.Text;
                string musteriAd = TxtBxMusteriAdSoyad.Text;

                if (!CheckMusteriExists(musteriId, musteriAd))
                {
                    MessageBox.Show("ID ve Ad Eşleşmiyor Veya Böyle Bir Müşteri Yok!");
                }
                else
                {
                    DialogResult result = MessageBox.Show("Bu müşteriyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection connection = DataBaseControl.GetConnection())
                        {
                            connection.Open();
                            SqlTransaction transaction = connection.BeginTransaction();

                            try
                            {

                                // TBLMUSTERILER tablosundan silme
                                SqlCommand silMusteri = new SqlCommand("DELETE FROM TBLMUSTERILER WHERE MUSTERIID=@p1 AND MUSTERIADSOYAD=@p2", connection, transaction);
                                silMusteri.Parameters.AddWithValue("@p1", musteriId);
                                silMusteri.Parameters.AddWithValue("@p2", musteriAd);
                                silMusteri.ExecuteNonQuery();

                                // Transaction başarılıysa işlemleri onayla
                                transaction.Commit();

                                MessageBox.Show("Kayıt Başarıyla Silinmiştir");
                                TxtBxMusteriID.Text = "";
                                LoadMusteriListesi();

                                // `FrmEkle` formu açık mı kontrol et
                                //if (frm != null)
                                //{
                                //    frm.MusteriGetir(); // `FrmEkle` formu varsa `MusteriGetir` metodunu çağır
                                //}
                            }
                            catch (Exception ex)
                            {
                                // Hata oluşursa işlemleri geri al
                                transaction.Rollback();
                                MessageBox.Show("Müşteri silme sırasında bir hata oluştu: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri silme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void BtnMusteriGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(TxtBxMusteriID.Text))
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand upd = new SqlCommand("UPDATE TBLMUSTERILER SET MUSTERIADSOYAD=@p1, MUSTERITELNO=@p2, MUSTERIMESLEK=@p3, MUSTERIACIKLAMA=@p4 WHERE MUSTERIID=@p5", connection);
                        upd.Parameters.AddWithValue("@p1", TxtBxMusteriAdSoyad.Text.ToUpper());
                        upd.Parameters.AddWithValue("@p2", TxtBxMusteriTelNo.Text);
                        upd.Parameters.AddWithValue("@p3", TxtBxMusteriMeslek.Text);
                        upd.Parameters.AddWithValue("@p4", TxtBxMusteriAciklama.Text);
                        upd.Parameters.AddWithValue("@p5", TxtBxMusteriID.Text);
                        upd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Kayıt Başarıyla Güncellenmiştir");
                    TxtBxMusteriID.Text = " ";
                    LoadMusteriListesi();
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Müşteri Seçiniz!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri güncelleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void TxtBxMusteriAraFrm_TextChanged(object sender, EventArgs e)
        {
            MusteriAra();
        }

        private void TxtBxMusteriAdı_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter tuşunun varsayılan davranışını engelle

                string musteriId = TxtBxMusteriID.Text.Trim();
                string musteriAdı = TxtBxMusteriAdSoyad.Text.Trim();
                string placeholder = "Müşteri Adı";

                if (musteriAdı == placeholder)
                {
                    musteriAdı = string.Empty;
                }

                if (!string.IsNullOrEmpty(musteriId))
                {
                    // Güncelleme işlemi
                    if (!string.IsNullOrEmpty(musteriAdı))
                    {
                        BtnMusteriGuncelle.PerformClick(); // Güncelleme butonuna tıklama işlemi tetiklenir
                    }
                    else
                    {
                        MessageBox.Show("Müşteri adı boş olamaz!");
                    }
                }
                else
                {
                    // Ekleme işlemi
                    if (!string.IsNullOrEmpty(musteriAdı))
                    {
                        BtnMusteriEkle.PerformClick(); // Ekleme butonuna tıklama işlemi tetiklenir
                    }
                    else
                    {
                        MessageBox.Show("Müşteri adı boş olamaz!");
                    }
                }
            }
        }

        private void TxtBxMusteriAdı_KeyDown_1(object sender, KeyEventArgs e)
        {
            // Boş bırakabilirsiniz
        }
    }
}
