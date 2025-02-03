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
    public partial class FrmSepet : Form
    {
        private List<SepetItem> sepetListesi = new List<SepetItem>();

        public FrmSepet(List<SepetItem> sepet)
        {
            InitializeComponent();
            sepetListesi = sepet ?? new List<SepetItem>(); // 📌 **Eğer null ise boş liste ata**
        }

        private void FrmSepet_Load(object sender, EventArgs e)
        {
            flowLayoutPanelSepet.Controls.Clear();

            foreach (var item in sepetListesi)
            {
                SepetCerceveler sepetItem = new SepetCerceveler();
                sepetItem.SetData(item);
                flowLayoutPanelSepet.Controls.Add(sepetItem);
                MusteriListesiSatis.ResetSelectedMusteriID();

            }

            // 📌 **Kart ödeme türünü varsayılan olarak seç**
            radioButtonKart.Checked = true;
            radioButtonNakit.Checked = false;

            // 📌 **RadioButton değişiklik eventlerini bağla**
            radioButtonNakit.CheckedChanged += RadioButtonOdemeTuru_CheckedChanged;
            radioButtonKart.CheckedChanged += RadioButtonOdemeTuru_CheckedChanged;

            // 📌 **Kart varsayılan olduğundan UygulaNakitIndirimi değil, ToplamFiyatHesapla çağır**
            ToplamFiyatHesapla();
            MusteriGetir();

            // 📌 **Müşteri arama kutusunu dinlemeye başla**
            TxtBxMusteriAra.TextChanged += TxtBxMusteriAra_TextChanged;

        }



        private void RadioButtonOdemeTuru_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNakit.Checked)
            {
                UygulaNakitIndirimi();  // 📌 **Nakit seçiliyse indirim uygula**
            }
            else
            {
                ToplamFiyatHesapla();  // 📌 **Kart seçiliyse normal fiyatı göster**
                TxtBxSepetIndirim.Text = "₺0,00";
            }
        }
        private decimal HesaplaNakitIndirimi(decimal toplamFiyat)
        {
            decimal nakitIndirimOrani = 0;
            FrmAnaSayfa frmAnaSayfa = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();

            if (frmAnaSayfa != null && decimal.TryParse(frmAnaSayfa.TxtBxNakitOran.Text.Replace("%", "").Trim(), out nakitIndirimOrani))
            {
                nakitIndirimOrani /= 100;  // 📌 **%5 -> 0.05 olarak çevriliyor**
                return toplamFiyat * nakitIndirimOrani;  // 📌 **İndirim hesapla**
            }

            return 0;  // 📌 **Eğer indirim oranı geçerli değilse, 0 kabul et**
        }


        public void GuncelleSepet(List<SepetItem> yeniSepetListesi)
        {
            sepetListesi = yeniSepetListesi;  // 📌 Güncel sepet listesini kaydet
            flowLayoutPanelSepet.Controls.Clear();  // 📌 Arayüzdeki eski listeyi temizle

            foreach (var item in sepetListesi)
            {
                SepetCerceveler sepetItem = new SepetCerceveler();
                sepetItem.SetData(item);
                flowLayoutPanelSepet.Controls.Add(sepetItem);
            }

            // 📌 Sepet boşaldıysa fiyatları sıfırla
            if (sepetListesi.Count == 0)
            {
                TxtBxSepetToplamFiyat.Text = "₺0";
                TxtBxSepetIndirim.Text = "₺0";
                return;
            }

            // 📌 Eğer Nakit seçiliyse indirim uygula
            if (radioButtonNakit.Checked)
            {
                UygulaNakitIndirimi();
            }
            else
            {
                ToplamFiyatHesapla(); // 📌 Kart seçiliyse normal fiyatı göster
                TxtBxSepetIndirim.Text = "₺0"; // 📌 Kart seçiliyse indirimi sıfırla
            }
        }



        private void UygulaNakitIndirimi()
        {
            decimal toplamFiyat = sepetListesi.Sum(item => item.ToplamFiyat); // Tüm ürünlerin toplamını hesapla
            decimal indirimMiktari = 0;
            decimal nakitIndirimOrani = 0;

            FrmAnaSayfa frmAnaSayfa = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();
            if (frmAnaSayfa != null && decimal.TryParse(frmAnaSayfa.TxtBxNakitOran.Text.Replace("%", "").Trim(), out nakitIndirimOrani))
            {
                nakitIndirimOrani /= 100;  // **%5 -> 0.05 olarak çevriliyor**
                indirimMiktari = toplamFiyat * nakitIndirimOrani;  // **İndirim hesapla**
                toplamFiyat -= indirimMiktari;  // **İndirimli fiyatı hesapla**
            }

            TxtBxSepetToplamFiyat.Text = $"{toplamFiyat:C}";  // 📌 Yeni fiyatı textbox'a yazdır
            TxtBxSepetIndirim.Text = $"{indirimMiktari:C}";  // 📌 **İndirim tutarını yazdır**
        }


        private void BtnSepetSat_Click(object sender, EventArgs e)
        {
            if (sepetListesi.Count == 0)
            {
                MessageBox.Show("Lütfen ürün ekleyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // 📌 Satış işlemini durdur
            }

            // 📌 **Müşteri seçilmiş mi kontrol et**
            if (MusteriListesiSatis.selectedMusteriID == 0)
            {
                MessageBox.Show("Lütfen bir müşteri seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 📌 Kullanıcıya satış onayı sor
            DialogResult result = MessageBox.Show("Satışı onaylamak istiyor musunuz?", "Satış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 📌 Eğer kullanıcı "Hayır" derse işlemi iptal et
            if (result != DialogResult.Yes)
            {
                return;
            }

            using (SqlConnection connection = DataBaseControl.GetConnection())
            {
                connection.Open();

                FrmAnaSayfa frmAnaSayfa = Application.OpenForms.OfType<FrmAnaSayfa>().FirstOrDefault();
                if (frmAnaSayfa == null)
                {
                    MessageBox.Show("Ana sayfa bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 📌 **FrmAnaSayfa`dan güncel fiyatları çekiyoruz**
                decimal camMetrekareFiyat = 0, paspartuMetrekareFiyat = 0, kutuBirimFiyat = 0;
                int musteriID = MusteriListesiSatis.selectedMusteriID;


                decimal.TryParse(frmAnaSayfa.TxtBxCamFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out camMetrekareFiyat);
                decimal.TryParse(frmAnaSayfa.TxtBxPaspartuFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out paspartuMetrekareFiyat);
                decimal.TryParse(frmAnaSayfa.TxtBxKutuFiyat.Text.Replace("₺", "").Replace("%", "").Trim(), out kutuBirimFiyat);

                // 📌 **Toplam indirimi al ve geçerli bir değer olup olmadığını kontrol et**
                decimal.TryParse(TxtBxSepetIndirim.Text.Replace("₺", "").Trim(), out decimal toplamIndirim);
                toplamIndirim = Math.Max(0, toplamIndirim); // Negatif indirim önle

                // 📌 **Ödeme türünü belirle (Nakit: 1, Kart: 0)**
                int odemeTuru = radioButtonNakit.Checked ? 1 : 0;

                // 📌 **Toplam ürün adedini hesapla**
                int toplamAdet = sepetListesi.Sum(item => item.Adet);

                // 📌 **Her bir ürüne düşen indirim miktarını hesapla**
                decimal urunBasiIndirim = toplamAdet > 0 ? toplamIndirim / toplamAdet : 0;

                foreach (var item in sepetListesi)
                {
                    // 📌 **Seçili değilse fiyatları 0 yap**
                    decimal camSatisFiyat = item.CamFiyat > 0 ? camMetrekareFiyat * item.Genislik * item.Yukseklik / 10000 : 0;
                    decimal paspartuSatisFiyat = item.PaspartuFiyat > 0 ? paspartuMetrekareFiyat * item.Genislik * item.Yukseklik / 10000 : 0;
                    decimal kutuSatisFiyat = item.KutuFiyat > 0 ? kutuBirimFiyat * item.Genislik * item.Yukseklik / 10000 : 0;

                    // 📌 **Bu ürüne uygulanacak indirim (adet x birim indirim)**
                    decimal urunIndirim = item.Adet * urunBasiIndirim;

                    // 📌 **TBLCERCEVESATIS tablosuna INSERT sorgusu**
                    string sorgu = @"
        INSERT INTO TBLCERCEVESATIS (MUSTERIADSOYADID, URUNRESMIID, CERCEVEADID, CERCEVEKODID, BIRIMSATISFIYATID, 
        CAMFIYAT, PASPARTUFIYAT, KUTUFIYAT, CERCEVEEN, CERCEVEBOY, CAMSATISFIYAT, PASPARTUSATISFIYAT, KUTUSATISFIYAT,
        CERCEVESATISFIYAT, CERCEVEADET, TOPLAMADETFIYAT, GENELTOPLAM, INDIRIM, ODEMETURU, ACIKLAMA)
        VALUES (@MusteriID, @UrunResmiID, @CerceveAdID, @CerceveKodID, @BirimSatisFiyatID, @CamFiyat, @PaspartuFiyat, 
        @KutuFiyat, @Genislik, @Yukseklik, @CamSatisFiyat, @PaspartuSatisFiyat, @KutuSatisFiyat, 
        @CerceveSatisFiyat, @Adet, @ToplamFiyat, @GenelToplam, @Indirim, @OdemeTuru, @Aciklama)";

                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@MusteriID", musteriID); // 📌 **Müşteri ID eklendi**
                        komut.Parameters.AddWithValue("@UrunResmiID", item.UrunResmiBase64 ?? (object)DBNull.Value);
                        komut.Parameters.AddWithValue("@CerceveAdID", item.CerceveAdID);
                        komut.Parameters.AddWithValue("@CerceveKodID", item.CerceveKodID);
                        komut.Parameters.AddWithValue("@BirimSatisFiyatID", item.BirimSatisFiyatID);
                        komut.Parameters.AddWithValue("@CamFiyat", camMetrekareFiyat);
                        komut.Parameters.AddWithValue("@PaspartuFiyat", paspartuMetrekareFiyat);
                        komut.Parameters.AddWithValue("@KutuFiyat", kutuBirimFiyat);
                        komut.Parameters.AddWithValue("@Genislik", item.Genislik);
                        komut.Parameters.AddWithValue("@Yukseklik", item.Yukseklik);
                        komut.Parameters.AddWithValue("@CamSatisFiyat", camSatisFiyat);
                        komut.Parameters.AddWithValue("@PaspartuSatisFiyat", paspartuSatisFiyat);
                        komut.Parameters.AddWithValue("@KutuSatisFiyat", kutuSatisFiyat);
                        komut.Parameters.AddWithValue("@CerceveSatisFiyat", item.CerceveSatisFiyat);
                        komut.Parameters.AddWithValue("@Adet", item.Adet);
                        komut.Parameters.AddWithValue("@ToplamFiyat", item.ToplamFiyat);
                        komut.Parameters.AddWithValue("@GenelToplam", item.ToplamFiyat);
                        komut.Parameters.AddWithValue("@Indirim", urunIndirim); // 📌 **Her ürün için hesaplanan indirim**
                        komut.Parameters.AddWithValue("@OdemeTuru", odemeTuru); // 📌 **Nakit/Kart bilgisi eklendi**
                        komut.Parameters.AddWithValue("@Aciklama", item.Aciklama ?? (object)DBNull.Value);

                        komut.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Satış başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 📌 **Sepeti temizle**
            sepetListesi.Clear();
            flowLayoutPanelSepet.Controls.Clear();
            TxtBxSepetIndirim.Text = "₺0"; // 📌 **İndirim alanını sıfırla**
            TxtBxSepetToplamFiyat.Text = "₺0"; // 📌 **Toplam fiyatı sıfırla**
        }


        private void ToplamFiyatHesapla()
        {
            decimal toplamFiyat = sepetListesi.Sum(item => item.ToplamFiyat);  // 📌 **Tüm ürünlerin toplam fiyatını hesapla**
            TxtBxSepetToplamFiyat.Text = $"{toplamFiyat:C}";  // **₺ simgesi ile yazdır**
        }
        private void TxtBxSepetToplamFiyat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (decimal.TryParse(TxtBxSepetToplamFiyat.Text.Replace("₺", "").Trim(), out decimal manuelFiyat))
                {
                    decimal orijinalFiyat = sepetListesi.Sum(item => item.ToplamFiyat); // 📌 **Sepetteki toplam fiyat**
                    decimal mevcutIndirim = radioButtonNakit.Checked ? HesaplaNakitIndirimi(orijinalFiyat) : 0; // 📌 **Eğer nakit seçiliyse hesapla, değilse 0**
                    decimal fiyatNakitIndirimli = orijinalFiyat - mevcutIndirim; // 📌 **Nakit indirimi uygulanmış fiyat**
                    decimal manuelIndirim = fiyatNakitIndirimli - manuelFiyat; // 📌 **Manuel fark hesapla**

                    if (manuelIndirim < 0) manuelIndirim = 0; // 📌 **Negatif indirim önle**

                    if (radioButtonNakit.Checked) // 📌 **Nakit seçiliyse toplam indirimi hesapla**
                    {
                        decimal toplamIndirim = mevcutIndirim + manuelIndirim;
                        TxtBxSepetIndirim.Text = $"{toplamIndirim:C}";
                    }
                    else // 📌 **Kart seçiliyse sadece manuel fark göster**
                    {
                        TxtBxSepetIndirim.Text = $"{manuelIndirim:C}";
                    }

                    TxtBxSepetToplamFiyat.Text = $"{manuelFiyat:C}"; // 📌 **TextBox'ı yeni fiyat ile güncelle**

                    e.Handled = true;
                    e.SuppressKeyPress = true; // 📌 **Enter tuşunun başka bir işlem yapmasını engelle**
                }
            }
        }

        private void BtnSepetBosalt_Click(object sender, EventArgs e)
        {
            if (sepetListesi.Count == 0)
            {
                MessageBox.Show("Sepetiniz zaten boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Sepeti boşaltmak istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sepetListesi.Clear(); // 📌 Listeyi tamamen temizle
                flowLayoutPanelSepet.Controls.Clear(); // 📌 Arayüzü de temizle
                TxtBxSepetToplamFiyat.Text = "₺0"; // 📌 Toplam fiyatı sıfırla
                TxtBxSepetIndirim.Text = "₺0"; // 📌 İndirim değerini sıfırla

            }
        }
        public List<SepetItem> GetSepetListesi()
        {
            return sepetListesi; // 📌 Sepet içindeki ürünleri döndür
        }
        private void TxtBxMusteriAra_TextChanged(object sender, EventArgs e)
        {
            MusteriGetir(TxtBxMusteriAra.Text.Trim()); // 📌 Girilen metne göre filtreleme yap
        }
        public void MusteriGetir(string musteriAra = "")
        {
            try
            {
                MusteriSatisListePanel.Controls.Clear(); // 📌 Önce listeyi temizle

                using (SqlConnection connection = DataBaseControl.GetConnection())
                {
                    connection.Open();
                    string sorgu = @"
            SELECT 
                MUSTERIADSOYAD AS MusteriAdSoyad, 
                MUSTERIID AS MusteriId
            FROM 
                TBLMUSTERILER
            WHERE 
                (@MusteriAra = '' OR MUSTERIADSOYAD LIKE '%' + @MusteriAra + '%')
            ORDER BY 
                MUSTERIADSOYAD ASC";

                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        komut.Parameters.Add("@MusteriAra", SqlDbType.NVarChar).Value = musteriAra;

                        using (SqlDataReader oku = komut.ExecuteReader())
                        {
                            while (oku.Read())
                            {
                                MusteriListesiSatis arac = new MusteriListesiSatis
                                {
                                    LblListeSatisMusteriAd = { Text = oku["MusteriAdSoyad"].ToString() },
                                    LblListeSatisMusteriId = { Text = "ID: " + oku["MusteriId"].ToString() }
                                };

                                MusteriSatisListePanel.Controls.Add(arac);
                            }
                        }
                    }
                }

                // 📌 Eğer müşteri değiştiyse, güncelleme zamanını kaydet
                DateTime currentChangeTimeMusteri = FrmAnaMenu.GetLastMusterilerDatabaseChangeTime();
                if (currentChangeTimeMusteri > FrmAnaMenu.lastCheckedTimeMusteri)
                {
                    FrmAnaMenu.lastCheckedTimeMusteri = currentChangeTimeMusteri;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri listesi yüklenirken bir hata oluştu: " + ex.Message);
            }
        }


    }
}
