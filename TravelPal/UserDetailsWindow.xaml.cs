using System;
using System.Windows;
using TravelPal.Enums;
using TravelPal.Managers;
using TravelPal.Models;

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
                MessageBox.Show("Please enter new details in at least one of the above fields 'User name', 'Country', and/or, 'New password'.", "Warning!");
            }
            else if (txtUpdateUserName.Text.Trim().Length > 1 && txtUpdateUserName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a new username with at least three characters.", "Warning!");
            }
            else if (txtUpdateUserName.Text.Trim().Length > 2 && !_userManager.ValidateUserNameUpdate(txtUpdateUserName.Text.Trim()))
            {
                MessageBox.Show("The user name is not available. Please choose a different user name.", "Warning!");
            }
            else if (pbUpdatePassword.Password.Length > 1 && pbUpdatePassword.Password.Length < 5)
            {
                MessageBox.Show("Please enter a new password with at least five characters.", "Warning!");
            }
            else if (pbUpdatePassword.Password.Length > 4 && pbConfirmPassword.Password.Length == 0)
            {
                MessageBox.Show("Please confirm new password by entering it again in the 'Confirm password' field.", "Warning!");
            }
            else if (pbUpdatePassword.Password != pbConfirmPassword.Password)
            {
                MessageBox.Show("'Confirm password' and 'New password' doesn't match.", "Warning!");
            }
            else
            {           // Update user name, location, and password 
                if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            // Update creator user name
                            User user = _userManager.Users[i] as User;
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
                            _userManager.Users[i].Password = pbUpdatePassword.Password;
                        }
                    }
                }           // Update user name and location 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Length == 0)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        // Update creator user name
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            User user = _userManager.Users[i] as User;
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
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                            _userManager.Users[i].Password = pbUpdatePassword.Password;
                        }
                    }
                }           // Update user name and password 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length == 0 && pbUpdatePassword.Password.Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            // Update creator user name
                            User user = _userManager.Users[i] as User;
                            for (int n = 0; n < _travelManager.Travels.Count; n++)
                            {
                                if (_travelManager.Travels[n].CreatorUserName == _userManager.SignedInUser.UserName)
                                {
                                    _travelManager.Travels[n].CreatorUserName = txtUpdateUserName.Text.Trim();
                                }
                            }

                            _userManager.Users[i].UserName = txtUpdateUserName.Text.Trim();
                            _userManager.Users[i].Password = pbUpdatePassword.Password;
                        }
                    }
                }           // Update user name 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length == 0 && pbUpdatePassword.Password.Length == 0)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            // Update creator user name
                            User user = _userManager.Users[i] as User;
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
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Length == 0)
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
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length == 0 && pbUpdatePassword.Password.Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            _userManager.Users[i].Password = pbUpdatePassword.Password;
                        }
                    }
                }

                MessageBox.Show("Your user details have been updated!", "Success!");

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
