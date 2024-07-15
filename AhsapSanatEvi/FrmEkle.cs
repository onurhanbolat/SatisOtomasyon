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
            FirmaGetir();
            // TxtBxFirmaAra için placeholder metni ekleyin
            InitializePlaceholder(TxtBxFirmaAra, "Firma Ara..");
            InitializePlaceholder(TxtBxKodAra, "Kod Ara..");
            InitializePlaceholder(TxtBxKodEkle, "Kod Ekle..");
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True");

        // Babamın Db Kodu: Data Source=BOLAT\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True 
        // Benim Db Kodum: Data Source=DESKTOP-0MFCG1S\SQLEXPRESS;Initial Catalog=dbAhsapSanatEvi;Integrated Security=True

        private void Form_Resize(object sender, EventArgs e)
        {
            CenterGroupBox();
        }

        public void FirmaGetir()
        {
            string sorgu = "select * from TBLFIRMALAR ORDER BY FIRMAAD ASC";
            EkleFirmaListePanel.Controls.Clear();
            connection.Open();
            SqlCommand komut = new SqlCommand(sorgu, connection);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                EkleFirmaListesi arac = new EkleFirmaListesi();
                arac.EkleFirmaListeLabel.Text = oku["FIRMAAD"].ToString();
                EkleFirmaListePanel.Controls.Add(arac);
            }
            connection.Close();
        }

        private void CenterGroupBox()
        {
            FrmAnaMenu frm = new FrmAnaMenu();

            if (GrpBoxGenel != null)
            {
                int formWidth = this.ClientSize.Width;
                int formHeight = this.ClientSize.Height;
                int groupBoxWidth = GrpBoxGenel.Width;
                int groupBoxHeight = GrpBoxGenel.Height;
                int startX = (formWidth - groupBoxWidth) / 2;
                int startY = (formHeight - groupBoxHeight) / 2 - 27;
                GrpBoxGenel.Location = new Point(startX, startY);
            }
        }

        private void BtnYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "RESIM SEÇME EKRANI";
            ofd.Filter = "PNG | *.png | JPG-JPEG | *.jpg;*.jpeg | All Files | *.*";
            ofd.FilterIndex = 3;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PictureBoxImage.Image = new Bitmap(ofd.FileName);
                MessageBox.Show(ofd.FileName.ToString());
            }
        }

        private void InitializePlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

    }
}