using System.Windows;
using System.Windows.Controls;
using TravelPal.Interfaces;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for ManageAccountsWindow.xaml
    /// </summary>
    public partial class ManageAccountsWindow : Window
    {
        UserManager _userManager;
        TravelManager _travelManager;

        // Constructor to initialize ManageAccountsWindow. Sets field variables and calls methods UpdateUI and InitializeComponent. 
        public ManageAccountsWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();

            UpdateUI();
        }

        // Closes ManageAccountsWindow and initializes and shows UserDetailsWindow
        private void btnCancelManageAccountsWindow_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(_userManager, _travelManager);

            userDetailsWindow.Show();

            Close();
        }

        // Sets field variable in _userManager as the selected user account and initializes and shows AdminConfirmRemoveAccountWindow
        private void btnRemoveUserAccount_Click(object sender, RoutedEventArgs e)
        {
            if (lvUserAccounts.SelectedItems.Count > 0)
            {
                ListViewItem item = lvUserAccounts.SelectedItem as ListViewItem;
                IUser user = item.Tag as IUser;
                _userManager.TemporaryUser = user;

                AdminConfirmRemoveAccountWindow adminConfirmRemoveAccountWindow = new(_userManager, _travelManager);
                adminConfirmRemoveAccountWindow.Show();

                Close();
            }
            else
            {
                MessageBox.Show("Please select user account from list to remove.");
            }
        }



        // Updates window UI 
        private void UpdateUI()
        {
            lvUserAccounts.Items.Clear();

            foreach (IUser user in _userManager.Users)
            {
                if (user.GetType().Name == "User")
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = user;
                    item.Content = user.GetInfo();
                    lvUserAccounts.Items.Add(item);
                }
            }
        }
    }
}
