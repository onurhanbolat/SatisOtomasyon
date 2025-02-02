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
using AhsapSanatEvi.Models;

namespace AhsapSanatEvi
{
    public partial class MusteriGecmis : UserControl
    {
        public MusteriGecmis()
        {
            InitializeComponent();
        }

        private void BtnVeriSil_Click(object sender, EventArgs e)
        {
            // 📌 **FrmMusteriGecmis formunu bul**
            FrmMusteriGecmis musteriGecmisForm = Application.OpenForms.OfType<FrmMusteriGecmis>().FirstOrDefault();

            if (musteriGecmisForm == null)
            {
                MessageBox.Show("Müşteri geçmişi ekranı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 📌 **Seçili satışın benzersiz ID'sini çekiyoruz (CERCEVESATISID)**
            string satisId = this.Tag?.ToString();

            if (string.IsNullOrEmpty(satisId))
            {
                MessageBox.Show("Satış ID alınamadı! Silme işlemi gerçekleştirilemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 📌 **Silme onayı al**
            DialogResult result = MessageBox.Show("Bu veriyi silmek istiyor musunuz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();

                    // 📌 **Satışın gerçekten var olup olmadığını kontrol et**
                    string kontrolSorgu = "SELECT COUNT(*) FROM TBLCERCEVESATIS WHERE CERCEVESATISID = @SatisId";

                    using (SqlCommand kontrolKomut = new SqlCommand(kontrolSorgu, connection))
                    {
                        kontrolKomut.Parameters.AddWithValue("@SatisId", satisId);
                        int mevcutKayit = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                        if (mevcutKayit == 0)
                        {
                            MessageBox.Show("Silinmek istenen kayıt bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 📌 **Silme işlemini gerçekleştir**
                    string silmeSorgu = "DELETE FROM TBLCERCEVESATIS WHERE CERCEVESATISID = @SatisId";

                    using (SqlCommand komut = new SqlCommand(silmeSorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@SatisId", satisId);

                        int affectedRows = komut.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Veri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 📌 **Bu UserControl'ü FlowLayoutPanel'den kaldır**
                            if (this.Parent != null)
                            {
                                this.Parent.Controls.Remove(this);
                            }
                            else
                            {
                                musteriGecmisForm.flowLayoutMusteriGecmis.Controls.Remove(this);
                            }

                            // 📌 **FrmMusteriGecmis'teki listeyi güncelle**
                        }
                        else
                        {
                            MessageBox.Show("Veri silinemedi! Belirtilen kayıt bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri silme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}