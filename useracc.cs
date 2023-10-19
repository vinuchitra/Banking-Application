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
    public partial class useracc: Form
    {
        public useracc()
        {
            InitializeComponent();
                DisplayAccounts();
        }
        SqlConnection conn = new SqlConnection("Data Source = desktop-td8pcig\\sqlexpress2019;Initial Catalog=Myproject;Integrated Security=True");

        private void useracc_Load(object sender, EventArgs e)
        {

        }
        private void DisplayAccounts()
        {
            conn.Open();
            string Query = "Select * from useracc";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into useracc values(@Firstname, @Lastname, @Dob, @Gender, @homeaddress, @City, @Pincode, @Nominee, @Balance, @Phoneno)", conn);
            cmd.Parameters.AddWithValue("@Firstname", txtfname.Text.Trim());
            cmd.Parameters.AddWithValue("@Lastname", txtlname.Text.Trim());
            cmd.Parameters.AddWithValue("@Dob", txtdob.Text.Trim());
            cmd.Parameters.AddWithValue("Gender", gencb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@homeaddress", txtaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@City", txtcity.Text.Trim());
            cmd.Parameters.AddWithValue("@Pincode", int.Parse(txtpincode.Text));
            cmd.Parameters.AddWithValue("@Nominee", txtnominee.Text.Trim());
            cmd.Parameters.AddWithValue("@Balance", int.Parse(txtbalance.Text));
            cmd.Parameters.AddWithValue("@Phoneno", long.Parse(txtphoneno.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Accounted created Successfully");
            conn.Close();
            DisplayAccounts();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 rm = new Form1();
            rm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtfname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtlname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtdob.Text = dataGridView1.SelectedRows[0].Cells[3].Style.Format;
            gencb.SelectedItem= dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtaddress.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtcity.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            txtpincode.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            txtnominee.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            txtbalance.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            txtphoneno.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            if (txtfname.Text == " ")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());


            }
        }

            private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update useracc set Firstname=@Firstname, Lastname=@Lastname,Dob=@Dob, Gender=@Gender, homeaddress=@homeaddress, City=@City, Pincode=@Pincode, Nominee=@Nominee, Balance=@Balance, Phoneno=@Phoneno where Accno=@AccKey", conn);
            cmd.Parameters.AddWithValue("@Firstname", txtfname.Text.Trim());
            cmd.Parameters.AddWithValue("@Lastname", txtlname.Text.Trim());
            cmd.Parameters.AddWithValue("@Dob", txtdob.Text.ToString());
            cmd.Parameters.AddWithValue("@Gender", gencb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@homeaddress", txtaddress.Text.Trim());
            cmd.Parameters.AddWithValue("@City", txtcity.Text.Trim());
            cmd.Parameters.AddWithValue("@Pincode", int.Parse(txtpincode.Text));
            cmd.Parameters.AddWithValue("@Nominee", txtnominee.Text.Trim());
            cmd.Parameters.AddWithValue("@Balance", int.Parse(txtbalance.Text));
            cmd.Parameters.AddWithValue("@Phoneno", long.Parse(txtphoneno.Text));
            cmd.Parameters.AddWithValue("@Acckey", Key);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated Successfully");
            conn.Close();
            DisplayAccounts();
        }

        private void button3_Click(object sender, EventArgs e)
            {
                if (Key == 0)
                {
                    MessageBox.Show("Select the Account");

                }
                else
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("delete from useracc where Accno=@ACKey", conn);
                        cmd.Parameters.AddWithValue("@ACKey", Key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account Deleted");
                        conn.Close();
                        DisplayAccounts();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            
        }
    }
}
