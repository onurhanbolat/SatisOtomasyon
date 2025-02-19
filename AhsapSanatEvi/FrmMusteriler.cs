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
        private bool isFormatting = false;

        public FrmMusteriler()
        {
            InitializeComponent();
            TxtBxMusteriAdSoyad.KeyPress += TxtBxMusteriAdSoyad_KeyPress;
            TxtBxMusteriTelNo.KeyPress += TxtBxMusteriTelNo_KeyPress;
            TxtBxMusteriTelNo.TextChanged += TxtBxMusteriTelNo_TextChanged;
            TxtBxMusteriTelNo.GotFocus += TxtBxMusteriTelNo_GotFocus;
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            try
            {
                LoadMusteriListesi();
                TxtBxMusteriAdSoyad.KeyPress += TxtBxMusteriAdSoyad_KeyPress;
                TxtBxMusteriTelNo.KeyPress += TxtBxMusteriTelNo_KeyPress;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        public void LoadMusteriListesi()
        {
            try
            {
                MusteriListePanel.SuspendLayout();
                MusteriListePanel.Controls.Clear();
                MusteriListePanel.ResumeLayout();

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERILER ORDER BY MUSTERIADSOYAD ASC", connection);
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            MusteriListesi musteri = new MusteriListesi
                            {
                                LblListeMusteriAd = { Text = oku["MUSTERIADSOYAD"].ToString() },
                                LblListeMusteriId = { Text = "ID: " + oku["MUSTERIID"].ToString() },
                                LblListeTelefon = { Text = oku["MUSTERITELNO"].ToString() },
                                LblListeMeslek = { Text = ToTitleCase(oku["MUSTERIMESLEK"].ToString()) },
                                LblListeAciklama = { Text = ToTitleCase(oku["MUSTERIACIKLAMA"].ToString()) }

                            };
                            MusteriListePanel.Controls.Add(musteri);
                        }
                    }
                }
                MusteriListePanel.Refresh();

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
                            LblListeTelefon = { Text = oku["MUSTERITELNO"].ToString() },
                            LblListeMeslek = { Text = oku["MUSTERIMESLEK"].ToString() },
                            LblListeAciklama = { Text = oku["MUSTERIACIKLAMA"].ToString() }

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
        private void TxtBxMusteriTelNo_TextChanged(object sender, EventArgs e)
        {
            if (isFormatting) return;
            isFormatting = true;

            string text = new string(TxtBxMusteriTelNo.Text.Where(char.IsDigit).ToArray());
            if (text.StartsWith("0"))
                text = text.Substring(1);
            if (text.Length > 10)
                text = text.Substring(0, 10);

            string formattedText = "0 (";
            if (text.Length > 0) formattedText += text.Substring(0, Math.Min(3, text.Length));
            if (text.Length > 3) formattedText += ") ";
            if (text.Length > 3) formattedText += text.Substring(3, Math.Min(3, text.Length - 3));
            if (text.Length > 6) formattedText += " ";
            if (text.Length > 6) formattedText += text.Substring(6, Math.Min(4, text.Length - 6));

            TxtBxMusteriTelNo.TextChanged -= TxtBxMusteriTelNo_TextChanged;
            TxtBxMusteriTelNo.Text = formattedText;
            TxtBxMusteriTelNo.SelectionStart = formattedText.Length;
            TxtBxMusteriTelNo.TextChanged += TxtBxMusteriTelNo_TextChanged;

            isFormatting = false;
        }

        private void TxtBxMusteriTelNo_GotFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBxMusteriTelNo.Text))
            {
                TxtBxMusteriTelNo.Text = "0 (___) ___ ____";
                TxtBxMusteriTelNo.SelectionStart = 3;
            }
        }
        private void TxtBxMusteriAdSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece harf ve boşluk girişine izin ver
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        private void TxtBxMusteriTelNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam girişine izin ver
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }

        private bool MusteriVarMi(string adSoyad, string telNo)
        {
            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                connection.Open();
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM TBLMUSTERILER WHERE MUSTERIADSOYAD = @p1 AND MUSTERITELNO = @p2", connection);
                checkCmd.Parameters.AddWithValue("@p1", adSoyad);
                checkCmd.Parameters.AddWithValue("@p2", telNo);
                int count = (int)checkCmd.ExecuteScalar();
                return count > 0;
            }
        }
        private void BtnMusteriEkle_Click(object sender, EventArgs e)
        {
            string musteriAdSoyad = TxtBxMusteriAdSoyad.Text.Trim().ToUpper();
            string musteriTelNo = TxtBxMusteriTelNo.Text.Trim(); // Boş olabilir
            string musteriMeslek = TxtBxMusteriMeslek.Text.Trim();
            string musteriAciklama = TxtBxMusteriAciklama.Text.Trim();

            // Aynı isimle kayıtlı müşteri olup olmadığını kontrol et
            if (MusteriVarMi(musteriAdSoyad, musteriTelNo))
            {
                MessageBox.Show("Bu isim ve telefon numarasıyla kayıtlı bir müşteri zaten var!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(musteriAdSoyad))
            {
                MessageBox.Show("Lütfen geçerli bir Ad Soyad giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Müşteri eklensin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand kayit = new SqlCommand(@"
                INSERT INTO TBLMUSTERILER (MUSTERIADSOYAD, MUSTERITELNO, MUSTERIMESLEK, MUSTERIACIKLAMA, SONGUNCELLEME) 
                VALUES (@p1, @p2, @p3, @p4, GETDATE())", connection);

                    kayit.Parameters.AddWithValue("@p1", musteriAdSoyad);
                    kayit.Parameters.AddWithValue("@p2", string.IsNullOrEmpty(musteriTelNo) ? (object)DBNull.Value : musteriTelNo);
                    kayit.Parameters.AddWithValue("@p3", musteriMeslek);
                    kayit.Parameters.AddWithValue("@p4", musteriAciklama);

                    kayit.ExecuteNonQuery();
                }

                MessageBox.Show("Müşteri başarıyla eklendi.");

                // 📌 **TextBox'ları sıfırla**
                TemizleMusteriTextBoxlari();

                LoadMusteriListesi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri ekleme sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void BtnMusteriSil_Click(object sender, EventArgs e)
        {
            try
            {
                string musteriId = TxtBxMusteriID.Text;
                string musteriAd = TxtBxMusteriAdSoyad.Text;

                if (!CheckMusteriExists(musteriId, musteriAd))
                {
                    MessageBox.Show("ID ve Ad Eşleşmiyor Veya Böyle Bir Müşteri Yok!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                // 📌 **Müşteriyi sil**
                                SqlCommand silMusteri = new SqlCommand("DELETE FROM TBLMUSTERILER WHERE MUSTERIID=@p1", connection, transaction);
                                silMusteri.Parameters.AddWithValue("@p1", musteriId);
                                silMusteri.ExecuteNonQuery();

                                // 📌 **SONGUNCELLEME değerini güncelle**
                                SqlCommand updateTime = new SqlCommand("UPDATE TBLMUSTERILER SET SONGUNCELLEME = GETDATE()", connection, transaction);
                                updateTime.ExecuteNonQuery();

                                transaction.Commit();

                                MessageBox.Show("Müşteri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // 📌 **TextBoxları temizle**
                                TxtBxMusteriID.Text = "";
                                TxtBxMusteriAdSoyad.Text = "";

                                // 📌 **Müşteri listesini güncelle**
                                LoadMusteriListesi();

                                // 📌 **Sepet listesini güncelle**
                                FrmSepet sepetForm = Application.OpenForms.OfType<FrmSepet>().FirstOrDefault();
                                if (sepetForm != null)
                                {
                                    sepetForm.MusteriGetir();
                                }
                            }
                            catch (SqlException ex)
                            {
                                transaction.Rollback();

                                if (ex.Number == 547) // 📌 **Foreign key constraint (Bağlantılı tablo hatası)**
                                {
                                    MessageBox.Show("Bu müşteri geçmiş satışlara sahip olduğu için silinemiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    MessageBox.Show("Müşteri silme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri silme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void TemizleMusteriTextBoxlari()
        {
            TxtBxMusteriID.Text = "";
            TxtBxMusteriAdSoyad.Text = "";
            TxtBxMusteriTelNo.Clear(); 
            TxtBxMusteriMeslek.Text = "";
            TxtBxMusteriAciklama.Text = "";
        }
    }
}