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

            if (_userManager.SignedInUser is Admin)
            {
                lblListviewTravels.Content = "All registered travels";

                foreach (User user in _userManager.Users)       // TODO: solve admin/user bug 
                {
                    foreach (Travel travel in user.GetUserTravelList())
                    {
                        ListViewItem item = new();
                        item.Tag = user;

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

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new(_userManager, _travelManager);
            mainWindow.Show();

            Close();
        }

        // ******************** EVENTS *********************

        // ******************** METHODS ********************
    }
}
