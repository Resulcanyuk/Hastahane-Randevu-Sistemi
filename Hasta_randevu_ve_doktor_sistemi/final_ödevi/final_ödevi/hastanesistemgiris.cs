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
    public partial class hastanesistemgiris : Form
    {
        public hastanesistemgiris()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
        void doktorlistele()
        {
            baglanti.Open();
            DataTable dt1 = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter("Select * from doktorlar", baglanti);
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            baglanti.Close();
        }
        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            maskedTextBox1.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
        }
        private void hastanesistemgiris_Load(object sender, EventArgs e)
        {
            doktorlistele();

            baglanti.Open();
            OleDbCommand bransal = new OleDbCommand("Select Brans_adi from Branslar", baglanti);
            OleDbDataReader bransoku = bransal.ExecuteReader();
            while (bransoku.Read())
            {
                comboBox1.Items.Add(bransoku[0]);
            }
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=""&&textBox2.Text!=""&&textBox3.Text!=""&&maskedTextBox1.Text!=""&&comboBox1.SelectedIndex!=-1)
            {
                baglanti.Open();
                OleDbCommand doktorekle = new OleDbCommand("insert into Doktorlar (Doktor_Tc,Doktor_adi,Doktor_soyadi,Doktor_brans,Sifre) values (@tc,@ad,@soyad,@brans,@sifre)", baglanti);
                doktorekle.Parameters.AddWithValue("@tc", maskedTextBox1.Text);
                doktorekle.Parameters.AddWithValue("@ad", textBox1.Text);
                doktorekle.Parameters.AddWithValue("@soyad", textBox2.Text);
                doktorekle.Parameters.AddWithValue("@brans", comboBox1.Text);
                doktorekle.Parameters.AddWithValue("@sifre", textBox3.Text);
                doktorekle.ExecuteNonQuery();
                baglanti.Close();
                doktorlistele();
                MessageBox.Show("Doktor Başarıyla Eklendi");
                temizle();
               
            }
            else
            {
                MessageBox.Show("Lütfen Doktor ekleme ile ilgili tüm alanları doldurunuz");
            }        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && maskedTextBox1.Text != "" && comboBox1.SelectedIndex != -1)
            {
                OleDbCommand sil = new OleDbCommand();
                baglanti.Open();
                sil.Connection = baglanti;
                string komut1 = "DELETE from Doktorlar where id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sil.CommandText = komut1;
                sil.ExecuteNonQuery();
                MessageBox.Show("Başarıyla silindi");
                baglanti.Close();
                doktorlistele();   
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçiniz");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && maskedTextBox1.Text != "" && comboBox1.SelectedIndex != -1)
            {
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = baglanti;
                komut.CommandText = "update Doktorlar set Doktor_adi=@ad,Doktor_soyadi=@soyad,Doktor_brans=@brans,Sifre=@sifre where Doktor_Tc=@tc";
                komut.Parameters.AddWithValue("@ad", textBox1.Text);
                komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                komut.Parameters.AddWithValue("@brans", comboBox1.Text);
                komut.Parameters.AddWithValue("@sifre", textBox3.Text);
                komut.Parameters.AddWithValue("@tc", maskedTextBox1.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                temizle();
                doktorlistele();    
            }
            else
            {
                MessageBox.Show("Lütfen Güncellemek İçin İstediğiniz kişiyi seçiniz.");
            }
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 grs = new Form1();
            grs.Show();
            this.Hide();
        }
    }
}
