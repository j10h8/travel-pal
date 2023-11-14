using System.Linq;
using System.Windows;
using TravelPal.Interfaces;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for ConfirmRemoveAccountWindow.xaml
    /// </summary>
    public partial class ConfirmRemoveAccountWindow : Window
    {
        private UserManager _userManager;
        private TravelManager _travelManager;

        // Constructor to initialize ConfirmRemoveAccountWindow. Sets field variables and calls method InitializeComponent. 
        public ConfirmRemoveAccountWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();
        }

        // Closes window and shows a new UserDetailsWindow 
        private void btnCancelRemove_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(_userManager, _travelManager);
            userDetailsWindow.Show();

            Close();
        }

        // Removes specified user account and all associated user travels 
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            for (int i = _travelManager.Travels.Count() - 1; i >= 0; i--)
            {
                if (_userManager.SignedInUser.UserName == _travelManager.Travels[i].CreatorUserName)
                {
                    _travelManager.RemoveTravel(_travelManager.Travels[i]);
                }
            }

            _userManager.RemoveUser(_userManager.SignedInUser);

            foreach (IUser user in _userManager.Users)
            {
                if (user.GetType().Name.ToString() == "Admin")
                {
                    _userManager.SignedInUser = user;
                }
            }

            Hide();

            MessageBox.Show("The user account has been removed.");

            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();

            Close();
        }
    }
}
