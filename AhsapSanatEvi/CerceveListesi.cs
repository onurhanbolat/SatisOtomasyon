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
    public partial class CerceveListesi : UserControl
    {
        public CerceveListesi()
        {
            InitializeComponent();
        }

        private void BtnCerceveListeSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Açık olan formCerceveler formunu bul
                formCerceveler frm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();

                if (frm != null)
                {
                    DialogResult result = MessageBox.Show("Bu çerçeveyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection connection = DataBaseControl.GetConnection())
                        {
                            connection.Open();
                            SqlCommand sil = new SqlCommand("DELETE FROM TBLCERCEVELER WHERE BIRIMSATISFIYATI=@p1", connection);
                            sil.Parameters.AddWithValue("@p1", LblBirimSatisFiyat.Text);
                            sil.ExecuteNonQuery();
                        }
                        MessageBox.Show("Çerçeve Başarıyla Silinmiştir");

                        // Mevcut formdaki CerceveGetir metodunu çağır
                        frm.CerceveGetir();
                    }
                }
                else
                {
                    MessageBox.Show("Ana form bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kod silme sırasında bir hata oluştu: " + ex.Message);
            }
        }
    }
}
