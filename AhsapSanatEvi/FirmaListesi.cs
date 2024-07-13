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
    public partial class FirmaListesi : UserControl
    {
        public FirmaListesi()
        {
            InitializeComponent();
        }

        public void LblListeFirmaId_Click(object sender, EventArgs e)
        {
            FrmFirmalar firmaForm = Application.OpenForms.OfType<FrmFirmalar>().FirstOrDefault();

            if (firmaForm != null)
            {
                firmaForm.UpdateFirmaDetails(LblListeFirmaId.Text.Replace("ID: ", ""), LblListeFirmaAd.Text.Replace("Ad: ", ""));
            }
        }

    }
}
