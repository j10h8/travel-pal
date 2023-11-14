using System;
using System.Windows;
using System.Windows.Media;
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

        // Constructor to initialize UserDetailsWindow. Sets field variables and calls methods UpdateUI and InitializeComponent. 
        public UserDetailsWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();

            UpdateUI();
        }

        // Checks input and (if OK) updates user name, password, and/or location, depending on which field(s) contain(s) input 
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
            {   // Update user name, location, and password 
                if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    RemoveOldPassports();

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

                    GenerateNewPassport();
                }
                // Update user name and location 
                else if (txtUpdateUserName.Text.Trim().Length > 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length == 0)
                {
                    RemoveOldPassports();

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

                    GenerateNewPassport();
                }
                // Update location and password 
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length > 0 && pbUpdatePassword.Password == pbConfirmPassword.Password)
                {
                    RemoveOldPassports();

                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                            _userManager.Users[i].Password = pbUpdatePassword.Password.Trim();
                        }
                    }

                    GenerateNewPassport();
                }
                // Update user name and password 
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
                }
                // Update user name 
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
                }
                // Update location 
                else if (txtUpdateUserName.Text.Trim().Length == 0 && cbUpdateCountry.Text.Length > 0 && pbUpdatePassword.Password.Trim().Length == 0)
                {
                    RemoveOldPassports();

                    for (int i = 0; i < _userManager.Users.Count; i++)
                    {
                        if (_userManager.SignedInUser.UserName == _userManager.Users[i].UserName)
                        {
                            Countries location = (Countries)Enum.Parse(typeof(Countries), cbUpdateCountry.SelectedItem.ToString().Replace(' ', '_'));
                            _userManager.Users[i].Location = location;
                        }
                    }

                    GenerateNewPassport();
                }
                // Update password 
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

                UserDetailsWindow userDetailsWindow = new(_userManager, _travelManager);
                userDetailsWindow.Show();

                Close();
            }
        }

        // Closes window and initializes and shows a new TravelsWindow
        private void btnCancelDetailsWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(_userManager, _travelManager);
            travelsWindow.Show();

            Close();
        }

        // Sets update user name text to red if characters < 3
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

        // Sets update password text to red if characters < 5
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

        // Sets update confirm password text to red if length < update password text
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

        // Closes window and initializes and shows a new ConfirmRemoveAccountWindow 
        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            ConfirmRemoveAccountWindow confirmRemoveAccountWindow = new(_userManager, _travelManager);

            confirmRemoveAccountWindow.Show();

            Close();
        }



        // Updates window UI 
        private void UpdateUI()
        {
            lblCurrentName.Content = _userManager.SignedInUser.UserName;
            lblCurrentCountry.Content = _userManager.SignedInUser.Location.ToString().Replace('_', ' ');

            string[] countries = Enum.GetNames(typeof(Countries));
            foreach (string country in countries)
            {
                cbUpdateCountry.Items.Add(country.Replace('_', ' '));
            }

            if (_userManager.SignedInUser.GetType().Name == "Admin")
            {
                btnRemoveUser.Visibility = Visibility.Hidden;
            }
            else
            {
                btnManageUserAccounts.Visibility = Visibility.Hidden;
            }
        }

        // Removes IPackingListItem Passport(s) from signed in users Travel list and in signed in users Travels in TravelManager Travels list 
        private void RemoveOldPassports()
        {
            for (int i = 0; i < _travelManager.Travels.Count; i++)
            {
                if (_userManager.SignedInUser.UserName == _travelManager.Travels[i].CreatorUserName)
                {
                    for (int n = _travelManager.Travels[i].PackingList.Count - 1; n >= 0; n--)
                    {
                        if (_travelManager.Travels[i].PackingList[n].GetInfo() == "Passport (required)" || _travelManager.Travels[i].PackingList[n].GetInfo() == "Passport (not required)")
                        {
                            _travelManager.Travels[i].PackingList.RemoveAt(n);
                        }
                    }
                }
            }
        }

        // initializes IPackingListItem Passport depending on travel country and updated user location, and adds to signed in users Travel list and in signed in users Travels in TravelManager Travels list.
        private void GenerateNewPassport()
        {
            for (int i = 0; i < _travelManager.Travels.Count; i++)
            {
                if (_userManager.SignedInUser.UserName == _travelManager.Travels[i].CreatorUserName)
                {
                    if (!Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()))
                    {
                        TravelDocument travelDocument = new("Passport", true);
                        _travelManager.Travels[i].PackingList.Add(travelDocument);
                    }
                    else if (Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()) && !Enum.IsDefined(typeof(EuropeanCountries), _travelManager.Travels[i].Country.ToString()))
                    {
                        TravelDocument travelDocument = new("Passport", true);
                        _travelManager.Travels[i].PackingList.Add(travelDocument);
                    }
                    else if (Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()) && Enum.IsDefined(typeof(EuropeanCountries), _travelManager.Travels[i].Country.ToString()) && _userManager.SignedInUser.Location.ToString() != _travelManager.Travels[i].Country.ToString())
                    {
                        TravelDocument travelDocument = new("Passport", false);
                        _travelManager.Travels[i].PackingList.Add(travelDocument);
                    }
                }
            }
        }

        // Closes window and initializes and shows new ManageAccountsWindow 
        private void btnManageUserAccounts_Click(object sender, RoutedEventArgs e)
        {
            ManageAccountsWindow manageAccountsWindow = new(_userManager, _travelManager);

            manageAccountsWindow.Show();

            Close();
        }
    }
}
