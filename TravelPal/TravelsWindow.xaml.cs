using System.Windows;
using System.Windows.Controls;
using TravelPal.Interfaces;
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

        // Constructor to initialize TravelsWindow. Sets field variables and calls methods UpdateUI and InitializeComponent. 
        public TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            _travelManager = travelManager;
            _userManager = userManager;

            InitializeComponent();

            UpdateUI();
        }

        // Signs out user (sets SignedInUser to null), closes window, and initializes and shows new MainWindow
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            _userManager.SignedInUser = null;

            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();

            Close();
        }

        // initializes and shows new InfoWindow 
        private void btnTravelPalInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new();
            infoWindow.Show();
        }

        // Closes window and initializes and shows UserDetailsWindow 
        private void btnViewUserDetails_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new(_userManager, _travelManager);
            userDetailsWindow.Show();

            Close();
        }

        // Closes window and initializes and shows new TravelDetailsWindow 
        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvYourTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select travel from list before clicking Details.");
            }
            else
            {
                ListViewItem item = lvYourTravels.SelectedItem as ListViewItem;
                Travel travel = item.Tag as Travel;
                TravelDetailsWindow travelDetailsWindow = new(_userManager, _travelManager, travel);
                travelDetailsWindow.Show();

                Close();
            }
        }

        // Closes window and initializes and shows new AddTravelWindow 
        private void AddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(_userManager, _travelManager);
            addTravelWindow.Show();

            Close();
        }

        // Removes specified travel in _travelManager Travel and in users travel list, and updates the UI 
        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            if (lvYourTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select travel from list to remove.");
            }
            else
            {
                ListViewItem item = lvYourTravels.SelectedItem as ListViewItem;
                Travel travel = item.Tag as Travel;

                _travelManager.RemoveTravel(travel);

                foreach (IUser user in _userManager.Users)
                {
                    if (user.UserName == travel.CreatorUserName && user.GetType().Name.ToString() == "User")
                    {
                        User identifiedUser = user as User;
                        identifiedUser.Travels.Remove(travel);
                    }
                }

                UpdateUI();
            }
        }



        // Updates the window UI 
        private void UpdateUI()
        {
            lvYourTravels.Items.Clear();

            // If Admin or User 
            if (_userManager.SignedInUser.GetType().Name == "Admin")
            {
                lblListviewTravels.Content = "All travels";
            }
            else
            {
                lblListviewTravels.Content = "Your travels";
            }

            lblUserName.Content = _userManager.SignedInUser.UserName;

            // Populate listview with Travels
            if (_userManager.SignedInUser is Admin)
            {
                foreach (Travel travel in _travelManager.Travels)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = travel;
                    item.Content = travel.GetInfo();
                    lvYourTravels.Items.Add(item);
                }
            }
            else
            {
                User user = _userManager.SignedInUser as User;

                foreach (Travel travel in user.GetUserTravelList())
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = travel;
                    item.Content = travel.GetInfo();
                    lvYourTravels.Items.Add(item);
                }
            }
        }
    }
}
