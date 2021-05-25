using FinalFantasy7ItemManager.DataSet1TableAdapters;
using System.Data;
using System.Linq;
using System.Windows;

namespace FinalFantasy7ItemManager
{
    /// <summary>
    /// Meant to allow people to login if they exist in the database
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string username;
        private string password;
        public string Username
        {
            get { return username; }
            set => username = value;
        }
        public string Password
        {
            get { return password; }
            set => password = value;
        }
        private readonly DataSet1 DataSet = new();
        public UsersTableAdapter userTableAdapter = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userTableAdapter.Fill(DataSet.Users);

        }
        /// <summary>
        /// specifically for logging and if the user doesn't exist give an error and try again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            var query = from user in DataSet.Users
                        where (user.Name == UsernameInput.Text)
                        where (user.Password == PasswordInput.Text)
                        select user;
            if (query.Any())
            {
                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("User does not exist!");
                UsernameInput.Clear();
                PasswordInput.Clear();
            }
        }
        /// <summary>
        /// add users to database if you need a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSet1.UsersRow row = (DataSet1.UsersRow)DataSet.Users.NewRow();
            row.Name = UsernameInput.Text;
            row.Password = PasswordInput.Text;
            DataSet.Users.AddUsersRow(row);
            userTableAdapter.Update(DataSet);
            MessageBox.Show("User has been successfully registered!");
            UsernameInput.Clear();
            PasswordInput.Clear();
        }
    }
}
