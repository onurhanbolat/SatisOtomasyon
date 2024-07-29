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
    }
}
