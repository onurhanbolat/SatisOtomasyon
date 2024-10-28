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
    public partial class MusteriListesiSatis : UserControl
    {
        public static int selectedMusteriID { get; private set; } = 0;
        private static MusteriListesiSatis selectedControl = null; // Static olarak seçilen kontrolü tutar
        public MusteriListesiSatis()
        {
            InitializeComponent();
        }
        public static void ResetSelectedMusteriID()
        {
            selectedMusteriID = 0;
        }

        private void BtnMusteriSec_Click(object sender, EventArgs e)
        {

            if (selectedControl != null && selectedControl != this)
            {
                // Önceden seçilen kontrolü eski haline döndür
                selectedControl.LblListeSatisMusteriAd.ForeColor = Color.FromArgb(77, 77, 77);
                string oldImagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
                selectedControl.BtnMusteriSec.Image = Image.FromFile(oldImagePath);
            }

            if (LblListeSatisMusteriAd.ForeColor == Color.FromArgb(77, 77, 77))
            {
                LblListeSatisMusteriAd.ForeColor = Color.FromArgb(204, 133, 63);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\turuncu3.png";
                BtnMusteriSec.Image = Image.FromFile(imagePath);
                selectedMusteriID = GetMusteriID(LblListeSatisMusteriAd.Text);
                selectedControl = this;

            }
            else
            {
                LblListeSatisMusteriAd.ForeColor = Color.FromArgb(77, 77, 77);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
                BtnMusteriSec.Image = Image.FromFile(imagePath);
                selectedMusteriID = 0;
                selectedControl = null;
            }
        }
        private int GetMusteriID(string musteriAdSoyad)
        {
            const string query = @"SELECT MUSTERIID FROM TBLMUSTERILER WHERE MUSTERIADSOYAD = @MusteriAdSoyad";
            using (SqlConnection connection = DataBaseControl.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MusteriAdSoyad", musteriAdSoyad);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

    }
}
