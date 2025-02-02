using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhsapSanatEvi.Models
{
    public class SepetItem
    {
        public int MusteriID { get; set; }
        public int UrunResmiID { get; set; }
        public int CerceveAdID { get; set; }
        public int CerceveKodID { get; set; }
        public int BirimSatisFiyatID { get; set; }
        public decimal CamFiyat { get; set; }
        public decimal PaspartuFiyat { get; set; }
        public decimal KutuFiyat { get; set; }
        public decimal Genislik { get; set; }
        public decimal Yukseklik { get; set; }
        public decimal CerceveSatisFiyat { get; set; }
        public int Adet { get; set; }
        public decimal ToplamFiyat { get; set; }
        public decimal Indirim { get; set; }
        public int OdemeTuru { get; set; }
        public string Aciklama { get; set; }
        public string UrunResmiBase64 { get; set; }

        // 📌 Yeni eklenen alanlar
        public string CerceveKodAdi { get; set; }  // ID yerine gösterilecek ad
        public string FirmaAdi { get; set; }       // Firma adı
        public string MusteriAdi { get; set; } // **Müşteri adını tutacak alan**
    }

}
