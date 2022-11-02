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

        public ConfirmRemoveAccountWindow(UserManager userManager, TravelManager travelManager)
        {
            _userManager = userManager;
            _travelManager = travelManager;

            InitializeComponent();
        }

        private void btnCancelRemove_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

            for (int i = _travelManager.Travels.Count() - 1; i >= 0; i--)
            {
                if (_userManager.SignedInUser.UserName == _travelManager.Travels[i].CreatorUserName)
                {
                    _travelManager.RemoveTravel(_travelManager.Travels[i]);
                }
            }

            _userManager.RemoveUser(_userManager.SignedInUser); //**************************

            foreach (IUser user in _userManager.Users)
            {
                if (user.UserName == "admin")
                {
                    _userManager.SignedInUser = user;
                }
            }

            MessageBox.Show("The user account has been successfully removed!");

            UserDetailsWindow userDetailsWindow = new(_userManager, _travelManager);
            userDetailsWindow.Close();

            Close();

            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();
        }
    }
}
