using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Windows;
using System.Windows.Markup;

namespace Notebook
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mafte\Documents\notes.mdf;Integrated Security=True;Connect Timeout=30");

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from usersTable where username='" + username.Text + "' and password='" + password.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Hide();
                con.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username/password");
            }

            con.Close();
        }

        private void newAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
            this.Hide();
        }
    }
}