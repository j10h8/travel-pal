using System;
using System.Windows;
using System.Windows.Media;
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

        // Constructor to initialize RegisterWindow. Sets field variables and calls methods PopulateCbCountries and InitializeComponent. 
        public RegisterWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();

            PopulateCbCountries();
        }

        // Checks input and initializes and adds new user if input is OK 
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtRegisterUserName.Text.Trim().Length == 0 || pbRegisterPassword.Password.Trim().Length == 0 || string.IsNullOrEmpty(cbCountries.Text) || pbConfirmPassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("Please provide all required inputs (user name, country, and password).");
            }
            else if (txtRegisterUserName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a username with at least three characters.");
            }
            else if (pbRegisterPassword.Password.Trim().Length < 5)
            {
                MessageBox.Show("Please enter a password with at least five characters.");
            }
            else if (pbRegisterPassword.Password.Trim() != pbConfirmPassword.Password.Trim())
            {
                MessageBox.Show("'Confirm password' and 'New password' doesn't match.");
            }
            else
            {
                Countries location = (Countries)Enum.Parse(typeof(Countries), cbCountries.SelectedItem.ToString().Replace(' ', '_'));
                User user = new(txtRegisterUserName.Text.Trim().ToString(), pbRegisterPassword.Password.Trim().ToString(), location);

                if (!_userManager.AddUser(user))
                {
                    MessageBox.Show("The user name is not available. Please choose a different user name.");
                }
                else
                {
                    Hide();

                    MessageBox.Show("You have been successfully registered!");

                    MainWindow mainWindow = new(_userManager, _travelManager);
                    mainWindow.Show();

                    Close();
                }
            }
        }

        // Closes window and initializes and shows new MainWindow 
        private void btnCancelRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();

            Close();
        }

        // Changes textbox text to red if less than 3 characters 
        private void txtRegisterUserName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtRegisterUserName.Text.Trim().Length < 3)
            {
                txtRegisterUserName.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtRegisterUserName.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }

        // Changes password input to red if less than 5 characters 
        private void pbRegisterPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbRegisterPassword.Password.Trim().Length < 5)
            {
                pbRegisterPassword.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                pbRegisterPassword.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }

        // Changes confirm password input to red if less than 5 characters 
        private void pbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbConfirmPassword.Password.Trim().Length < 5)
            {
                pbConfirmPassword.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                pbConfirmPassword.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }



        // Populates combobox with Countries enum 
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
