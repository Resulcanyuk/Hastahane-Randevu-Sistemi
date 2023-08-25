using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Printing;

namespace final_ödevi
{
    public partial class hastarandevu : Form
    {
        public hastarandevu()
        {
            InitializeComponent();
        }
        public string tc;
        OleDbConnection baglanti= new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
        void doldur()
        {
            baglanti.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT*FROM  Randevular  WHERE Hasta_tc='" + lbltc.Text + "'", baglanti);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();
        }
        void gecmisdoldur()
        {
            baglanti.Open();
            DataTable dt2 = new DataTable();
            OleDbDataAdapter da2 = new OleDbDataAdapter("SELECT*FROM  Gecmis_Randevular  WHERE Hasta_tc='" + lbltc.Text + "'", baglanti);
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            baglanti.Close();
        }
        void combosaat()
        {
            baglanti.Open();
            OleDbCommand komutsaat = new OleDbCommand("Select Saatler from Saatler", baglanti);
            OleDbDataReader okusaat = komutsaat.ExecuteReader();
            while (okusaat.Read())
            {
                comboBox3.Items.Add(okusaat[0]);
            }
            baglanti.Close();
        }
        private void hastarandevu_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc; 
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select*from hastalar where tc=@p1", baglanti); 
            komut.Connection = baglanti;
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                label5.Text = oku[1].ToString()+" "+oku[2].ToString();
            }
            baglanti.Close();
            baglanti.Open();
            OleDbCommand komut2 = new OleDbCommand("Select Brans_adi from Branslar", baglanti);
            OleDbDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                comboBox1.Items.Add(oku2[0]);
            }
            baglanti.Close();
            baglanti.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Hasta_tc,Hasta_ad_soyad,Brans,Doktor,Sikayet,Tarih,Saat FROM  Randevular  WHERE Hasta_tc='" + lbltc.Text + "'",baglanti);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();
            gecmisdoldur();
            combosaat();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut3 = new OleDbCommand("Select Doktor_adi,Doktor_soyadi from Doktorlar where Doktor_brans=@brans", baglanti);
            komut3.Parameters.AddWithValue("@brans", comboBox1.Text);
            OleDbDataReader oku3 = komut3.ExecuteReader();
            while (oku3.Read())
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add(oku3[0] + " " + oku3[1]);
            }
            comboBox2.Text = "";
            baglanti.Close(); 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string selectedDate = dateTimePicker1.Text;
            string selectedTime = comboBox3.Text;
            string selectedDoctor = comboBox2.Text;
            baglanti.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Hasta_tc,Hasta_ad_soyad,Brans,Doktor,Sikayet,Tarih,Saat FROM  Randevular", baglanti);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            baglanti.Close();
            dataGridView2.DataSource = dt;
            DataRow[] results = dt.Select(string.Format("Tarih='{0}' AND Saat='{1}' AND Doktor='{2}'", selectedDate, selectedTime, selectedDoctor));
            if (results.Length > 0 || (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || richTextBox1.Text == "" || comboBox3.Text == ""))
            {
                baglanti.Open();
                DataTable dt2 = new DataTable();
                OleDbDataAdapter da2 = new OleDbDataAdapter("SELECT*FROM  Randevular  WHERE Hasta_tc='" + lbltc.Text + "'", baglanti);
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;
                baglanti.Close();
                MessageBox.Show("Seçtiğiniz tarih ve saate ait bir randevu veya randevu ile ilgili tüm bölümleri doldurmadınız lütfen doldurun yada tarihi değiştiriniz");
            }
            else
            {
                baglanti.Open();
                OleDbCommand komut4 = new OleDbCommand("insert into Randevular (Hasta_ad_soyad,Hasta_tc,Brans,Doktor,Sikayet,Tarih,Saat) values (@adsoyad,@tcno,@brans,@doktor,@sikayet,@Tarih,@Saat)", baglanti);
                komut4.Parameters.AddWithValue("@adsoyad", label5.Text);
                komut4.Parameters.AddWithValue("@tcno", lbltc.Text);
                komut4.Parameters.AddWithValue("@brans", comboBox1.Text);
                komut4.Parameters.AddWithValue("@doktor", comboBox2.Text);
                komut4.Parameters.AddWithValue("@sikayet", richTextBox1.Text);
                komut4.Parameters.AddWithValue("@Tarih", dateTimePicker1.Text);
                komut4.Parameters.AddWithValue("@Saat", comboBox3.Text);
                komut4.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show(label5.Text + " " + comboBox1.Text + "Bölümünden" + comboBox2.Text + " adlı doktora randevunuz başarıyla alınmıştır");
                doldur();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 grs = new Form1();
            grs.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bir veri seçilmedi ve ya geçmiş randevularım tablosunda veri yok");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bitmap = Properties.Resources.ust;
            Image image = new Bitmap(bitmap);
            e.Graphics.DrawImage(image, 25, 25, 750, 150);
            string çizgi = "_________________________________________________";
            e.Graphics.DrawString("HASTA" , new Font("Constantia", 20, FontStyle.Regular), Brushes.Black, new Point(375, 170));
            e.Graphics.DrawString(çizgi, new Font("Constantia", 20, FontStyle.Regular), Brushes.Orange, new Point(50, 190));
            e.Graphics.DrawString("Tc No : " + dataGridView1.CurrentRow.Cells[1].Value.ToString(), new Font("Constantia", 11, FontStyle.Regular), Brushes.Black, new Point(50, 230));
            e.Graphics.DrawString("Hasta Adı Soyadı :" + dataGridView1.CurrentRow.Cells[2].Value.ToString(), new Font("Comic Sans Serif", 11, FontStyle.Regular), Brushes.Black, new Point(50, 260));
            e.Graphics.DrawString("DOKTOR", new Font("Constantia", 20, FontStyle.Regular), Brushes.Black, new Point(375, 290));
            e.Graphics.DrawString(çizgi, new Font("Constantia", 20, FontStyle.Regular), Brushes.Orange, new Point(50, 310));
            e.Graphics.DrawString("Randevu Aldığı Bölüm : " + dataGridView1.CurrentRow.Cells[3].Value.ToString(), new Font("Comic Sans Serif", 11, FontStyle.Regular), Brushes.Black, new Point(50, 345));
            e.Graphics.DrawString("Randevu Aldığı Doktor : " + dataGridView1.CurrentRow.Cells[4].Value.ToString(), new Font("Comic Sans Serif", 11, FontStyle.Regular), Brushes.Black, new Point(50, 375));
            e.Graphics.DrawString("Hasta Şikayeti : " + dataGridView1.CurrentRow.Cells[5].Value.ToString(), new Font("Comic Sans Serif", 11, FontStyle.Regular), Brushes.Black, new Point(50, 405));
            e.Graphics.DrawString("Eczane", new Font("Constantia", 20, FontStyle.Regular), Brushes.Black, new Point(375, 430));
            e.Graphics.DrawString(çizgi, new Font("Constantia", 20, FontStyle.Regular), Brushes.Orange, new Point(50, 455));
            e.Graphics.DrawString("Doktor Teşhisi : " + dataGridView1.CurrentRow.Cells[6].Value.ToString(), new Font("Comic Sans Serif", 11, FontStyle.Regular), Brushes.Black, new Point(50, 490));
            e.Graphics.DrawString("Doktor İlacı : " + dataGridView1.CurrentRow.Cells[7].Value.ToString(), new Font("Comic Sans Serif", 11, FontStyle.Regular), Brushes.Black, new Point(50, 525));
            e.Graphics.DrawString("Doktor İmzası" , new Font("Comic Sans Serif", 13, FontStyle.Regular), Brushes.Black, new Point(650, 700));
            e.Graphics.DrawString("_________________", new Font("Comic Sans Serif", 13, FontStyle.Regular), Brushes.Black, new Point(620, 740));
            e.Graphics.DrawString("Çıktı Alınan Tarih: " + DateTime.Now, new Font("Comic Sans Serif", 13, FontStyle.Regular), Brushes.Black, new Point(450, 650));e.Graphics.DrawString("SAĞLIKLI GÜNLER DİLERİZ", new Font("Constantia", 20, FontStyle.Regular), Brushes.Black, new Point(200, 900));
            Bitmap bitmap2 = Properties.Resources.alt;
            Image image2 = new Bitmap(bitmap2);
            e.Graphics.DrawImage(image2, 200, 950, 300, 200);
        }
    }
}
