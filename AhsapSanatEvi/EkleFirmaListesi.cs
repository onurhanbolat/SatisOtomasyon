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
    public partial class EkleFirmaListesi : UserControl
    {
        public EkleFirmaListesi()
        {
            InitializeComponent();
        }

        private void BtnFirmaSec_Click(object sender, EventArgs e)
        {
            if (EkleFirmaListeLabel.ForeColor == Color.FromArgb(77, 77, 77))
            {
                EkleFirmaListeLabel.ForeColor = Color.FromArgb(204, 133, 63);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\turuncu3.png";
                BtnFirmaSec.Image = Image.FromFile(imagePath);
            }
            else
            {
                EkleFirmaListeLabel.ForeColor = Color.FromArgb(77, 77, 77);
                string imagePath = @"C:\Users\onurh\Desktop\OkulDbLogo\gri.png";
                BtnFirmaSec.Image = Image.FromFile(imagePath);

            }
        }
    }
}