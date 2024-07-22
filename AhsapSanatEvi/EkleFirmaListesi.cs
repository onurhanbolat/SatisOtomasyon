using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AhsapSanatEvi
{
    public partial class EkleFirmaListesi : UserControl
    {
        public static int selectedFirmaID { get; private set; } = 0;
        private static EkleFirmaListesi selectedControl = null; // Static olarak seçilen kontrolü tutar

        public EkleFirmaListesi()
        {
            InitializeComponent();
        }

        private void BtnFirmaSec_Click(object sender, EventArgs e)
        {
            FrmEkle frmEkle = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();

            if (selectedControl != null && selectedControl != this)
            {
                // Önceden seçilen kontrolü eski haline döndür
                selectedControl.EkleFirmaListeLabel.ForeColor = Color.FromArgb(77, 77, 77);
                string oldImagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
                selectedControl.BtnFirmaSec.Image = Image.FromFile(oldImagePath);
            }

            if (EkleFirmaListeLabel.ForeColor == Color.FromArgb(77, 77, 77))
            {
                EkleFirmaListeLabel.ForeColor = Color.FromArgb(204, 133, 63);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\turuncu3.png";
                BtnFirmaSec.Image = Image.FromFile(imagePath);

                // Veritabanı işlemleri
                try
                {
                    selectedFirmaID = GetFirmaID(EkleFirmaListeLabel.Text);
                    if (selectedFirmaID > 0)
                    {
                        LoadCerceveKodlari(frmEkle);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }

                // Yeni seçilen kontrolü güncelle
                selectedControl = this;
            }
            else
            {
                EkleFirmaListeLabel.ForeColor = Color.FromArgb(77, 77, 77);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
                BtnFirmaSec.Image = Image.FromFile(imagePath);
                frmEkle.KodGetir();
                selectedFirmaID = 0;

                // Seçimi kaldır
                selectedControl = null;
            }
        }

        private int GetFirmaID(string firmaAd)
        {
            const string query = @"SELECT FIRMAID FROM TBLFIRMALAR WHERE FIRMAAD = @FirmaAd";
            using (SqlConnection connection = DataBaseControl.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirmaAd", firmaAd);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public void LoadCerceveKodlari(FrmEkle frmEkle)
        {
            const string query = @"SELECT CERCEVEKOD FROM TBLCERCEVEKODLARI WHERE FIRMAADID = @FirmaID ORDER BY CERCEVEKOD ASC";
            using (SqlConnection connection = DataBaseControl.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirmaID", selectedFirmaID);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    frmEkle.EkleKodListePanel.Controls.Clear(); // Önceki verileri temizle
                    while (reader.Read())
                    {
                        EkleKodListesi arac = new EkleKodListesi
                        {
                            EkleKodListeLabel = { Text = reader["CERCEVEKOD"].ToString() }
                        };
                        frmEkle.EkleKodListePanel.Controls.Add(arac);
                    }
                }
            }
        }
    }
}
