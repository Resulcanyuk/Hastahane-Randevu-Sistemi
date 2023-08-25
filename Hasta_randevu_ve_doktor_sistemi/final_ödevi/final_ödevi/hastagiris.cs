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
    public partial class hastagiris : Form
    {
        public hastagiris()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        OleDbCommand komut;
        OleDbDataReader oku;
        private void lnkkayıt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hastakayit grs = new hastakayit();
            grs.Show();
            this.Hide();
        }
        private void btngiris_Click(object sender, EventArgs e)
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM hastalar where tc='" +msktc.Text +  "' AND sifre='" +  textBox1.Text+   "'";
            oku = komut.ExecuteReader();
            if (oku.Read())
            {
                hastarandevu grs = new hastarandevu();
                grs.tc = msktc.Text;  
                grs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC ya da Şifre yanlış");
            }
            baglanti.Close(); 
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
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
