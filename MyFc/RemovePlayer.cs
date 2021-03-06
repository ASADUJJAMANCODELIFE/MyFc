using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFc
{
    public partial class RemovePlayer : Form
    {
        public RemovePlayer()
        {
            InitializeComponent();
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Maximisebutton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            else this.WindowState = FormWindowState.Maximized;
        }

        private void Minimisebutton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Backbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private Image GetPhoto(byte[] value)
        {
            MemoryStream memoryStream = new MemoryStream(value);
            return Image.FromStream(memoryStream);
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection.Open();

            string sql = "delete from players where playerId = '" + PlayerIdtextBox.Text + "'";
            SqlCommand command = new SqlCommand(sql, connection);
            int flag = command.ExecuteNonQuery();

            connection.Close();

            SqlConnection connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection1.Open();

            string sql1 = "delete from active where playerId = '" + PlayerIdtextBox.Text + "'";
            SqlCommand command1 = new SqlCommand(sql1, connection1);
            int flag1 = command1.ExecuteNonQuery();

            SqlConnection connection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection2.Open();

            string sql2 = "delete from injuries where playerId = '" + PlayerIdtextBox.Text + "'";
            SqlCommand command2 = new SqlCommand(sql2, connection2);
            int flag2 = command2.ExecuteNonQuery();

            if (flag == 0 && flag1 == 0 && flag2 == 0)
            {
                MessageBox.Show("Player Not Removed!", "ERROR");
                connection1.Close();
            }
            else
            {
                MessageBox.Show("Player Rmoved", "SUCCESSFUL");
                this.Hide();
                connection1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            connection.Open();

            string sql = "Select * from PLAYERS where playerId = '" + PlayerIdtextBox.Text + "'";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                NametextBox.Text = reader["name"].ToString();
                DateOfBirthdateTimePicker.Text = reader["dateOfBirth"].ToString();
                RemovePlayerpictureBox.Image = GetPhoto((byte[])reader["photo"]);
                HeighttextBox.Text = reader["height"].ToString();
                PreferredFootcomboBox.Text = reader["foot"].ToString();
                WagetextBox.Text = reader["wage"].ToString();
                CurrencycomboBox1.Text = reader["wageCurrency"].ToString();
                PricetextBox.Text = reader["price"].ToString();
                CurrencycomboBox2.Text = reader["priceCurrency"].ToString();
                PositioncomboBox.Text = reader["position"].ToString();
            }

            else
            {
                NametextBox.Text = DateOfBirthdateTimePicker.Text = HeighttextBox.Text = PreferredFootcomboBox.Text = WagetextBox.Text = CurrencycomboBox1.Text = PricetextBox.Text = CurrencycomboBox2.Text = PositioncomboBox.Text = "";
                MessageBox.Show("Player Not Found!", "ERROR");
            }

            connection.Close();
        }
    }
}
