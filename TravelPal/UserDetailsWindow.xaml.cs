using System;
using System.Windows;
using System.Windows.Media;
using TravelPal.Enums;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private UserManager _userManager;
        private TravelManager _travelManager;

        public UserDetailsWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();

            UpdateUI();
        }

        // ******************** EVENTS *********************
        private void btnSaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtUpdateUserName.Text.Trim().Length == 0 && string.IsNullOrEmpty(cbUpdateCountry.Text) && pbUpdatePassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("Please enter new details in at least one of the above fields 'User name', 'Country', and/or, 'New password'.");
            }
            else if (txtUpdateUserName.Text.Trim().Length > 1 && txtUpdateUserName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a new username with at least three characters.");
            }
            else if (txtUpdateUserName.Text.Trim().Length > 2 && !_userManager.ValidateUserNameUpdate(txtUpdateUserName.Text.Trim()))
            {
                MessageBox.Show("The user name is not available. Please choose a different user name.");
            }
            else if (pbUpdatePassword.Password.Trim().Length > 1 && pbUpdatePassword.Password.Trim().Length < 5)
            {
                MessageBox.Show("Please enter a new password with at least five characters.");
            }
            else if (pbUpdatePassword.Password.Trim().Length > 4 && pbConfirmPassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("Please confirm new password by entering it again in the 'Confirm password' field.");
            }
            else if (pbUpdatePassword.Password != pbConfirmPassword.Password)
            {
                MessageBox.Show("'Confirm password' and 'New password' doesn't match.");
            }
            else
            {           // Update user name, location, and password 
                if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            // Creator user name
                            for (int n = 0; n < _travelManager.Travels.Count; n++)
                            {
                                if (_travelManager.Travels[n].CreatorUserName == _userManager.SignedInUser.UserName)
                                {
                                    _travelManager.Travels[n].CreatorUserName = txtUpdateUserName.Text.Trim();
                                }
                            }
                            _userManager.Users[i].UserName = txtUpdateUserName.Text.Trim();
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                            _userManager.Users[i].Password = pbUpdatePassword.Password.Trim();
                        }
                    }
                }           // Update user name and location 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length == 0)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        // Creator user name
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            for (int n = 0; n < _travelManager.Travels.Count; n++)
                            {
                                if (_travelManager.Travels[n].CreatorUserName == _userManager.SignedInUser.UserName)
                                {
                                    _travelManager.Travels[n].CreatorUserName = txtUpdateUserName.Text.Trim();
                                }
                            }
                            _userManager.Users[i].UserName = txtUpdateUserName.Text.Trim();
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                        }
                    }
                }           // Update location and password 
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                            _userManager.Users[i].Password = pbUpdatePassword.Password.Trim();
                        }
                    }
                }           // Update user name and password 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length == 0 && pbUpdatePassword.Password.Trim().Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            // Update creator user name
                            for (int n = 0; n < _travelManager.Travels.Count; n++)
                            {
                                if (_travelManager.Travels[n].CreatorUserName == _userManager.SignedInUser.UserName)
                                {
                                    _travelManager.Travels[n].CreatorUserName = txtUpdateUserName.Text.Trim();
                                }
                            }
                            _userManager.Users[i].UserName = txtUpdateUserName.Text.Trim();
                            _userManager.Users[i].Password = pbUpdatePassword.Password.Trim();
                        }
                    }
                }           // Update user name 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length == 0 && pbUpdatePassword.Password.Trim().Length == 0)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            // Update creator user name
                            for (int n = 0; n < _travelManager.Travels.Count; n++)
                            {
                                if (_travelManager.Travels[n].CreatorUserName == _userManager.SignedInUser.UserName)
                                {
                                    _travelManager.Travels[n].CreatorUserName = txtUpdateUserName.Text.Trim();
                                }
                            }
                            _userManager.Users[i].UserName = txtUpdateUserName.Text.Trim();
                        }
                    }
                }           // Update location 
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length == 0)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                        }
                    }
                }           // Update password 
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length == 0 && pbUpdatePassword.Password.Trim().Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            _userManager.Users[i].Password = pbUpdatePassword.Password.Trim();
                        }
                    }
                }

                MessageBox.Show("Your user details have been updated!");

                TravelsWindow travelsWindow = new(_userManager, _travelManager);
                travelsWindow.Show();

                Close();
            }
        }

        private void btnCancelDetailsWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(_userManager, _travelManager);
            travelsWindow.Show();

            Close();
        }

        private void txtUpdateUserName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtUpdateUserName.Text.Length < 3)
            {
                txtUpdateUserName.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtUpdateUserName.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }

        private void pbUpdatePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbUpdatePassword.Password.Length < 5)
            {
                pbUpdatePassword.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                pbUpdatePassword.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }

        private void pbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (pbConfirmPassword.Password.Length < pbUpdatePassword.Password.Length)
            {
                pbConfirmPassword.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                pbConfirmPassword.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (_userManager.SignedInUser.GetType().Name.ToString() != "Admin")
            {
                ConfirmRemoveAccountWindow confirmRemoveAccountWindow = new(_userManager, _travelManager);

                confirmRemoveAccountWindow.Show();
            }
            else
            {
                MessageBox.Show("Removing the admin user is not allowed.");
            }

        }

        // ******************** METHODS ********************

        private void UpdateUI()
        {
            lblCurrentName.Content = _userManager.SignedInUser.UserName;
            lblCurrentCountry.Content = _userManager.SignedInUser.Location.ToString().Replace('_', ' ');

            string[] countries = Enum.GetNames(typeof(Countries));
            foreach (string country in countries)
            {
                cbUpdateCountry.Items.Add(country.Replace('_', ' '));
            }
        }
    }
}
