using System;
using System.Data.SqlClient;
using System.Windows;

namespace Notebook
{
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mafte\Documents\notes.mdf;Integrated Security=True;Connect Timeout=30");

        private void createNewAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into usersTable values('" + username.Text + "', '" + password.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account created successfully");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally 
                {
                    Login log = new Login();
                    log.Show();
                    this.Hide();
                }
            }
        }
    }
}