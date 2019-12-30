using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank_Dome
{
    public partial class begin : Form
    {
        public begin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(1);
            f.ShowDialog(this);
            this.Close();
        }

        private void level1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(1);
            f.ShowDialog(this);
            this.Close();
        }

        private void level2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(2);
            f.ShowDialog(this);
            this.Close();
        }

        private void level3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1(3);
            f.ShowDialog(this);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
