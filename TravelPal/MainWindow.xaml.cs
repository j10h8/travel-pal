using System;
using System.Collections.Generic;
using System.Windows;
using TravelPal.Enums;
using TravelPal.Interfaces;
using TravelPal.Managers;
using TravelPal.Models;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager _userManager;
        TravelManager _travelManager;

        // Constructor that initializes UserManager and TravelManager, sets field variables, and calls methods AddGandalf, AddAdmin, and InitializeComponent. 
        public MainWindow()
        {
            InitializeComponent();

            _userManager = new();
            _travelManager = new();

            AddGandalf();
            AddAdmin();
        }

        // Constructor to initialize MainWindow object. Sets field variables and UserManager SignedInUser, calls method InitializeComponent. 
        public MainWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();

            _userManager = userManager;
            _travelManager = travelManager;

            _userManager.SignedInUser = null;
        }

        // ******************** EVENTS *********************
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0 || pbPassword.Password.Trim().Length == 0)
            {
                MessageBox.Show("Please enter username and password!");
            }
            else if (!_userManager.SignInUser(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
            {
                MessageBox.Show("User name and/or password do/does not exist!");
            }
            else if (_userManager.SignInUser(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
            {
                TravelsWindow travelsWindow = new(_userManager, _travelManager);
                travelsWindow.Show();

                Close();
            }
        }

        private void btnToRegisterWindow_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new(_userManager, _travelManager);
            registerWindow.Show();

            Close();
        }

        private void btnCloseApplication_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // ******************** METHODS ********************
        public void AddGandalf()
        {
            User user = new("Gandalf", "password", Countries.Sweden);

            // Create Gandalf's vacation
            List<IPackingListItem> packingList = new();
            TravelDocument travelDocument = new("Passport", false);
            OtherItem otherItem = new("Magic wand", 1);
            packingList.Add(travelDocument);
            packingList.Add(otherItem);
            DateTime startDate = new DateTime(2023, 2, 4);
            DateTime endDate = new DateTime(2023, 2, 12);
            Vacation vacation = new(true, "Copenhagen", Countries.Denmark, 1, packingList, user.UserName, startDate, endDate);

            // Create Gandalf's trip
            List<IPackingListItem> packingList2 = new();
            TravelDocument travelDocument2 = new("Passport", true);
            TravelDocument travelDocument3 = new("Contract papers", true);
            OtherItem otherItem2 = new("Toothbrush", 1);
            OtherItem otherItem2b = new("Toothpaste", 1);
            packingList2.Add(travelDocument2);
            packingList2.Add(travelDocument3);
            packingList2.Add(otherItem2);
            packingList2.Add(otherItem2b);
            DateTime startDate2 = new DateTime(2022, 12, 10);
            DateTime endDate2 = new DateTime(2022, 12, 18);
            Trip trip = new(TripTypes.Work, "New Delhi", Countries.India, 1, packingList2, user.UserName, startDate2, endDate2);

            user.Travels.Add(vacation);
            user.Travels.Add(trip);
            _userManager.Users.Add(user);

            _travelManager.AddTravel(vacation);
            _travelManager.AddTravel(trip);
        }

        public void AddAdmin()
        {
            Admin admin = new("admin", "password", Countries.Sweden);

            _userManager.Users.Add(admin);
        }
    }
}
