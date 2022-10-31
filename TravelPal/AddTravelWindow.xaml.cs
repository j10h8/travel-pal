using System;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Enums;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        TravelManager _travelManager;
        UserManager _userManager;

        public AddTravelWindow(UserManager userManager, TravelManager travelManager)
        {
            _travelManager = travelManager;
            _userManager = userManager;

            InitializeComponent();

            UpdateUI();
        }

        // ******************** EVENTS *********************
        private void btnCancelAddTravelWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(_userManager, _travelManager);
            travelsWindow.Show();

            Close();
        }

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

        private void cbxDocument_Checked(object sender, RoutedEventArgs e)
        {
            lblRequired.Visibility = Visibility.Visible;
            cbxRequired.IsEnabled = true;
            brdCbxRequired.Visibility = Visibility.Visible;

            lblQuantity.Visibility = Visibility.Hidden;
            txtQuantity.Visibility = Visibility.Hidden;
            txtQuantity.IsEnabled = false;
        }

        private void cbxDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            lblRequired.Visibility = Visibility.Hidden;
            cbxRequired.IsEnabled = false;
            cbxRequired.IsChecked = false;
            brdCbxRequired.Visibility = Visibility.Hidden;

            lblQuantity.Visibility = Visibility.Visible;
            txtQuantity.Visibility = Visibility.Visible;
            txtQuantity.IsEnabled = true;
        }

        private void txtPackingListItem_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtPackingListItem.Text.Length > 0)
            {
                lblDocument.Visibility = Visibility.Visible;
                cbxDocument.IsEnabled = true;
                brdCbxDocument.Visibility = Visibility.Visible;

                lblQuantity.Visibility = Visibility.Visible;
                txtQuantity.Visibility = Visibility.Visible;
                txtQuantity.IsEnabled = true;
            }
            else
            {
                lblDocument.Visibility = Visibility.Hidden;
                cbxDocument.IsEnabled = false;
                cbxDocument.IsChecked = false;
                brdCbxDocument.Visibility = Visibility.Hidden;

                lblQuantity.Visibility = Visibility.Hidden;
                txtQuantity.Visibility = Visibility.Hidden;
                txtQuantity.Text = null;
                txtQuantity.IsEnabled = false;
            }
        }

        private void cbCountryAddTravel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int lvIndex = -1;

            foreach (ListViewItem listViewItem in lvPackingList.Items)
            {
                if (listViewItem.Content.ToString() == "Passport (required)" || listViewItem.Content.ToString() == "Passport (not required)")
                {
                    lvIndex = lvPackingList.Items.IndexOf(listViewItem);
                }
            }

            if (lvIndex > -1)
            {
                lvPackingList.Items.RemoveAt(lvIndex);
            }

            if (Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()) && !Enum.IsDefined(typeof(EuropeanCountries), cbCountryAddTravel.SelectedItem.ToString()))
            {
                TravelDocument travelDocument = new("Passport", true);
                ListViewItem item = new();
                item.Tag = travelDocument;
                item.Content = travelDocument.GetInfo();
                lvPackingList.Items.Add(item);
            }

            if (Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()) && Enum.IsDefined(typeof(EuropeanCountries), cbCountryAddTravel.SelectedItem.ToString()) && cbCountryAddTravel.SelectedItem.ToString() != _userManager.SignedInUser.Location.ToString())
            {
                TravelDocument travelDocument = new("Passport", false);
                ListViewItem item = new();
                item.Tag = travelDocument;
                item.Content = travelDocument.GetInfo();
                lvPackingList.Items.Add(item);
            }
        }

        // ******************** METHODS ********************
        private void UpdateUI()
        {
            if (!Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()))
            {
                TravelDocument travelDocument = new("Passport", true);
                ListViewItem item = new();
                item.Tag = travelDocument;
                item.Content = travelDocument.GetInfo();
                lvPackingList.Items.Add(item);
            }

            cldEndDate.DisplayDateStart = DateTime.Now;
            cldStartDate.DisplayDateStart = DateTime.Now;

            string[] countries = Enum.GetNames(typeof(Countries));
            foreach (string country in countries)
            {
                cbCountryAddTravel.Items.Add(country.Replace('_', ' '));
            }

            cbTypeOfTravelAddTravel.Items.Add("Trip");
            cbTypeOfTravelAddTravel.Items.Add("Vacation");

            string[] tripTypes = Enum.GetNames(typeof(TripTypes));
            foreach (string tripType in tripTypes)
            {
                cbTripTypeDetailsAddTravel.Items.Add(tripType);
            }

            // Hide and disable 
            lblAllInclusive.Visibility = Visibility.Hidden;
            brdAllInclusive.Visibility = Visibility.Hidden;
            cbxAllInclusiveDetails.Visibility = Visibility.Hidden;
            cbxAllInclusiveDetails.IsChecked = false;
            cbxAllInclusiveDetails.IsEnabled = false;

            lblTypeOfTrip.Visibility = Visibility.Hidden;
            brdTripTypeDetails.Visibility = Visibility.Hidden;
            cbTripTypeDetailsAddTravel.Visibility = Visibility.Hidden;
            cbTripTypeDetailsAddTravel.SelectedItem = null;
            cbTripTypeDetailsAddTravel.IsEnabled = false;

            lblRequired.Visibility = Visibility.Hidden;
            cbxRequired.IsEnabled = false;
            brdCbxRequired.Visibility = Visibility.Hidden;

            txtQuantity.Visibility = Visibility.Hidden;
            lblQuantity.Visibility = Visibility.Hidden;

            lblDocument.Visibility = Visibility.Hidden;
            brdCbxDocument.Visibility = Visibility.Hidden;
            cbxDocument.IsEnabled = false;

            txtQuantity.IsEnabled = false;
        }
    }
}

// Remember to make it unable to remove automatically added packing list items (Passport) 