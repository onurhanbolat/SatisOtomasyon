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
        private int ExtractCerceveId(string cerceveIdText)
        {
            // ID'nin "ID: " kısmından ayıklanması
            string[] parts = cerceveIdText.Split(':');
            if (parts.Length > 1 && int.TryParse(parts[1].Trim(), out int cerceveId))
            {
                return cerceveId;
            }
            throw new FormatException("Geçersiz ID formatı.");
        }
        // Silme işlemi butonuna tıklandığında çağrılan metod
        private void BtnCerceveListeSil_Click(object sender, EventArgs e)
        {
            try
            {
                // Açık olan formCerceveler formunu bul
                var frm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();
                var anaMenuForm = Application.OpenForms.OfType<FrmAnaMenu>().FirstOrDefault();
                int cerceveid = ExtractCerceveId(LblCerceveID.Text);

                if (frm != null)
                {
                    DialogResult result = MessageBox.Show("Bu çerçeveyi silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (var connection = DataBaseControl.GetConnection())
                        {
                            connection.Open();

                            // 📌 Çerçeveyi sil
                            var sil = new SqlCommand("DELETE FROM TBLCERCEVELER WHERE CERCEVEID=@p1", connection);
                            sil.Parameters.AddWithValue("@p1", cerceveid);
                            sil.ExecuteNonQuery();

                            // 📌 SONGUNCELLEME alanını güncelle
                            var update = new SqlCommand("UPDATE TBLCERCEVELER SET SONGUNCELLEME = GETDATE()", connection);
                            update.ExecuteNonQuery();
                        }

                        MessageBox.Show("Çerçeve Başarıyla Silinmiştir");

                        // 📌 Mevcut formdaki CerceveGetir metodunu çağırarak listeyi yenile
                        frm.CerceveGetir();

                        // 📌 Satış formunu güncelle
                        var satisForm = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
                        if (satisForm != null)
                        {
                            satisForm.CerceveGetir(); // Satış listesini yenile
                        }

                        // 📌 `lastCheckedTimeCerceve` güncelle
                        if (anaMenuForm != null)
                        {
                            FrmAnaMenu.lastCheckedTimeCerceve = DateTime.Now;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ana form bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Çerçeve silme sırasında bir hata oluştu: " + ex.Message);
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
            var cerceveForm = Application.OpenForms.OfType<formCerceveler>().FirstOrDefault();

            // Eğer ana menü formu bulunamazsa işlemi iptal et
            if (anaMenuForm == null)
            {
                MessageBox.Show("Ana menü formu bulunamadı.");
                return;
            }

            // 📌 Eğer FrmEkle açık değilse yeni bir tane oluştur
            var frmEkle = Application.OpenForms.OfType<FrmEkle>().FirstOrDefault();
            if (frmEkle == null)
            {
                frmEkle = new FrmEkle();
                frmEkle.TopLevel = false;
                frmEkle.Dock = DockStyle.Fill;
                anaMenuForm.AnaMenuArkaPanel.Controls.Add(frmEkle); // 📌 Ana panele ekle
                frmEkle.Show();
            }

            // 📌 FrmEkle'yi öne getir
            frmEkle.BringToFront();

            // 📌 Buton ve textbox durumlarını düzenle
            frmEkle.groupBoxFirmalae.Enabled = false;
            frmEkle.groupBoxKodlar.Enabled = false;
            frmEkle.BtnFirmaGuncelle.Enabled = true;
            frmEkle.BtnFirmaEkle.Enabled = false;

            // 📌 Çerçeve ID'yi al
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

            // 📌 Çerçeve bilgilerini yükle
            CerceveAta(frmEkle);
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
