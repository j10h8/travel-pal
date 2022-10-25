using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager userManager = new();
        TravelManager travelManager = new();

        public MainWindow()
        {
            InitializeComponent();

            userManager.AddGandalf();
            userManager.AddAdmin();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0 || pbPassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("Please enter username and password!");
            }
            else if (!userManager.SignInUser(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
            {
                MessageBox.Show("User name and/or password do/does not exist!");
            }
            else if (userManager.SignInUser(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
            {
                TravelsWindow travelsWindow = new(travelManager, userManager);      // TODO: Check demand for parameters to send (IsSignedIn as well). 

                travelsWindow.Show();
            }
        }

        private void btnToRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(userManager);

            registerWindow.Show();
        }
    }
}
