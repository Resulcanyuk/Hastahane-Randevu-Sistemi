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

namespace final_ödevi
{
    public partial class doktorsistemi : Form
    {
        public string tc;
        public doktorsistemi()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        public void randevudoldur()
        {
            baglanti.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT*FROM  Randevular  WHERE Doktor='" + label5.Text + "'", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void doktorsistemi_Load(object sender, EventArgs e)
        {
           textBox3.Enabled = false;
           richTextBox1.Enabled = false;
           lbltc.Text = tc; 
           baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
           baglanti.Open();
           OleDbCommand komut = new OleDbCommand("Select*from Doktorlar where Doktor_Tc=@p1", baglanti); 
           komut.Connection = baglanti;
           komut.Parameters.AddWithValue("@p1", lbltc.Text);
           OleDbDataReader oku = komut.ExecuteReader();
           while (oku.Read())
           {
                label5.Text = oku[1].ToString() + " " + oku[2].ToString();   
           }
            baglanti.Close();
            randevudoldur();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();     
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "update Randevular set Teşhis=@teshis,İlaç=@ilac where Hasta_tc=@tc and Sikayet=@sikayet";
            komut.Parameters.AddWithValue("@teshis", textBox1.Text);
            komut.Parameters.AddWithValue("@ilac", textBox2.Text);
            komut.Parameters.AddWithValue("@tc", textBox3.Text);
            komut.Parameters.AddWithValue("@sikayet", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();     
            randevudoldur();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut5 = new OleDbCommand("insert into Gecmis_Randevular (Hasta_ad_soyad,Hasta_tc,Brans,Doktor,Sikayet,Teşhis,İlaç) values (@adsoyad,@tcno,@brans,@doktor,@sikayet,@teshis,@ilac)", baglanti);
            komut5.Parameters.AddWithValue("@adsoyad", dataGridView1.CurrentRow.Cells[2].Value);
            komut5.Parameters.AddWithValue("@tcno", dataGridView1.CurrentRow.Cells[1].Value);
            komut5.Parameters.AddWithValue("@brans", dataGridView1.CurrentRow.Cells[3].Value);
            komut5.Parameters.AddWithValue("@doktor", dataGridView1.CurrentRow.Cells[4].Value);
            komut5.Parameters.AddWithValue("@sikayet", dataGridView1.CurrentRow.Cells[5].Value);
            komut5.Parameters.AddWithValue("@teshis", dataGridView1.CurrentRow.Cells[6].Value);
            komut5.Parameters.AddWithValue("@ilac", dataGridView1.CurrentRow.Cells[7].Value);
            baglanti.Close();
            OleDbCommand sil = new OleDbCommand();
            baglanti.Open();
            sil.Connection = baglanti;
            string komut6 = "DELETE from Randevular where id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
            sil.CommandText = komut6;
            sil.ExecuteNonQuery();
            MessageBox.Show("Başarıyla silindi"); 
            komut5.ExecuteNonQuery();
            baglanti.Close();
            randevudoldur();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 grs = new Form1();
            grs.Show();
            this.Hide();
        }
    }
}
