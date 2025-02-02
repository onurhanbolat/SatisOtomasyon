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

            // TxtBxNakitOran, TxtBxCamFiyat, TxtBxPaspartuFiyat ve TxtBxKutuFiyat'ın olaylarını bağlayalım
            TxtBxNakitOran.TextChanged += TxtBxNakitOran_TextChanged;
            TxtBxNakitOran.Leave += TxtBxNakitOran_Leave;

            TxtBxCamFiyat.Leave += TxtBxCamFiyat_Leave;
            TxtBxCamFiyat.Enter += TxtBxCamFiyat_Enter;

            TxtBxPaspartuFiyat.Leave += TxtBxPaspartuFiyat_Leave;
            TxtBxPaspartuFiyat.Enter += TxtBxPaspartuFiyat_Enter;

            TxtBxKutuFiyat.Leave += TxtBxKutuFiyat_Leave;
            TxtBxKutuFiyat.Enter += TxtBxKutuFiyat_Enter;
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

        // TxtBxPaspartuFiyat'tan çıkıldığında sonuna ₺ işareti ekleyelim
        private void TxtBxPaspartuFiyat_Leave(object sender, EventArgs e)
        {
            var frmSatis = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            if (frmSatis != null)
            {
                frmSatis.HesaplaVeYazdir();
            }
            // Eğer kullanıcı metni boş bıraktıysa tekrar ₺ ekleme
            if (string.IsNullOrWhiteSpace(TxtBxPaspartuFiyat.Text))
            {
                return;
            }

            // ₺ işareti zaten varsa, tekrar eklemeyelim
            if (!TxtBxPaspartuFiyat.Text.EndsWith("₺"))
            {
                TxtBxPaspartuFiyat.Text = TxtBxPaspartuFiyat.Text.TrimEnd('₺').Trim() + "₺"; // Sonundaki ₺ veya boşlukları temizle
            }
        }

        // TxtBxPaspartuFiyat'a odaklandığında ₺ işaretini kaldır
        private void TxtBxPaspartuFiyat_Enter(object sender, EventArgs e)
        {
            if (TxtBxPaspartuFiyat.Text.EndsWith("₺"))
            {
                // ₺ işaretini kaldır
                TxtBxPaspartuFiyat.Text = TxtBxPaspartuFiyat.Text.TrimEnd('₺').Trim(); // Boşlukları da temizle
                // İmlecin doğru yerde kalması için metnin sonuna geçelim
                TxtBxPaspartuFiyat.SelectionStart = TxtBxPaspartuFiyat.Text.Length;
            }
        }

        // TxtBxKutuFiyat'tan çıkıldığında sonuna ₺ işareti ekleyelim
        private void TxtBxKutuFiyat_Leave(object sender, EventArgs e)
        {
            var frmSatis = Application.OpenForms.OfType<FrmCerceveSatis>().FirstOrDefault();
            if (frmSatis != null)
            {
                frmSatis.HesaplaVeYazdir();
            }
            // Eğer kullanıcı metni boş bıraktıysa tekrar ₺ ekleme
            if (string.IsNullOrWhiteSpace(TxtBxKutuFiyat.Text))
            {
                return;
            }

            // ₺ işareti zaten varsa, tekrar eklemeyelim
            if (!TxtBxKutuFiyat.Text.EndsWith("₺"))
            {
                TxtBxKutuFiyat.Text = TxtBxKutuFiyat.Text.TrimEnd('₺').Trim() + "₺"; // Sonundaki ₺ veya boşlukları temizle
            }
        }

        // TxtBxKutuFiyat'a odaklandığında ₺ işaretini kaldır
        private void TxtBxKutuFiyat_Enter(object sender, EventArgs e)
        {
            if (TxtBxKutuFiyat.Text.EndsWith("₺"))
            {
                // ₺ işaretini kaldır
                TxtBxKutuFiyat.Text = TxtBxKutuFiyat.Text.TrimEnd('₺').Trim(); // Boşlukları da temizle
                // İmlecin doğru yerde kalması için metnin sonuna geçelim
                TxtBxKutuFiyat.SelectionStart = TxtBxKutuFiyat.Text.Length;
            }
        }

        // Kullanıcının girdiği verileri sakla
        private void SaveSettings()
        {
            Properties.Settings.Default.NakitOran = TxtBxNakitOran.Text;
            Properties.Settings.Default.CamFiyat = TxtBxCamFiyat.Text;
            Properties.Settings.Default.PaspartuFiyat = TxtBxPaspartuFiyat.Text;
            Properties.Settings.Default.KutuFiyat = TxtBxKutuFiyat.Text;

            // Ayarları kaydet
            Properties.Settings.Default.Save();
        }

        // Form açıldığında ayarları geri yükle
        private void LoadSettings()
        {
            TxtBxNakitOran.Text = Properties.Settings.Default.NakitOran;
            TxtBxCamFiyat.Text = Properties.Settings.Default.CamFiyat;
            TxtBxPaspartuFiyat.Text = Properties.Settings.Default.PaspartuFiyat;
            TxtBxKutuFiyat.Text = Properties.Settings.Default.KutuFiyat;
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
