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
    public partial class CerceveListesiSatis : UserControl
    {
        public static int selectedCerceveID { get; private set; } = 0;
        public static string selectedUrunResmiId { get; private set; } = string.Empty;
        public static int selectedFirmaAdId { get; private set; } = 0;
        public static int selectedCerceveKodId { get; private set; } = 0;
        public static int selectedBirimSatisFiyatId { get; private set; } = 0;
        public static int selectedCerceveKalınlık { get; private set; } = 0;
        public static int selectedCerceveBirimFiyat { get; private set; } = 0;

        private static CerceveListesiSatis selectedControl = null;


        public CerceveListesiSatis()
        {
            InitializeComponent();
        }

        public static void ResetSelectedIds()
        {
            selectedCerceveID = 0;
            selectedUrunResmiId = string.Empty;
            selectedFirmaAdId = 0;
            selectedCerceveKodId = 0;
            selectedBirimSatisFiyatId = 0;
            selectedCerceveKalınlık = 0;
            selectedCerceveBirimFiyat = 0;
        }

        private void BtnCerceveSec_Click(object sender, EventArgs e)
        {
            FrmCerceveSatis frmCerceveSatis = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            
            if (selectedControl != null && selectedControl != this)
            {
                selectedControl.ResetControlAppearance();
            }

            if (LblListeSatisCerceveKod.ForeColor == Color.FromArgb(77, 77, 77))
            {
                LblListeSatisCerceveKod.ForeColor = Color.FromArgb(204, 133, 63);
                LblListeSatisCerceveFirmaAd.ForeColor = Color.FromArgb(204, 133, 63);
                LblListeSatisCerceveAciklama.ForeColor = Color.FromArgb(204, 133, 63);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\turuncu3.png";
                BtnCerceveSec.Image = Image.FromFile(imagePath);

                try
                {
                    string cerceveIdText = LblListeSatisCerceveId.Text;
                    string cerceveIdNumber = new string(cerceveIdText.Where(char.IsDigit).ToArray());

                    // Veritabanı sorgusunu çalıştırarak çerçeve bilgilerini alıyoruz
                    (selectedCerceveID, selectedUrunResmiId, selectedFirmaAdId, selectedCerceveKodId, selectedBirimSatisFiyatId) = GetCerceveDetails(cerceveIdNumber);

                    // Kalınlık ve Birim fiyat bilgilerini alıyoruz
                    (selectedCerceveKalınlık, selectedCerceveBirimFiyat) = GetKalinlikBirim(cerceveIdNumber);
                   
                    if (frmCerceveSatis != null)
                    {
                        frmCerceveSatis.HesaplaVeYazdir();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                }

                selectedControl = this;
            }
            else
            {
                ResetControlAppearance();
                ResetSelectedIds();
                selectedControl = null;
                if (frmCerceveSatis != null)
                {
                    frmCerceveSatis.HesaplaVeYazdir();
                }
            }
        }

        private void ResetControlAppearance()
        {
            if (selectedControl != null)
            {
                selectedControl.LblListeSatisCerceveKod.ForeColor = Color.FromArgb(77, 77, 77);
                selectedControl.LblListeSatisCerceveFirmaAd.ForeColor = Color.FromArgb(77, 77, 77);
                selectedControl.LblListeSatisCerceveAciklama.ForeColor = Color.FromArgb(77, 77, 77);
                string oldImagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
                selectedControl.BtnCerceveSec.Image = Image.FromFile(oldImagePath);

            }
        }

        // TBLCERCEVELER tablosundan seçilen çerçevenin detaylarını almak için kullanılır
        private (int, string, int, int, int) GetCerceveDetails(string cerceveId)
        {
            const string query = @"
                SELECT CERCEVEID, URUNRESMI, FIRMAADID, CERCEVEKODID
                FROM TBLCERCEVELER
                WHERE CERCEVEID = @CerceveId";

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CerceveId", SqlDbType.Int).Value = int.Parse(cerceveId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int cerceveID = reader.GetInt32(0);
                            string urunResmiId = reader.GetString(1);
                            int firmaAdId = reader.GetInt32(2);
                            int cerceveKodId = reader.GetInt32(3);
                            int birimSatisFiyatId = reader.GetInt32(0);
                            return (cerceveID, urunResmiId, firmaAdId, cerceveKodId, birimSatisFiyatId);
                        }
                        else
                        {
                            return (0, string.Empty, 0, 0, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCerceveDetails: {ex.Message}");
                throw;
            }
        }

        // TBLCERCEVELER tablosundan CERCEVEKALINLIK ve CERCEVEBIRIMFIYAT verilerini almak için kullanılır
        private (int, int) GetKalinlikBirim(string cerceveId)
        {
            const string query = @"
                SELECT CERCEVEKALINLIK, BIRIMSATISFIYATI
                FROM TBLCERCEVELER
                WHERE CERCEVEID = @CerceveId";

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CerceveId", SqlDbType.Int).Value = int.Parse(cerceveId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int cerceveKalınlık = reader.GetInt32(0);
                            int cerceveBirimFiyat = reader.GetInt32(1);
                            return (cerceveKalınlık, cerceveBirimFiyat);
                        }
                        else
                        {
                            return (0, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetKalınlıkBirim: {ex.Message}");
                throw;
            }
        }

    }
}
