using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Astro_Bank_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            useracc sj = new useracc();
            sj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            transaction my = new transaction();
            my.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            customer pj = new customer();
            pj.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            manageacc kt = new manageacc();
            kt.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
