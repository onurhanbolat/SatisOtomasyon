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
        }

        private readonly string connectionString = @"Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True";

        // Babamın Db Kodu: Data Source=BOLAT\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True 
        // Benim Db Kodum: Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True

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

        public void LoadFirmaListesi()
        {
            try
            {
                FirmaListePanel.Controls.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("select * from TBLFIRMALAR", connection);
                    using (SqlDataReader oku = komut.ExecuteReader())
                    {
                        while (oku.Read())
                        {
                            FirmaListesi arac = new FirmaListesi();
                            arac.LblListeFirmaAd.Text = oku["FIRMAAD"].ToString();
                            arac.LblListeFirmaId.Text = "ID: " + oku["FIRMAID"].ToString();
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand checkCmd = new SqlCommand("select count(*) from TBLFIRMALAR where FIRMAID=@p1 and FIRMAAD=@p2", connection);
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
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand kayit = new SqlCommand("insert into TBLFIRMALAR (FIRMAAD) values (@p1)", connection);
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
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand sil = new SqlCommand("delete from TBLFIRMALAR where FIRMAID=@p1 and FIRMAAD=@p2", connection);
                            sil.Parameters.AddWithValue("@p1", firmaId);
                            sil.Parameters.AddWithValue("@p2", firmaAd);
                            sil.ExecuteNonQuery();
                        }
                        MessageBox.Show("Kayıt Başarıyla Silinmiştir");
                        TxtBxFirmaID.Text = " ";
                        LoadFirmaListesi();
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
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand upd = new SqlCommand("update TBLFIRMALAR set FIRMAAD=@p1 where FIRMAID=@p2", connection);
                        upd.Parameters.AddWithValue("@p1", TxtBxFirmaAdı.Text);
                        upd.Parameters.AddWithValue("@p2", TxtBxFirmaID.Text);
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
    }
}