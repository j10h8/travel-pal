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
            else if (!userManager.SignInUser(txtUserName.Text, pbPassword.Password))
            {
                MessageBox.Show("User name and/or password do/does not exist!");
            }
            else if (userManager.SignInUser(txtUserName.Text, pbPassword.Password))
            {
                TravelsWindow travelsWindow = new(travelManager, userManager, userManager.SignedInUser);

                travelsWindow.Show();
            }
        }
    }
}
