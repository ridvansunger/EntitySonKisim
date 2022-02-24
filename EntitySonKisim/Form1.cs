using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitySonKisim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            ////Şehire göre gruplandırma yaptık
            //var degerler = db.TBLOGRENCİ.OrderBy(x => x.Sehir).GroupBy(y => y.Sehir).Select(z => new
            //{
            //    Sehir = z.Key,
            //    Toplam = z.Count()
            //});
            //dataGridView1.DataSource = degerler.ToList();


            //şehire göre sırlayıp sehir sayısı büyük olaı başa aldık.
            var degerler = db.TBLOGRENCİ.OrderBy(x => x.Sehir).GroupBy(y => y.Sehir).Select(z => new
            {
                Sehir = z.Key,
                Toplam = z.Count()
            }).OrderByDescending(z => z.Toplam);
            dataGridView1.DataSource = degerler.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //en yüksel not ortalamsı
            label1.Text = db.TBLNOTLARs.Max(x => x.ORTALAMA).ToString();

            //minimum sınav noru
            label2.Text = db.TBLNOTLARs.Min(x => x.SINAV).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //70 den küçük en büyük ortalamas
            //kalan öğrenci içinde en yüksek ortalama öğrencinin not ortalaması
            label3.Text = db.TBLNOTLARs.Where(x => x.DURUM == false).Max(y => y.ORTALAMA).ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //urun sayısı
            //label1.Text = db.TBLURUNs.Count().ToString();

            //stoktaki toplam stok fiyatları
            //label1.Text = db.TBLURUNs.Sum(x => x.Stok).ToString();

            //sadece buzdolaplarının sayısı
            //label1.Text = db.TBLURUNs.Count(x => x.Ad=="BUZDOLABI").ToString();

            //Ortalama bir ürün fiyatı
            //label1.Text = db.TBLURUNs.Average(x => x.Fiyat).ToString();

            //Bir buzdolabı ortalama kaç tl ye satılıyor.
            //label1.Text = db.TBLURUNs.Where(a=>a.Ad=="BUZDOLABI").Average(x => x.Fiyat).ToString();


            //en fazla stoğu bulunan ürünün adı
            //label1.Text = (from x in db.TBLURUNs
            //               orderby x.Stok descending
            //               select x.Ad
            //               ).First();



            //prosedürü çalıştırma önce databasei update edip prosedürü ekledik
            dataGridView1.DataSource = db.Sp_Ogrenci().ToList();
            


        }
    }
}
