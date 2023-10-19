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
    public partial class transaction : Form
    {
        public transaction()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source = desktop-td8pcig\\sqlexpress2019;Initial Catalog=Myproject;Integrated Security=True");
        int Acbalance;
        private void transaction_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 rm = new Form1();
            rm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Checkbal()
        {
            conn.Open();
            string Query = "select * from useracc where Accno=" + txtcheckbal.Text + "";
            SqlCommand cmd = new SqlCommand(Query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Balanlabel.Text = "Rs."+ dr["Balance"].ToString();
                Acbalance = Convert.ToInt32(dr["Balance"].ToString());
            }
            conn.Close();
        }
        private void Checkavailbal()
        {
            //conn.Open();
            string Query = "select * from useracc where Accno=" + txtfrmacc.Text + "";
            SqlCommand cmd = new SqlCommand(Query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Balancelbl.Text = "Rs." + dr["Balance"].ToString();
                Acbalance = Convert.ToInt32(dr["Balance"].ToString());
            }
            //conn.Close();
        }
        private void Newbal(string text)
        {
            conn.Open();
            string Query = "select * from useracc where Accno=" + txtcheckbal.Text + "";
            SqlCommand cmd = new SqlCommand(Query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               // Balanlabel.Text = "Rs." + dr["Balance"].ToString();
                Acbalance = Convert.ToInt32(dr["Balance"].ToString());
            }
            conn.Close();
        }
        private void Balancebtn_Click(object sender, EventArgs e)
        {
            if(txtcheckbal.Text=="")
            {
                MessageBox.Show("Pls enter the account no.");
            }
            else
            {
                Checkbal();
                if(Balanlabel.Text=="USER BALANCE")
                {
                    MessageBox.Show("Account not found");
                    txtcheckbal.Text = "";
                }
            }
        }
        private void Deposit()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Transactiontbl values(@TrName, @TrDate,@TrAmt, @TrAccno)",conn);
                cmd.Parameters.AddWithValue("@TrName", "Deposit");
                cmd.Parameters.AddWithValue("@TrDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TrAmt", txtdepamt.Text);
                cmd.Parameters.AddWithValue("@TrAccno", txtdepaccno.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                conn.Close();
            }
        }

        private void Withdraw()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Transactiontbl values(@TrName, @TrDate,@TrAmt, @TrAccno)", conn);
                cmd.Parameters.AddWithValue("@TrName", "Withdrawal");
                cmd.Parameters.AddWithValue("@TrDate", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TrAmt", txtdepamt.Text);
                cmd.Parameters.AddWithValue("@TrAccno", txtdepaccno.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                conn.Close();
            }
        }

        private void Incrementbal()
        {
            Newbal(txttoacc.Text);
            int newbalance = Acbalance + Convert.ToInt16(txttrsamt.Text);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update useracc set Balance=@Balance where Accno=@AccKey", conn);
                cmd.Parameters.AddWithValue("@Balance", newbalance);
                cmd.Parameters.AddWithValue("@Acckey", txttoacc.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
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

     

        private void Decrementbal()
        {
            Newbal(txtfrmacc.Text);
            int newbalance = Acbalance - Convert.ToInt32(txttrsamt.Text);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update useracc set Balance=@Balance where Accno=@AccKey", conn);
                cmd.Parameters.AddWithValue("@Balance", newbalance);
                cmd.Parameters.AddWithValue("@Acckey", txtfrmacc.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
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
        private void depositbtn_Click(object sender, EventArgs e)
        {
            if (txtdepaccno.Text == "" || txtdepamt.Text == "")
            {
                MessageBox.Show("Data is Missing");
            }
            else
            {
                Deposit();
                Newbal(txtdepamt.Text);
                int newbalance = Acbalance + Convert.ToInt32(txtdepamt.Text);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update useracc set Balance=@Balance where Accno=@AccKey", conn);
                    cmd.Parameters.AddWithValue("@Balance", newbalance);
                    cmd.Parameters.AddWithValue("@Acckey", txtdepaccno.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Amount Deposited!!");
                    conn.Close();
                    txtdepamt.Text = "";
                    txtdepaccno.Text = "";
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
        }

        private void withdrawbtn_Click(object sender, EventArgs e)
        {
            if (txtwithAccno.Text == "" || txtwithamt.Text == "")
            {
                MessageBox.Show("Data is Missing");
            }
            else
            {
                Newbal(txtwithamt.Text);
                Withdraw();
                if (Acbalance < Convert.ToInt32(txtwithamt.Text))
                {
                    MessageBox.Show("Insufficient Balance for Withdrawal!!");
                }
                else
                {
                    int newbalance = Acbalance - Convert.ToInt32(txtwithamt.Text);
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("update useracc set Balance=@Balance where Accno=@AccKey", conn);
                        cmd.Parameters.AddWithValue("@Balance", newbalance);
                        cmd.Parameters.AddWithValue("@Acckey", txtwithAccno.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Amount Withdrawn!!");
                        conn.Close();
                        txtwithamt.Text = "";
                        txtwithAccno.Text = "";
                        
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
            }
        }

        private void Balanlabel_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(txtfrmacc.Text=="")
            {
                MessageBox.Show("Enter the account no");
            }
            else
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count (*) from useracc where Accno = '"+txtfrmacc.Text+"'",conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows[0][0].ToString()=="1")
                {
                    Checkavailbal();
                    conn.Close();


                }
                else
                {
                    MessageBox.Show("Account does not exist");
                    txtfrmacc.Text = "";
                }
                conn.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txttoacc.Text == "")
            {
                MessageBox.Show("Enter the account no");
            }
            else
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count (*) from useracc where Accno = '" + txttoacc.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    //Checkavailbal();
                    MessageBox.Show("Account no valid");
                    conn.Close();

                    if (txtfrmacc.Text==txttoacc.Text)
                    {
                        MessageBox.Show("Both Account nos are same. Pls check!!");
                        txttoacc.Text = " ";
                        
                    }


                }
                else
                {
                    MessageBox.Show("Account does not exist");
                    txttoacc.Text = "";
                }
                conn.Close();
            }

            }

        private void AmtTransfer()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Transfer values(@TranSrc, @TranDest,@TranAmt, @Trandate)", conn);
                cmd.Parameters.AddWithValue("@TranSrc", txtfrmacc.Text);
                cmd.Parameters.AddWithValue("@TranDest", txttoacc.Text);
                cmd.Parameters.AddWithValue("@TranAmt", txttrsamt.Text);
                cmd.Parameters.AddWithValue("@Trandate", DateTime.Now.Date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Amount transferred!!");
                conn.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                conn.Close();
            }
        }
        private void transferbtn_Click(object sender, EventArgs e)
        {
            if(txttoacc.Text=="" || txtfrmacc.Text=="" || txttrsamt.Text=="")
            {
                MessageBox.Show("Data is missing. Pls Check!!");
                }
            else if(Convert.ToInt16(txttrsamt.Text)> Acbalance)
            {
                MessageBox.Show("Insufficient Balance");
            }
            else
            {
                AmtTransfer();
                Incrementbal();
                Decrementbal();
                txtfrmacc.Text = "";
                txttoacc.Text = "";
                txttrsamt.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcheckbal.Text = "";
            txtdepaccno.Text = "";
            txtdepamt.Text = "";
            txtwithAccno.Text = "";
            txtwithamt.Text = "";
            txtfrmacc.Text = "";
            txttoacc.Text = "";
            txttrsamt.Text = "";
            Balanlabel.Text = "";
        }
    }
}
