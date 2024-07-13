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

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True");

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            LoadFirmaListesi();
        }

        public void LoadFirmaListesi()
        {
            FirmaListePanel.Controls.Clear();
            connection.Open();
            SqlCommand komut = new SqlCommand("select * from TBLFIRMALAR", connection);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                FirmaListesi arac = new FirmaListesi();
                arac.LblListeFirmaAd.Text = oku["FIRMAAD"].ToString();
                arac.LblListeFirmaId.Text = "ID: " + oku["FIRMAID"].ToString();
                FirmaListePanel.Controls.Add(arac);
            }
            connection.Close();
        }

        public void UpdateFirmaDetails(string firmaId, string firmaAdi)
        {
            TxtBxFirmaID.Text = firmaId;
            TxtBxFirmaAdı.Text = firmaAdi;
            BringToFront(); // Formu ön plana getirir
        }

        private bool CheckFirmaExists(string firmaId, string firmaAdi)
        {
            connection.Open();
            SqlCommand checkCmd = new SqlCommand("select count(*) from TBLFIRMALAR where FIRMAID=@p1 and FIRMAAD=@p2", connection);
            checkCmd.Parameters.AddWithValue("@p1", firmaId);
            checkCmd.Parameters.AddWithValue("@p2", firmaAdi);

            int count = (int)checkCmd.ExecuteScalar();
            connection.Close();
            return count > 0;
        }

        private void BtnFirmaEkle_Click(object sender, EventArgs e)
        {
            string firmaAd = TxtBxFirmaAdı.Text.ToUpper();

            if (string.IsNullOrWhiteSpace(firmaAd))
            {
                MessageBox.Show("Alan Boş Bırakılamaz!");
            }
            else
            {
                connection.Open();
                SqlCommand kayit = new SqlCommand("insert into TBLFIRMALAR (FIRMAAD) values (@p1)", connection);
                kayit.Parameters.AddWithValue("@p1", firmaAd);
                kayit.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Ekleme İşlemi Başarılı");
                TxtBxFirmaID.Text = " ";
                // Yeni eklenen firmayı göstermek için listeyi güncelle
                LoadFirmaListesi();
            }
        }

        private void BtnFirmaSil_Click(object sender, EventArgs e)
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
                    connection.Open();
                    SqlCommand sil = new SqlCommand("delete from TBLFIRMALAR where FIRMAID=@p1 and FIRMAAD=@p2", connection);
                    sil.Parameters.AddWithValue("@p1", firmaId);
                    sil.Parameters.AddWithValue("@p2", firmaAd);
                    sil.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Kayıt Başarıyla Silinmiştir");
                    TxtBxFirmaID.Text = " ";
                    LoadFirmaListesi();
                    firmaId = " ";
                }
                
            }
        }

        private void BtnFirmaGuncelle_Click(object sender, EventArgs e)
        {
            if (TxtBxFirmaID.Text != " ")
            {
                connection.Open();
                SqlCommand upd = new SqlCommand("update TBLFIRMALAR set FIRMAAD=@p1 where FIRMAID=@p2", connection);
                upd.Parameters.AddWithValue("@p1", TxtBxFirmaAdı.Text);
                upd.Parameters.AddWithValue("@p2", TxtBxFirmaID.Text);
                upd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Kayıt Başarıyla Güncellenmiştir");
                TxtBxFirmaID.Text = " ";
                LoadFirmaListesi();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Firma Seçiniz!");
            }
          
        }

    }
}