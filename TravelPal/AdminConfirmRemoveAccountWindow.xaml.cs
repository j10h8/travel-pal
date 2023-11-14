using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AdminConfirmRemoveAccountWindow.xaml
    /// </summary>
    public partial class AdminConfirmRemoveAccountWindow : Window
    {
        private UserManager _userManager;
        private TravelManager _travelManager;

        // Constructor to initialize AdminConfirmRemoveAccountWindow. Sets field variables and calls method InitializeComponent. 
        public AdminConfirmRemoveAccountWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();
        }

        // Removes specified user account and all associated user travels 
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            for (int i = _travelManager.Travels.Count - 1; i >= 0; i--)
            {
                if (_userManager.TemporaryUser.UserName == _travelManager.Travels[i].CreatorUserName)
                {
                    _travelManager.Travels.RemoveAt(i);
                }
            }

            _userManager.RemoveUser(_userManager.TemporaryUser);

            Hide();

            MessageBox.Show("The user account has been removed.");

            ManageAccountsWindow manageAccountsWindow = new(_userManager, _travelManager);
            manageAccountsWindow.Show();

            Close();
        }

        // Closes window and shows a new ManageAccountsWindow 
        private void btnCancelRemove_Click(object sender, RoutedEventArgs e)
        {
            ManageAccountsWindow manageAccountsWindow = new(_userManager, _travelManager);
            manageAccountsWindow.Show();

            Close();
        }
    }
}
