using System;
using System.Windows;
using TravelPal.Enums;
using TravelPal.Managers;

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
