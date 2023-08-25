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
    public partial class hastakayit : Form
    {
        public hastakayit()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
            baglanti.Open();
            string sql = "SELECT COUNT(*) FROM Hastalar WHERE tc = @tc";
            OleDbCommand command = new OleDbCommand(sql, baglanti);
            command.Parameters.AddWithValue("@tc", maskedTextBox1.Text);
            int count = (int)command.ExecuteScalar();  
            if (count > 0)
            {
                MessageBox.Show("Bu TC kimlik numarasına sahip bir kullanıcı zaten var!");
            }     
            else
            {     
                if ((textBox1.Text != "" && textBox2.Text != "" && maskedTextBox1.Text != "" && maskedTextBox2.Text != "" && textBox3.Text != "") && (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1))
                {
                    OleDbCommand komut = new OleDbCommand("insert into Hastalar (ad,soyad,tc,telefon,sifre,cinsiyet) values (@ad,@soyad,@tcno,@telefon,@sifre,@cinsiyet)", baglanti);
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@tcno", maskedTextBox1.Text);
                    komut.Parameters.AddWithValue("@telefon", maskedTextBox2.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox3.Text);
                    komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kaydiniz Gerceklesmistir Sifreniz : " + textBox3.Text, "Bilgi");
                    hastagiris grs = new hastagiris();
                    grs.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Lütfen Boş Alan Bırakmayınız");
                }
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 grs = new Form1();
            grs.Show();
            this.Hide();
        }
    }
}
