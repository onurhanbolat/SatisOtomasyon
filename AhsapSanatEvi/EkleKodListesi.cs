using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AhsapSanatEvi
{
    public partial class EkleKodListesi : UserControl
    {
        private static EkleKodListesi selectedControl = null;
        public static int selectedKodID { get; private set; } = 0;

        public EkleKodListesi()
        {
            InitializeComponent();
        }
        private void BtnKodListeSil_Click(object sender, EventArgs e)
        {
            FrmEkle frm = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            EkleFirmaListesi frm2 = new EkleFirmaListesi();
            try
            {
                DialogResult result = MessageBox.Show("Bu kodu silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = DataBaseControl.GetConnection())
                    {
                        connection.Open();
                        SqlCommand sil = new SqlCommand("DELETE FROM TBLCERCEVEKODLARI WHERE CERCEVEKOD=@p1", connection);
                        sil.Parameters.AddWithValue("@p1", EkleKodListeLabel.Text);
                        sil.ExecuteNonQuery();
                    }
                    MessageBox.Show("Kod Başarıyla Silinmiştir");
                    frm2.LoadCerceveKodlari(frm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kod silme sırasında bir hata oluştu: " + ex.Message);
            }

        }
        private void BtnKodSec_Click(object sender, EventArgs e)
        {
            if (selectedControl != this)
            {
                // Önceki seçili kontrol varsa, onu sıfırla
                if (selectedControl != null)
                {
                    selectedControl.DeselectKod();
                }

                // Yeni seçili kontrolü ayarla
                SelectKod();
                selectedControl = this;
            }
            else
            {
                // Aynı kontrol tekrar seçilirse, seçimi kaldır
                DeselectKod();
                selectedControl = null;
            }
        }

        private void SelectKod()
        {
            // Önce firma seçildi mi kontrol et
            if (EkleFirmaListesi.selectedFirmaID == 0)
            {
                // Firma seçilmemişse kullanıcıyı uyar
                MessageBox.Show("Lütfen firma seçiniz!");
                return;
            }

            EkleKodListeLabel.ForeColor = Color.FromArgb(204, 133, 63);
            string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\turuncu3.png";
            BtnKodSec.Image = Image.FromFile(imagePath);

            // Veritabanı işlemleri
            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("SELECT KODID FROM TBLCERCEVEKODLARI WHERE CERCEVEKOD=@p1", connection);
                    komut.Parameters.AddWithValue("@p1", EkleKodListeLabel.Text);
                    SqlDataReader oku = komut.ExecuteReader();
                    if (oku.Read())
                    {
                        selectedKodID = Convert.ToInt32(oku["KODID"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private void DeselectKod()
        {
            EkleKodListeLabel.ForeColor = Color.FromArgb(77, 77, 77);
            string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
            BtnKodSec.Image = Image.FromFile(imagePath);
            selectedKodID = 0;
        }
    }
}

