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
using AhsapSanatEvi.Models;


namespace AhsapSanatEvi
{
    public partial class SepetCerceveler : UserControl
    {

        private List<SepetItem> sepetListesi = new List<SepetItem>();

        public SepetCerceveler()
        {
            InitializeComponent();
        }

        public void SetData(SepetItem item)
        {
            LblMusteriAdi.Text = $"Müşteri: {item.MusteriAdi}"; // **Müşteri Adını Göster**
            LblSepetKod.Text = $"Kod: {item.CerceveKodAdi}";
            LblSepetFirma.Text = $"Firma Adı: {item.FirmaAdi}";
            LblSepetYG.Text = $"Y/G: {item.Yukseklik} x {item.Genislik} cm";
            LblSepetAdet.Text = $"Adet: {item.Adet}";
            LblSepetFiyat.Text = $"{item.ToplamFiyat:C}";

            if (!string.IsNullOrWhiteSpace(item.UrunResmiBase64))
            {
                try
                {
                    byte[] resimData = Convert.FromBase64String(item.UrunResmiBase64);
                    using (MemoryStream ms = new MemoryStream(resimData))
                    {
                        PictureBoxİmageSepet.Image = new Bitmap(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yükleme hatası: " + ex.Message);
                }
            }
            else
            {
                PictureBoxİmageSepet.Image = null;
            }
        }

        private void BtnSepetCerceveSil_Click(object sender, EventArgs e)
        {
            // 📌 FrmSepet formunu bul
            FrmSepet sepetForm = Application.OpenForms.OfType<FrmSepet>().FirstOrDefault();

            if (sepetForm == null)
            {
                MessageBox.Show("Sepet ekranı açık değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 📌 FrmSepet içindeki sepet listesi alınır
            List<SepetItem> sepetListesi = sepetForm.GetSepetListesi();

            if (sepetListesi == null || sepetListesi.Count == 0)
            {
                MessageBox.Show("Sepet zaten boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 📌 Sepetteki ürün listesinde, tıklanan ürünün kimliğini bulalım
            SepetItem silinecekUrun = sepetListesi.FirstOrDefault(item =>
                item.CerceveKodAdi == LblSepetKod.Text.Replace("Kod: ", "").Trim() &&
                item.FirmaAdi == LblSepetFirma.Text.Replace("Firma Adı: ", "").Trim());

            if (silinecekUrun != null)
            {
                DialogResult result = MessageBox.Show("Bu ürünü sepetten silmek istediğinize emin misiniz?", "Ürün Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    sepetListesi.Remove(silinecekUrun); // 📌 **Listeden ürünü kaldır**
                    sepetForm.GuncelleSepet(sepetListesi); // 📌 **FrmSepet içeriğini güncelle**
                }
            }
            else
            {
                MessageBox.Show("Ürün sepet listesinde bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }

}
