using System;
using System.Windows;
using TravelPal.Enums;
using TravelPal.Managers;

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
                // Show and set IsEnabled
                lblTypeOfTrip.Visibility = Visibility.Visible;
                brdTripTypeDetails.Visibility = Visibility.Visible;
                cbTripTypeDetailsAddTravel.Visibility = Visibility.Visible;
                cbTripTypeDetailsAddTravel.IsEnabled = true;

                // Hide and set IsEnabled
                lblAllInclusive.Visibility = Visibility.Hidden;
                brdAllInclusive.Visibility = Visibility.Hidden;
                cbxAllInclusiveDetails.Visibility = Visibility.Hidden;
                cbxAllInclusiveDetails.IsEnabled = false;

            }
            else if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Vacation")
            {
                // Show and set IsEnabled
                lblAllInclusive.Visibility = Visibility.Visible;
                brdAllInclusive.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.IsChecked = false;
                cbxAllInclusiveDetails.IsEnabled = true;

                // Hide and set IsEnabled
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
            //Om resan går till ett land utanför EU och användaren bor inom EU ska ett TravelDocument med name
            //“Passport” (med required satt till true) automatiskt läggas till i packlistan
            //Om resan går till ett annat land, inom EU och användaren bor inom EU, ska ett TravelDocument med
            //name “Passport” (med required satt till false) automatiskt läggas till i packlistan
            //Om användaren ändrar destinationsland under tiden ska required för passet ändras därefter
            //Om användaren bor utanför EU, oavsett destinationsland, ska ett pass(med required true) läggas till
            //automatiskt
        }

        // ******************** METHODS ********************
        private void UpdateUI()
        {
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

            // Hide and diasable 
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
