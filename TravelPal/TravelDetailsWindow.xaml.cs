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
            // Destination 
            txtDestinationDetails.Text = _travel.Destination;

            // Country 
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

            // No. of travellers 
            if (_travel.Travellers > 1)
            {
                txtTravellersDetails.Text = $"{_travel.Travellers.ToString()} travellers";
            }
            else
            {
                txtTravellersDetails.Text = $"{_travel.Travellers.ToString()} traveller";
            }

            // Travel type 
            txtTravelTypeDetails.Text = _travel.GetType().Name.ToString();

            // XAML elements visibility if travel type is Trip 
            if (_travel.GetType().Name.ToString() == "Trip")
            {
                txtIfNotATrip.Visibility = Visibility.Hidden;

                Trip trip = _travel as Trip;

                string[] tripTypes = Enum.GetNames(typeof(TripTypes));
                foreach (string tripType in tripTypes)
                {
                    cbTripTypeDetails.Items.Add(tripType);
                }

                cbTripTypeDetails.SelectedItem = trip.Type.ToString();
            }
            // XAML elements visibility if travel type is Vacation 
            else
            {
                cbTripTypeDetails.Visibility = Visibility.Hidden;
                txtIfNotAVacation.Visibility = Visibility.Hidden;

                // Checkbox checked or not 
                Vacation vacation = _travel as Vacation;

                if (vacation.AllInclusive == true)
                {
                    cbxAllInclusiveDetails.IsChecked = true;
                }
                else
                {
                    cbxAllInclusiveDetails.IsChecked = false;
                }
            }

            // Travel time 
            txtStartDateDetails.Text = _travel.StartDate.ToString("d");
            txtEndDateDetails.Text = _travel.EndDate.ToString("d");

            if (_travel.TravelDays > 1)
            {
                txtTravelLengthDetails.Text = $"{_travel.TravelDays.ToString()} days";
            }
            else
            {
                txtTravelLengthDetails.Text = $"{_travel.TravelDays.ToString()} day";
            }
        }
    }
}
