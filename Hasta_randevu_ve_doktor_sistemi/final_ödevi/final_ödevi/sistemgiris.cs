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
    public partial class sistemgiris : Form
    {
        public sistemgiris()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=veritabani.accdb");
        OleDbCommand komut1;
        OleDbDataReader oku1;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskedTextBox1.UseSystemPasswordChar =false;
            }
            else
            {
                maskedTextBox1.UseSystemPasswordChar = true;
            }
        }
        private void btngiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
             komut1 = new OleDbCommand();
             komut1.Connection = baglanti;
             komut1.CommandText = "SELECT * FROM hastahanegiris where tc='" + msktc.Text + "' AND sifre='" + maskedTextBox1.Text + "'";
             oku1 = komut1.ExecuteReader();
             if (oku1.Read())
             {
                 hastanesistemgiris grss = new hastanesistemgiris();
                 grss.Show();
                 this.Hide();
             }
             else
             {
                 MessageBox.Show("TC ya da Şifre yanlış");
             }
             baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 grs = new Form1();
            grs.Show();
            this.Hide();
        }
    }
}
