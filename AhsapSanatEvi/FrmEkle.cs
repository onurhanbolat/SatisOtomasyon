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

namespace AhsapSanatEvi
{
    public partial class FrmEkle : Form
    {
        public FrmEkle()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form_Resize);
        }

        SqlConnection connection = new SqlConnection(@"Data Source=BOLAT\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True ");

        //Babamın Db Kodu: Data Source=BOLAT\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True 
        //Benim Db Kodum: Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True

        private void Form_Resize(object sender, EventArgs e)
        {
            CenterGroupBox();
        }

        private void CenterGroupBox()
        {
            FrmAnaMenu frm = new FrmAnaMenu();

            if (GrpBoxGenel != null )
            {
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;
                int groupBoxWidth = GrpBoxGenel.Width;
                int groupBoxHeight = GrpBoxGenel.Height;
                int startX = (formWidth - groupBoxWidth) / 2;
                int startY = (formHeight - groupBoxHeight) / 2-28;
                GrpBoxGenel.Location = new Point(startX, startY);
            }
            
        }

    }
}