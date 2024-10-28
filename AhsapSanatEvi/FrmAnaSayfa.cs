using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AhsapSanatEvi
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();

            // Form yüklendiğinde ayarları yükle
            LoadSettings();

            // TxtBxNakitOran ve TxtBxCamFiyat'ın olaylarını bağlayalım
            TxtBxNakitOran.TextChanged += TxtBxNakitOran_TextChanged;
            TxtBxNakitOran.Leave += TxtBxNakitOran_Leave;

            // TxtBxCamFiyat için olayları bağlayalım
            TxtBxCamFiyat.Leave += TxtBxCamFiyat_Leave; 
            TxtBxCamFiyat.Enter += TxtBxCamFiyat_Enter; // Enter olayını dinleyelim
        }

        // TxtBxNakitOran'daki değişikliklerde başına % işareti ekleyelim
        private void TxtBxNakitOran_TextChanged(object sender, EventArgs e)
        {
            // Yüzde işareti zaten varsa, tekrar eklemeyelim
            if (!TxtBxNakitOran.Text.StartsWith("%"))
            {
                TxtBxNakitOran.Text = "%" + TxtBxNakitOran.Text;
                // İmlecin doğru yerde kalması için metnin sonuna geçelim
                TxtBxNakitOran.SelectionStart = TxtBxNakitOran.Text.Length;
                
            }
        }

        // TxtBxNakitOran'dan çıkıldığında başına % işareti eklemeyi kontrol et
        private void TxtBxNakitOran_Leave(object sender, EventArgs e)
        {
            var frmSatis = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            if (frmSatis != null)
            {
                frmSatis.HesaplaVeYazdir();
            }
            // Eğer kullanıcı metni boş bıraktıysa tekrar yüzde ekleme
            if (string.IsNullOrWhiteSpace(TxtBxNakitOran.Text))
            {
                return;
            }

            // Yüzde işareti zaten varsa, tekrar eklemeyelim
            if (!TxtBxNakitOran.Text.StartsWith("%"))
            {
                TxtBxNakitOran.Text = "%" + TxtBxNakitOran.Text;
            }
        }

        // TxtBxCamFiyat'tan çıkıldığında sonuna ₺ işareti ekleyelim
        private void TxtBxCamFiyat_Leave(object sender, EventArgs e)
        {
            var frmSatis = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            if (frmSatis != null)
            {
                frmSatis.HesaplaVeYazdir();
            }
            // Eğer kullanıcı metni boş bıraktıysa tekrar ₺ ekleme
            if (string.IsNullOrWhiteSpace(TxtBxCamFiyat.Text))
            {
                return;
            }

            // ₺ işareti zaten varsa, tekrar eklemeyelim
            if (!TxtBxCamFiyat.Text.EndsWith("₺"))
            {
                TxtBxCamFiyat.Text = TxtBxCamFiyat.Text.TrimEnd('₺').Trim() + "₺"; // Sonundaki ₺ veya boşlukları temizle
            }
        }

        // TxtBxCamFiyat'a odaklandığında ₺ işaretini kaldır
        private void TxtBxCamFiyat_Enter(object sender, EventArgs e)
        {
            if (TxtBxCamFiyat.Text.EndsWith("₺"))
            {
                // ₺ işaretini kaldır
                TxtBxCamFiyat.Text = TxtBxCamFiyat.Text.TrimEnd('₺').Trim(); // Boşlukları da temizle
                // İmlecin doğru yerde kalması için metnin sonuna geçelim
                TxtBxCamFiyat.SelectionStart = TxtBxCamFiyat.Text.Length;
            }
        }

        // Kullanıcının girdiği verileri sakla
        private void SaveSettings()
        {
            Properties.Settings.Default.NakitOran = TxtBxNakitOran.Text;
            Properties.Settings.Default.CamFiyat = TxtBxCamFiyat.Text;

            // Ayarları kaydet
            Properties.Settings.Default.Save();
        }

        // Form açıldığında ayarları geri yükle
        private void LoadSettings()
        {
            TxtBxNakitOran.Text = Properties.Settings.Default.NakitOran;
            TxtBxCamFiyat.Text = Properties.Settings.Default.CamFiyat;
        }

        // Form kapatılmadan önce ayarları kaydet
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SaveSettings();
        }
        private void FrmAnaSayfa_Leave(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
