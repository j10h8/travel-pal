using System;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Enums;
using TravelPal.Interfaces;
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

        // Constructor to initialize TravelDetailsWindow. Sets field variables and calls methods UpdateUI and InitializeComponent. 
        public TravelDetailsWindow(UserManager userManager, TravelManager travelManager, Travel travel)
        {
            _userManager = userManager;
            _travelManager = travelManager;
            _travel = travel;

            InitializeComponent();

            UpdateUI();
        }

        // Closes window and initializes and shows new TravelsWindow 
        private void btnCancelAddTravelWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(_userManager, _travelManager);
            travelsWindow.Show();

            Close();
        }

        // Updates UI depending on whether selected combobox item is Trip or Vacation
        private void cbTypeOfTravelAddTravel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Trip")
            {
                lblTypeOfTrip.Visibility = Visibility.Visible;
                brdTripTypeDetails.Visibility = Visibility.Visible;
                cbTripTypeDetailsAddTravel.Visibility = Visibility.Visible;
                cbTripTypeDetailsAddTravel.IsEnabled = true;

                lblAllInclusive.Visibility = Visibility.Hidden;
                brdAllInclusive.Visibility = Visibility.Hidden;
                cbxAllInclusiveDetails.Visibility = Visibility.Hidden;
                cbxAllInclusiveDetails.IsEnabled = false;

            }
            else if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Vacation")
            {
                lblAllInclusive.Visibility = Visibility.Visible;
                brdAllInclusive.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.IsChecked = false;
                cbxAllInclusiveDetails.IsEnabled = true;

                lblTypeOfTrip.Visibility = Visibility.Hidden;
                brdTripTypeDetails.Visibility = Visibility.Hidden;
                cbTripTypeDetailsAddTravel.Visibility = Visibility.Hidden;
                cbTripTypeDetailsAddTravel.SelectedItem = null;
                cbTripTypeDetailsAddTravel.IsEnabled = false;
            }
        }



        // Updates/populates UI with specific travel information 
        private void UpdateUI()
        {
            // Hide/lock fields 
            txtDestinationAddTravel.IsReadOnly = true;
            cbCountryAddTravel.IsEnabled = false;
            txtTravellersAddTravel.IsReadOnly = true;
            cbTypeOfTravelAddTravel.IsEnabled = false;
            brdTripTypeDetails.IsEnabled = false;
            brdAllInclusive.IsEnabled = false;
            brdAllInclusive.Visibility = Visibility.Hidden;
            lblTypeOfTrip.Visibility = Visibility.Hidden;
            lblAllInclusive.Visibility = Visibility.Hidden;
            cbTripTypeDetailsAddTravel.IsEnabled = false;

            // Travel destination 
            txtDestinationAddTravel.Text = _travel.Destination;

            // Populate cbCountry
            string[] countries = Enum.GetNames(typeof(Countries));
            foreach (string country in countries)
            {
                cbCountryAddTravel.Items.Add(country.Replace('_', ' '));
            }

            cbCountryAddTravel.SelectedItem = _travel.Country.ToString().Replace('_', ' ');

            // Travel no. of travellers 
            if (_travel.Travellers > 1)
            {
                txtTravellersAddTravel.Text = $"{_travel.Travellers.ToString()} travellers";
            }
            else
            {
                txtTravellersAddTravel.Text = $"{_travel.Travellers.ToString()} traveller";
            }

            // Populate cbTypeofTravel
            cbTypeOfTravelAddTravel.Items.Add("Trip");
            cbTypeOfTravelAddTravel.Items.Add("Vacation");

            // Travel type          
            if (_travel.GetType().Name.ToString() == "Trip")
            {
                cbTypeOfTravelAddTravel.SelectedItem = "Trip";
            }
            else
            {
                cbTypeOfTravelAddTravel.SelectedItem = "Vacation";
            }

            // if Trip
            if (_travel.GetType().Name.ToString() == "Trip")
            {
                Trip trip = _travel as Trip;

                string[] tripTypes = Enum.GetNames(typeof(TripTypes));
                foreach (string tripType in tripTypes)
                {
                    cbTripTypeDetailsAddTravel.Items.Add(tripType);
                }

                cbTripTypeDetailsAddTravel.SelectedItem = trip.Type.ToString();
            }

            // if Vacation  
            else
            {
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

            // Packing list 
            foreach (IPackingListItem packingListItem in _travel.PackingList)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = packingListItem;
                item.Content = packingListItem.GetInfo();
                lvPackingList.Items.Add(item);
            }

            // Travel dates/length 
            txtStartDate.Text = $"Start date: {_travel.StartDate.ToString("d")}";
            txtEndDate.Text = $"End date: {_travel.EndDate.ToString("d")}";

            if (_travel.TravelDays > 1)
            {
                txtTravelLength.Text = $"{_travel.TravelDays.ToString()} days";
            }
            else
            {
                txtTravelLength.Text = $"{_travel.TravelDays.ToString()} day";
            }
        }

        // Closes window and initializes and shows new EditTravelWindow
        private void btnEditTravel_Click(object sender, RoutedEventArgs e)
        {
            EditTravelWindow editTravelWindow = new(_userManager, _travelManager, _travel);
            editTravelWindow.Show();

            Close();
        }
    }
}