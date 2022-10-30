using System;
using System.Windows;
using TravelPal.Enums;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {
        UserManager _userManager;
        TravelManager _travelManager;
        Travel _travel;

        public TravelDetailsWindow(UserManager userManager, TravelManager travelManager, Travel travel)
        {
            _userManager = userManager;
            _travelManager = travelManager;
            _travel = travel;

            InitializeComponent();

            UpdateUI();
        }




        // ******************** EVENTS *********************
        private void btnCancelTravelDetailsWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(_userManager, _travelManager);
            travelsWindow.Show();

            Close();
        }




        // ******************** METHODS ********************
        private void UpdateUI()
        {
            txtDestinationDetails.Text = _travel.Destination;

            string[] countries = Enum.GetNames(typeof(Countries));
            foreach (string country in countries)
            {
                cbCountryDetails.Items.Add(country.Replace('_', ' '));
            }

            foreach (string country in countries)
            {
                if (country == _travel.Country.ToString())
                {
                    cbCountryDetails.SelectedItem = country;
                }
            }

            txtTravellersDetails.Text = _travel.Travellers.ToString();

            txtTravelTypeDetails.Text = _travel.GetType().Name.ToString();

            if (_travel.GetType().Name.ToString() == "Trip")
            {
                txtIfNotATrip.Visibility = Visibility.Hidden;

                Trip trip = _travel as Trip;

                string[] tripTypes = Enum.GetNames(typeof(TripTypes));
                foreach (string tripType in tripTypes)
                {
                    cbTripTypeDetails.Items.Add(tripType);
                }

                cbTripTypeDetails.SelectedItem = trip.GetInfo();
            }
            else
            {
                cbTripTypeDetails.Visibility = Visibility.Hidden;
            }
        }
    }
}
