using System.Windows;
using System.Windows.Controls;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        TravelManager _travelManager;
        UserManager _userManager;

        public TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            _travelManager = travelManager;
            _userManager = userManager;

            InitializeComponent();

            lblUserName.Content = _userManager.SignedInUser.UserName;

            GenerateUI();
        }

        // ******************** EVENTS *********************
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            _userManager.SignedInUser = null;

            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();

            Close();
        }

        private void btnTravelPalInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new();
            infoWindow.Show();
        }

        private void lvYourTravels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvYourTravels.SelectedItems.Count > 0)
            {
                btnRemoveTravel.IsEnabled = true;
                btnRemoveTravel.Visibility = Visibility.Visible;
            }
            else
            {
                btnRemoveTravel.IsEnabled = false;
                btnRemoveTravel.Visibility = Visibility.Hidden;
            }
        }

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = lvYourTravels.SelectedItem as ListViewItem;
            Travel travel = item.Tag as Travel;

            _travelManager.RemoveTravel(travel);

            foreach (User user in _userManager.GetFilteredUserList())
            {
                if (user.UserName == travel.CreatorsUserName)
                {
                    user.Travels.Remove(travel);
                }
            }

            GenerateUI();
        }

        // ******************** METHODS ********************
        private void GenerateUI()
        {
            lvYourTravels.Items.Clear();

            if (_userManager.SignedInUser is Admin)
            {
                lblListviewTravels.Content = "All registered travels";

                foreach (User user in _userManager.GetFilteredUserList())
                {
                    foreach (Travel travel in user.GetUserTravelList())
                    {
                        ListViewItem item = new();
                        item.Tag = travel;

                        if (travel.TravelDays < 2)
                        {
                            item.Content = $"{travel.Country}, {travel.TravelDays} day";
                            lvYourTravels.Items.Add(item);
                        }
                        else
                        {
                            item.Content = $"{travel.Country}, {travel.TravelDays} days";
                            lvYourTravels.Items.Add(item);
                        }
                    }
                }
            }
            else
            {
                lblListviewTravels.Content = "Your travels";

                User user = _userManager.SignedInUser as User;

                foreach (Travel travel in user.GetUserTravelList())
                {
                    ListViewItem item = new();
                    item.Tag = travel;

                    if (travel.TravelDays < 2)
                    {
                        item.Content = $"{travel.Country}, {travel.TravelDays} day";
                        lvYourTravels.Items.Add(item);
                    }
                    else
                    {
                        item.Content = $"{travel.Country}, {travel.TravelDays} days";
                        lvYourTravels.Items.Add(item);
                    }
                }
            }
        }
    }
}
