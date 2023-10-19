using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Astro_Bank_App
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();


        }
        SqlConnection conn = new SqlConnection("Data source=desktop-td8pcig\\sqlexpress2019;Initial Catalog=Myproject;Integrated security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            String Username, Password;
            Username = textBox1.Text;
            Password = textBox2.Text;
            try
            {
                String query = " select * from astrologin where Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Username = textBox1.Text;
                    Password = textBox1.Text;
                    Form1 rm = new Form1();
                    rm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
