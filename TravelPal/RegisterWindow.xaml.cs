using System;
using System.Windows;
using TravelPal.Enums;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager _userManager;
        private TravelManager _travelManager;

        public RegisterWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();

            PopulateCbCountries();
        }

        // ******************** EVENTS *********************
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtRegisterUserName.Text.Trim().Length == 0 || pbRegisterPassword.Password.Length == 0 || string.IsNullOrEmpty(cbCountries.Text))
            {
                MessageBox.Show("Please provide all required inputs (user name, password, and country).", "Warning!");
            }
            else if (txtRegisterUserName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a username with at least three characters.", "Warning!");
            }
            else if (pbRegisterPassword.Password.Length < 5)
            {
                MessageBox.Show("Please enter a password with at least five characters.", "Warning!");
            }
            else
            {
                Countries location = (Countries)Enum.Parse(typeof(Countries), cbCountries.SelectedItem.ToString().Replace(' ', '_'));
                User user = new(txtRegisterUserName.Text.ToString(), pbRegisterPassword.Password.ToString(), location);

                if (!_userManager.AddUser(user))
                {
                    MessageBox.Show("The user name is not available. Please choose a different user name.", "Warning!");
                }
                else
                {
                    MessageBox.Show("You have been successfully registered!");

                    MainWindow mainWindow = new(_userManager, _travelManager);
                    mainWindow.Show();

                    Close();
                }
            }
        }

        private void btnCancelRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();

            Close();
        }

        // ******************** METHODS ********************
        private void PopulateCbCountries()
        {
            string[] countries = Enum.GetNames(typeof(Countries));

            foreach (string country in countries)
            {
                cbCountries.Items.Add(country.Replace('_', ' '));
            }
        }
    }
}
