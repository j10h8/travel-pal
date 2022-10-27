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

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvYourTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select travel from list before clicking Details.", "Warning!");
            }
        }

        private void btnRemoveTravel_Click(object sender, RoutedEventArgs e)
        {
            if (lvYourTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select travel from list to remove.", "Warning!");
            }
        }

        // ******************** METHODS ********************
        private void GenerateUI()
        {
            lblUserName.Content = _userManager.SignedInUser.UserName;

            if (_userManager.SignedInUser is Admin)
            {
                foreach (Travel travel in _travelManager.Travels)
                {
                    ListViewItem item = new ListViewItem();
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
            else
            {
                User user = _userManager.SignedInUser as User;

                foreach (Travel travel in user.GetUserTravelList())
                {
                    ListViewItem item = new ListViewItem();
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


//private void GenerateUI()
//{
//    lvYourTravels.Items.Clear();

//    if (_userManager.SignedInUser is Admin)
//    {
//        lblListviewTravels.Content = "All registered travels";

//        foreach (User user in _userManager.GetFilteredUserList())       // Här använda main list, inte baserat på users...
//        {
//            foreach (Travel travel in user.GetUserTravelList())
//            {
//                ListViewItem item = new();
//                item.Tag = travel;

//                if (travel.TravelDays < 2)
//                {
//                    item.Content = $"{travel.Country}, {travel.TravelDays} day";
//                    lvYourTravels.Items.Add(item);
//                }
//                else
//                {
//                    item.Content = $"{travel.Country}, {travel.TravelDays} days";
//                    lvYourTravels.Items.Add(item);
//                }
//            }
//        }
//    }
//    else
//    {
//        lblListviewTravels.Content = "Your travels";

//        User user = _userManager.SignedInUser as User;

//        foreach (Travel travel in user.GetUserTravelList())
//        {
//            ListViewItem item = new();
//            item.Tag = travel;

//            if (travel.TravelDays < 2)
//            {
//                item.Content = $"{travel.Country}, {travel.TravelDays} day";
//                lvYourTravels.Items.Add(item);
//            }
//            else
//            {
//                item.Content = $"{travel.Country}, {travel.TravelDays} days";
//                lvYourTravels.Items.Add(item);
//            }
//        }
//    }
//}


//ListViewItem item = lvYourTravels.SelectedItem as ListViewItem;
//Travel travel = item.Tag as Travel;

//_travelManager.RemoveTravel(travel);

//foreach (User user in _userManager.GetFilteredUserList())
//{
//    if (user.UserName == travel.CreatorsUserName)
//    {
//        user.Travels.Remove(travel);
//    }
//}

//GenerateUI();

//ListViewItem item = lvYourTravels.SelectedItem as ListViewItem;
//Travel travel = item.Tag as Travel;

//_travelManager.RemoveTravel(travel);

//for (int i = 0; i < _travelManager.Travels.Count; i++)
//{
//    User user = _userManager.Users[i] as User;

//    if (user.UserName == travel.CreatorsUserName)
//    {
//        user.Travels.Remove(travel);
//    }
//}


////foreach (IUser iUser in _userManager.Users)
////{
////    User user = iUser as User;

////    if (user.UserName == travel.CreatorsUserName)
////    {
////        user.Travels.Remove(travel);
////    }
////}

////foreach (User user in _userManager.GetFilteredUserList())
////{
////    if (user.UserName == travel.CreatorsUserName)
////    {
////        user.Travels.Remove(travel);
////    }
////}