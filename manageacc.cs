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
    public partial class manageacc : Form
    {
        public manageacc()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data source=desktop-td8pcig\\sqlexpress2019;Initial Catalog=Myproject;Integrated security=True");

        private void manageacc_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            astrotrans rm = new Form1();
            rm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
      
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into astrologin values (@Userid, @Username, @Password)", conn);
                cmd.Parameters.AddWithValue("@Userid", textBox3.Text);
                cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Saved Successfully");
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

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from astrologin", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
