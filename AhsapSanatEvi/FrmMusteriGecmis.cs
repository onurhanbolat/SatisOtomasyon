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
    public partial class FrmMusteriGecmis : Form
    {

        public FrmMusteriGecmis()
        {
            InitializeComponent();
        }


        public void LoadGecmis(string musteriId)
        {
            try
            {
                // 📌 Önce eski verileri temizle
                flowLayoutMusteriGecmis.Controls.Clear();

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();

                    string sorgu = @"
            SELECT 
                s.CERCEVESATISID, 
                f.FIRMAAD AS FirmaAd, 
                k.CERCEVEKOD AS CerceveKod, 
                s.CERCEVEEN AS En, 
                s.CERCEVEBOY AS Boy, 
                s.CAMSATISFIYAT AS CamFiyat, 
                s.PASPARTUSATISFIYAT AS PaspartuFiyat, 
                s.KUTUSATISFIYAT AS KutuFiyat, 
                s.ACIKLAMA AS Aciklama, 
                s.CERCEVESATISFIYAT AS CerceveFiyat, 
                s.CERCEVEADET AS Adet, 
                s.GENELTOPLAM AS GenelToplam, 
                s.INDIRIM AS Indirim, 
                s.SATISTARIHI AS SatisTarihi 
            FROM TBLCERCEVESATIS s
            INNER JOIN TBLFIRMALAR f ON s.CERCEVEADID = f.FIRMAID
            INNER JOIN TBLCERCEVEKODLARI k ON s.CERCEVEKODID = k.KODID
            WHERE s.MUSTERIADSOYADID = @MusteriId
            ORDER BY s.SATISTARIHI DESC";

                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@MusteriId", musteriId);

                        using (SqlDataReader oku = komut.ExecuteReader())
                        {
                            while (oku.Read())
                            {
                                MusteriGecmis musteriGecmisItem = new MusteriGecmis();

                                // 📌 **Labellara verileri ata**
                                musteriGecmisItem.LblFirmaGecmis.Text = oku["FirmaAd"].ToString();
                                musteriGecmisItem.LblKodGecmis.Text = oku["CerceveKod"].ToString();
                                musteriGecmisItem.LblEnBoyGecmis.Text = oku["En"].ToString() + " x " + oku["Boy"].ToString();
                                musteriGecmisItem.LblCamGecmis.Text = oku["CamFiyat"].ToString() + " ₺";
                                musteriGecmisItem.LblPaspartuGecmis.Text = oku["PaspartuFiyat"].ToString() + " ₺";
                                musteriGecmisItem.LblKutuGecmis.Text = oku["KutuFiyat"].ToString() + " ₺";
                                musteriGecmisItem.LblAciklamaGecmis.Text = oku["Aciklama"].ToString();
                                musteriGecmisItem.LblCerceveFiyatGecmis.Text = oku["CerceveFiyat"].ToString() + " ₺";
                                musteriGecmisItem.LblAdetGecmis.Text = oku["Adet"].ToString();
                                musteriGecmisItem.LblGenelToplamGecmis.Text = oku["GenelToplam"].ToString() + " ₺";
                                musteriGecmisItem.LblIndirimGecmis.Text = oku["Indirim"].ToString() + " ₺";
                                musteriGecmisItem.LblSatisTarihi.Text = Convert.ToDateTime(oku["SatisTarihi"]).ToString("dd.MM.yyyy");

                                // 📌 **Benzersiz satış ID'yi Tag olarak sakla**
                                musteriGecmisItem.Tag = oku["CERCEVESATISID"].ToString();

                                // 📌 **FlowLayoutPanel'e ekle**
                                flowLayoutMusteriGecmis.Controls.Add(musteriGecmisItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri geçmişi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }



    }
}