using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_ödevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            hastagiris grs = new hastagiris();
            grs.Show();
            this.Hide();
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            sistemgiris grs = new sistemgiris();
            grs.Show();
            this.Hide();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            doktorgiris grs = new doktorgiris();
            grs.Show();
            this.Hide();
        }
    }
}
