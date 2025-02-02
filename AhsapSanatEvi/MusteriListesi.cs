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
    public partial class MusteriListesi : UserControl
    {
        public MusteriListesi()
        {
            InitializeComponent();
        }

        private void LblListeMusteriId_Click(object sender, EventArgs e)
        {
            FrmMusteriler musteriForm = Application.OpenForms.OfType<FrmMusteriler>().FirstOrDefault();

            if (musteriForm != null)
            {
                musteriForm.UpdateMusteriDetails(LblListeMusteriId.Text.Replace("ID: ", ""), LblListeMusteriAd.Text.Replace("Ad: ", ""), LblListeTelefon.Text.Replace("GİRİLMEDİ", ""), LblListeMeslek.Text.Replace("GİRİLMEDİ", ""), LblListeAciklama.Text.Replace("GİRİLMEDİ", ""));
            }
        }

        private void BtnMusteriSec_Click(object sender, EventArgs e)
        {
            FrmAnaMenu anaMenuForm = Application.OpenForms.OfType<FrmAnaMenu>().FirstOrDefault();

            if (anaMenuForm != null)
            {
                FrmMusteriGecmis musteriGecmisForm = Application.OpenForms.OfType<FrmMusteriGecmis>().FirstOrDefault();

                if (musteriGecmisForm == null)
                {
                    musteriGecmisForm = new FrmMusteriGecmis();
                    musteriGecmisForm.TopLevel = false;
                    musteriGecmisForm.Dock = DockStyle.Fill;
                    anaMenuForm.AnaMenuArkaPanel.Controls.Add(musteriGecmisForm);
                    musteriGecmisForm.Show();
                }

                // 📌 **Müşteri ID'yi al ve geçmişi yükle**
                string musteriId = LblListeMusteriId.Text.Replace("ID: ", "");

                musteriGecmisForm.LoadGecmis(musteriId); // 📌 **Metot artık ID alıyor!**

                musteriGecmisForm.BringToFront();
            }
        }





    }
}