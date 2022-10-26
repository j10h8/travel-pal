namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager _userManager;

        public RegisterWindow(UserManager userManager)
        {
            _userManager = userManager;

            InitializeComponent();

            PopulateCbCountries();
        }

        // ******************** EVENTS *********************
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtRegisterUserName.Text.Trim().Length == 0 || pbRegisterPassword.Password.Trim().Length == 0 || string.IsNullOrEmpty(cbCountries.Text))
            {
                MessageBox.Show("Please provide all required inputs (user name, password, and country)!");
            }
            else if (txtRegisterUserName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a username with at least three characters!");
            }
            else if (pbRegisterPassword.Password.Trim().Length < 5)
            {
                MessageBox.Show("Please enter a password with at least five characters!");
            }
            else
            {
                // Register new user, message that user was created, open mainwindow, and close registerwindow 
            }
        }

        private void btnCancelRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();

            Close();
        }

        // ******************** METHODS ********************
        private void PopulateCbCountries()
        {
            string[] countries = Enum.GetNames(typeof(Countries));

            foreach (string country in countries)
            {
                cbCountries.Items.Add(country.Replace('_', ' '));
            }
        }
    }
}
