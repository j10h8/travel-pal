using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager _userManager;
        TravelManager _travelManager;

        public MainWindow()
        {
            InitializeComponent();

            _userManager = new();
            _travelManager = new();

            _userManager.AddGandalf();
            _userManager.AddAdmin();
        }

        public MainWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();

            _userManager = userManager;
            _travelManager = travelManager;
        }

        // ******************** EVENTS *********************
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0 || pbPassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("Please enter username and password!");
            }
            else if (!_userManager.SignInUser(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
            {
                MessageBox.Show("User name and/or password do/does not exist!");
            }
            else if (_userManager.SignInUser(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
            {
                TravelsWindow travelsWindow = new(_userManager, _travelManager);
                travelsWindow.Show();

                Close();
            }
        }

        private void btnToRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(_userManager, _travelManager);
            registerWindow.Show();

            Close();
        }

        // ******************** METHODS ********************
    }
}
