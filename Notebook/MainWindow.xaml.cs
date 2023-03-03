using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.IO.Enumeration;
using System.Windows;
using System.Windows.Controls;

namespace Notebook
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetUsername();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mafte\Documents\notes.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetUsername()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(" select username from usersTable where username='" + usernameLabel.Content + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            usernameLabel.Content = "Hello, " + dt.Rows[0][0].ToString();
            con.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = noteTitle.Text;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save note...";
            saveFileDialog.FileName = fileName; 
            saveFileDialog.DefaultExt = ".txt"; 
            saveFileDialog.Filter = "Text Files (.xml)|*.xml";

            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, userNote.Text);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            noteTitle.Clear();
            userNote.Clear();
        }

        private void showAllNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (.txt)|*.txt";
            openFileDialog.Title = "Open a note...";

            if (openFileDialog.ShowDialog() == true) 
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName);
                noteTitle.Text = openFileDialog.FileName.ToString();
                userNote.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}