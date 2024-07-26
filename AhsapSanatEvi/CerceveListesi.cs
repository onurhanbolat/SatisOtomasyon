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
using System.IO;

namespace AhsapSanatEvi
{
    public partial class CerceveListesi : UserControl
    {
        public static int selectedCerceveID { get; private set; } = 0;

        public CerceveListesi()
        {
            InitializeComponent();
        }

        // Silme işlemi butonuna tıklandığında çağrılan metod
        private void BtnCerceveListeSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Açık olan formCerceveler formunu bul
                var frm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();

                if (frm != null)
                {
                    DialogResult result = MessageBox.Show("Bu çerçeveyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (var connection = DataBaseControl.GetConnection())
                        {
                            connection.Open();
                            var sil = new SqlCommand("DELETE FROM TBLCERCEVELER WHERE BIRIMSATISFIYATI=@p1", connection);
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

        // Belirtilen çerçeve ID'sine göre çerçeve ID'sini getiren metod
        private int GetCerceveID(int cerceveid)
        {
            const string query = @"SELECT CERCEVEID FROM TBLCERCEVELER WHERE CERCEVEID = @cerceveid";
            using (var connection = DataBaseControl.GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@cerceveid", cerceveid);
                connection.Open();
                var result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0; // null kontrolü ve dönüşüm
            }
        }

        // Düzenle butonuna tıklandığında çağrılan metod
        private void BtnCerceveListeDüzenle_Click(object sender, EventArgs e)
        {
            var anaMenuForm = Application.OpenForms.OfType<FrmAnaMenu>().FirstOrDefault();
            var frmEkle = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            var cerceveForm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();

            if (anaMenuForm != null && frmEkle != null)
            {
                anaMenuForm.lastCheckedTimeFirma = DateTime.Now;
                frmEkle.BringToFront();

                frmEkle.groupBoxFirmalae.Enabled = false;
                frmEkle.groupBoxKodlar.Enabled = false;
                frmEkle.BtnFirmaGuncelle.Enabled = true;
                frmEkle.BtnFirmaEkle.Enabled = false;

                string idText = LblCerceveID.Text.Substring(4);
                if (int.TryParse(idText, out int cerceveId))
                {
                    selectedCerceveID = GetCerceveID(cerceveId);
                }

                if (selectedCerceveID == 0)
                {
                    MessageBox.Show("Geçerli bir çerçeve ID'si bulunamadı.");
                    return;
                }

                // Çerçeve bilgilerini veritabanından yükle
                CerceveAta(frmEkle);
            }
        }

        // Çerçeve bilgilerini yükleyen metod
        private void CerceveAta(FrmEkle frmEkle)
        {
            const string query = @"
                SELECT 
                    c.CERCEVEACIKLAMA AS Aciklama, 
                    c.BIRIMSATISFIYATI AS BirimSatisFiyati,
                    c.CERCEVEKARMARJI AS KarMarji,
                    c.CERCEVEKALINLIK AS Kalinlik,
                    c.CERCEVEBIRIMFIYAT AS BirimFiyat,
                    c.URUNRESMI AS UrunResmi
                FROM 
                    TBLCERCEVELER c
                WHERE 
                    c.CERCEVEID = @CerceveID";

            try
            {
                using (var connection = DataBaseControl.GetConnection())
                using (var komut = new SqlCommand(query, connection))
                {
                    komut.Parameters.AddWithValue("@CerceveID", selectedCerceveID);
                    connection.Open();

                    using (var oku = komut.ExecuteReader())
                    {
                        if (oku.Read())
                        {
                            // Text box'lara değer ataması yapılıyor
                            frmEkle.TxtBxAciklama.Text = oku["Aciklama"].ToString();
                            frmEkle.TxtBxBirimSatisFiyat.Text = oku["BirimSatisFiyati"].ToString();
                            frmEkle.TxtBxKarMarji.Text = oku["KarMarji"].ToString();
                            frmEkle.TxtBxKalinlik.Text = oku["Kalinlik"].ToString();
                            frmEkle.TxtBxBirimFiyat.Text = oku["BirimFiyat"].ToString();

                            // Resmi base64 string'inden bitmap'e dönüştür
                            string base64Resim = oku["UrunResmi"].ToString();
                            if (!string.IsNullOrWhiteSpace(base64Resim))
                            {
                                try
                                {
                                    byte[] resimData = Convert.FromBase64String(base64Resim);
                                    using (var ms = new MemoryStream(resimData))
                                    {
                                        frmEkle.PictureBoxImage.Image = new Bitmap(ms);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Resim dönüştürme sırasında bir hata oluştu: " + ex.Message);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Veri bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çerçeve bilgileri yüklenirken bir hata oluştu: " + ex.Message);
            }
        }
    }
}
