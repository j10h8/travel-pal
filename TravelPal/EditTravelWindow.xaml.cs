using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TravelPal.Enums;
using TravelPal.Interfaces;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for EditTravelWindow.xaml
    /// </summary>
    public partial class EditTravelWindow : Window
    {
        TravelManager _travelManager;
        UserManager _userManager;
        Travel _travel;

        // Constructor to initialize EditTravelWindow. Sets field variables and calls methods UpdateUI and InitializeComponent.
        public EditTravelWindow(UserManager userManager, TravelManager travelManager, Travel travel)
        {
            _travelManager = travelManager;
            _userManager = userManager;
            _travel = travel;

            InitializeComponent();

            UpdateUI();
        }

        // Closes window and shows a new TravelsDetailsWindow 
        private void btnCancelAddTravelWindow_Click(object sender, RoutedEventArgs e)
        {
            TravelDetailsWindow travelDetailsWindow = new(_userManager, _travelManager, _travel);
            travelDetailsWindow.Show();

            Close();
        }

        // Sets UI visibility depending on if Travel is Trip or Vacation 
        private void cbTypeOfTravelAddTravel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Trip")
            {
                lblTypeOfTrip.Visibility = Visibility.Visible;
                brdTripTypeDetails.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.Visibility = Visibility.Hidden;

                lblAllInclusive.Visibility = Visibility.Hidden;
                brdAllInclusive.Visibility = Visibility.Hidden;

            }
            else if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Vacation")
            {
                lblAllInclusive.Visibility = Visibility.Visible;
                brdAllInclusive.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.Visibility = Visibility.Visible;

                lblTypeOfTrip.Visibility = Visibility.Hidden;
                brdTripTypeDetails.Visibility = Visibility.Hidden;
            }
        }

        // Updates UI if checkbox is checked 
        private void cbxDocument_Checked(object sender, RoutedEventArgs e)
        {
            lblRequired.Visibility = Visibility.Visible;
            cbxRequired.IsEnabled = true;
            brdCbxRequired.Visibility = Visibility.Visible;

            lblQuantity.Visibility = Visibility.Hidden;
            txtQuantity.Visibility = Visibility.Hidden;
            txtQuantity.IsEnabled = false;
        }

        // Updates UI if checkbox is unchecked 
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

        // Updates UI if packinglist text is entered 
        private void txtPackingListItem_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtPackingListItem.Text.Trim().Length > 0)
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

        // Removes eventual existing passwords and adds a new one depending on user location and travel country
        private void cbCountryAddTravel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()))
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

                if (!Enum.IsDefined(typeof(EuropeanCountries), cbCountryAddTravel.SelectedItem.ToString()))
                {
                    TravelDocument travelDocument = new("Passport", true);
                    ListViewItem item = new();
                    item.Tag = travelDocument;
                    item.Content = travelDocument.GetInfo();
                    lvPackingList.Items.Add(item);
                }

                if (Enum.IsDefined(typeof(EuropeanCountries), cbCountryAddTravel.SelectedItem.ToString()) && cbCountryAddTravel.SelectedItem.ToString() != _userManager.SignedInUser.Location.ToString())
                {
                    TravelDocument travelDocument = new("Passport", false);
                    ListViewItem item = new();
                    item.Tag = travelDocument;
                    item.Content = travelDocument.GetInfo();
                    lvPackingList.Items.Add(item);
                }
            }
        }

        // Adds item to packing list listview 
        private void btnAddToPackingList_Click(object sender, RoutedEventArgs e)
        {
            if (cbxDocument.IsChecked == true)
            {
                bool isRequired = false;

                if (cbxRequired.IsChecked == true)
                {
                    isRequired = true;
                }
                else
                {
                    isRequired = false;
                }

                TravelDocument travelDocument = new(txtPackingListItem.Text.Trim(), isRequired);
                ListViewItem item = new();
                item.Tag = travelDocument;
                item.Content = travelDocument.GetInfo();
                lvPackingList.Items.Add(item);

                txtPackingListItem.Text = null;
            }
            else
            {
                try
                {
                    if (int.Parse(txtQuantity.Text.Trim()) < 1)
                    {
                        MessageBox.Show("Please enter a number corresponding to the quantity of the packing list item.");
                    }
                    else
                    {
                        OtherItem otherItem = new(txtPackingListItem.Text.Trim(), int.Parse(txtQuantity.Text.Trim()));
                        ListViewItem item = new();
                        item.Tag = otherItem;
                        item.Content = otherItem.GetInfo();
                        lvPackingList.Items.Add(item);

                        txtPackingListItem.Text = null;
                    }
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show("Please specify quantity of the packing list item.");
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Please enter a number corresponding to the quantity of the packing list item.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The input you provided is not accepted by the system. Please change the input and try again.");
                }
            }
        }

        // Updates UI if item in listview is selected 
        private void lvPackingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPackingList.SelectedItems.Count > 0)
            {
                btnRemoveFromPackingList.IsEnabled = true;
                btnRemoveFromPackingList.Visibility = Visibility.Visible;
            }
            else
            {
                btnRemoveFromPackingList.IsEnabled = false;
                btnRemoveFromPackingList.Visibility = Visibility.Hidden;
            }
        }

        // Removes item from packing list listview 
        private void btnRemoveFromPackingList_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = lvPackingList.SelectedItem as ListViewItem;
            lvPackingList.Items.Remove(lvPackingList.SelectedItem);

            if (item.Tag.GetType().Name.ToString() == "TravelDocument")
            {
                TravelDocument travelDocument = item.Tag as TravelDocument;

                if (travelDocument.Required == true)
                {
                    MessageBox.Show($"You just removed a required travel document ({travelDocument.Name})");
                }
            }
        }

        // Sets travelday visibility and calculates travel number of days 
        private void cldStartDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            int travelDays = 0;

            if (cldStartDate.SelectedDate != null && cldEndDate.SelectedDate != null)
            {
                lblTravelDays.Visibility = Visibility.Visible;

                DateTime endDate = (DateTime)cldEndDate.SelectedDate;
                DateTime startDate = (DateTime)cldStartDate.SelectedDate;
                TimeSpan timeSpan = endDate - startDate;
                travelDays = timeSpan.Days;
                txtTravelLengthAddTravel.Text = travelDays.ToString();
            }

            if (travelDays < 0)
            {
                MessageBox.Show("The specified start date comes after the specified end date. Please change!");
            }
        }

        // Sets travelday visibility and calculates travel number of days 
        private void cldEndDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            int travelDays = 0;

            if (cldStartDate.SelectedDate != null && cldEndDate.SelectedDate != null)
            {
                lblTravelDays.Visibility = Visibility.Visible;

                DateTime endDate = (DateTime)cldEndDate.SelectedDate;
                DateTime startDate = (DateTime)cldStartDate.SelectedDate;
                TimeSpan timeSpan = endDate - startDate;
                travelDays = timeSpan.Days;
                txtTravelLengthAddTravel.Text = travelDays.ToString();
            }

            if (travelDays < 0)
            {
                MessageBox.Show("The specified start date comes after the specified end date. Please change!");
            }

        }

        // Sets travel length colour to red if travel days is a negative number 
        private void txtTravelLengthAddTravel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Convert.ToInt32(txtTravelLengthAddTravel.Text) < 0)
            {
                txtTravelLengthAddTravel.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                txtTravelLengthAddTravel.Foreground = new SolidColorBrush(Colors.MediumSlateBlue);
            }
        }

        // Removes old travel and adds new travel based on the updated information
        private void btnUpdateTravel_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs() == "OK")
            {
                try
                {
                    string creatorUserName = _travel.CreatorUserName;

                    _travelManager.RemoveTravel(_travel);

                    foreach (IUser user in _userManager.Users)
                    {
                        if (user.UserName == _travel.CreatorUserName && user.GetType().Name.ToString() == "User")
                        {
                            User identifiedUser = user as User;
                            identifiedUser.Travels.Remove(_travel);
                        }
                    }

                    List<IPackingListItem> packingList = new();

                    foreach (ListViewItem listViewItem in lvPackingList.Items)
                    {
                        if (listViewItem.Tag.GetType().Name.ToString() == "TravelDocument")
                        {
                            TravelDocument travelDocument = listViewItem.Tag as TravelDocument;
                            packingList.Add(travelDocument);
                        }
                        else
                        {
                            OtherItem otherItem = listViewItem.Tag as OtherItem;
                            packingList.Add(otherItem);
                        }
                    }

                    if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Trip")
                    {
                        Trip trip = new(((TripTypes)Enum.Parse(typeof(TripTypes), cbTripTypeDetailsAddTravel.SelectedItem.ToString())), txtDestinationAddTravel.Text.Trim(), ((Countries)Enum.Parse(typeof(Countries), cbCountryAddTravel.SelectedItem.ToString().Replace(' ', '_'))), int.Parse(txtTravellersAddTravel.Text.Trim()), packingList, creatorUserName, (DateTime)cldStartDate.SelectedDate, (DateTime)cldEndDate.SelectedDate);

                        if (_userManager.SignedInUser.GetType().Name.ToString() == "Admin")
                        {
                            _travelManager.Travels.Add(trip);
                        }
                        else
                        {
                            User user = _userManager.SignedInUser as User;
                            _travelManager.Travels.Add(trip);
                            user.Travels.Add(trip);
                        }

                        _travel = trip;
                    }
                    else
                    {
                        bool allInclusive = false;

                        if (cbxAllInclusiveDetails.IsChecked == true)
                        {
                            allInclusive = true;
                        }

                        Vacation vacation = new(allInclusive, txtDestinationAddTravel.Text.Trim(), ((Countries)Enum.Parse(typeof(Countries), cbCountryAddTravel.SelectedItem.ToString().Replace(' ', '_'))), int.Parse(txtTravellersAddTravel.Text.Trim()), packingList, creatorUserName, (DateTime)cldStartDate.SelectedDate, (DateTime)cldEndDate.SelectedDate);

                        if (_userManager.SignedInUser.GetType().Name.ToString() == "Admin")
                        {
                            _travelManager.Travels.Add(vacation);
                        }
                        else
                        {
                            User user = _userManager.SignedInUser as User;
                            _travelManager.Travels.Add(vacation);
                            user.Travels.Add(vacation);
                        }

                        _travel = vacation;
                    }

                    MessageBox.Show("The travel has been updated!");

                    TravelDetailsWindow travelDetailsWindow = new(_userManager, _travelManager, _travel);
                    travelDetailsWindow.Show();

                    Close();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Please enter a number corresponding to the number of travellers.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The input you provided is not accepted by the system. Please change the input and try again.");
                }
            }
            else
            {
                MessageBox.Show(CheckInputs());
            }
        }



        // Updates the UI 
        private void UpdateUI()
        {
            // Travel destination 
            txtDestinationAddTravel.Text = _travel.Destination;

            // Travel no. of travellers 
            txtTravellersAddTravel.Text = _travel.Travellers.ToString();

            // Travel start and and date 
            cldEndDate.SelectedDate = _travel.EndDate;
            cldStartDate.SelectedDate = _travel.StartDate;

            // Populate cbCountries 
            string[] countries = Enum.GetNames(typeof(Countries));
            foreach (string country in countries)
            {
                cbCountryAddTravel.Items.Add(country.Replace('_', ' '));
            }

            // Travel country 
            cbCountryAddTravel.SelectedItem = _travel.Country.ToString().Replace('_', ' ');

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

            // Trip type 
            // Populate cbTripType 
            string[] tripTypes = Enum.GetNames(typeof(TripTypes));
            foreach (string tripType in tripTypes)
            {
                cbTripTypeDetailsAddTravel.Items.Add(tripType);
            }
            // if Trip 
            if (_travel.GetType().Name.ToString() == "Trip")
            {
                Trip trip = _travel as Trip;

                // Travel trip type 
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
            if (!Enum.IsDefined(typeof(EuropeanCountries), _userManager.SignedInUser.Location.ToString()))
            {
                TravelDocument travelDocument = new("Passport", true);
                ListViewItem item = new();
                item.Tag = travelDocument;
                item.Content = travelDocument.GetInfo();
                lvPackingList.Items.Add(item);
            }
            foreach (IPackingListItem packingListItem in _travel.PackingList)
            {
                if (packingListItem.GetInfo() != "Passport (required)" && packingListItem.GetInfo() != "Passport (not required)")
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = packingListItem;
                    item.Content = packingListItem.GetInfo();
                    lvPackingList.Items.Add(item);
                }
            }

            // Travel dates/calendar 
            lblTravelDays.Visibility = Visibility.Visible;
            cldEndDate.DisplayDate = _travel.EndDate;
            cldStartDate.DisplayDate = _travel.StartDate;
            DateTime endDate = _travel.EndDate;
            DateTime startDate = _travel.StartDate;
            TimeSpan timeSpan = endDate - startDate;
            int travelDays = timeSpan.Days;
            txtTravelLengthAddTravel.Text = travelDays.ToString();

            // Hide and disable 
            lblAllInclusive.Visibility = Visibility.Hidden;
            brdAllInclusive.Visibility = Visibility.Hidden;
            cbxAllInclusiveDetails.Visibility = Visibility.Hidden;

            lblTypeOfTrip.Visibility = Visibility.Hidden;
            brdTripTypeDetails.Visibility = Visibility.Hidden;

            lblRequired.Visibility = Visibility.Hidden;
            brdCbxRequired.Visibility = Visibility.Hidden;

            txtQuantity.Visibility = Visibility.Hidden;
            lblQuantity.Visibility = Visibility.Hidden;

            lblDocument.Visibility = Visibility.Hidden;
            brdCbxDocument.Visibility = Visibility.Hidden;

            btnRemoveFromPackingList.Visibility = Visibility.Hidden;

            if (_travel.GetType().Name.ToString() == "Trip")
            {
                lblTypeOfTrip.Visibility = Visibility.Visible;
                brdTripTypeDetails.Visibility = Visibility.Visible;

                lblAllInclusive.Visibility = Visibility.Hidden;
                brdAllInclusive.Visibility = Visibility.Hidden;
            }
            else
            {
                lblAllInclusive.Visibility = Visibility.Visible;
                brdAllInclusive.Visibility = Visibility.Visible;
                cbxAllInclusiveDetails.Visibility = Visibility.Visible;

                lblTypeOfTrip.Visibility = Visibility.Hidden;
                brdTripTypeDetails.Visibility = Visibility.Hidden;
            }
        }

        // Checks input and returns error codes for adding a travel
        private string CheckInputs()
        {
            if (txtDestinationAddTravel.Text.Trim().Length == 0)
            {
                return "Please specify destination.";
            }
            else if (cbCountryAddTravel.SelectedItem == null)
            {
                return "Please select destination country.";
            }
            else if (!int.TryParse((txtTravellersAddTravel.Text.Trim()), out int success) || Convert.ToInt32(txtTravellersAddTravel.Text.Trim()) < 1)
            {
                if (txtTravellersAddTravel.Text.Trim().Length > 0)
                {
                    return "Please enter number corresponding to the number of travellers.";
                }
                else
                {
                    return "Please specify the number of travellers.";
                }
            }
            else if (cbTypeOfTravelAddTravel.SelectedItem == null)
            {
                return "Please select type of travel.";
            }
            else if (cbTypeOfTravelAddTravel.SelectedItem.ToString() == "Trip" && cbTripTypeDetailsAddTravel.SelectedItem == null)
            {
                return "Please select type of trip.";
            }
            else if (!cldStartDate.SelectedDate.HasValue)
            {
                return "Please select start date.";
            }
            else if (!cldEndDate.SelectedDate.HasValue)
            {
                return "Please select end date.";
            }
            else if (int.Parse(txtTravelLengthAddTravel.Text) < 0)
            {
                return "The specified start date comes after the specified end date. Please change!";
            }

            return "OK";
        }
    }
}
