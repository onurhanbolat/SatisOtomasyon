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
    public partial class EkleKodListesi : UserControl
    {
        public EkleKodListesi()
        {
            InitializeComponent();
        }

        private void BtnKodListeSil_Click(object sender, EventArgs e)
        {
            FrmEkle frm = new FrmEkle();
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
                    frm.KodGetir();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kod silme sırasında bir hata oluştu: " + ex.Message);
            }

        }
    }
}
