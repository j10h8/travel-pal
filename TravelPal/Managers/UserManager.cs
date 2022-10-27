using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public class UserManager
    {
        public List<IUser> Users { get; set; } = new();
        public IUser SignedInUser { get; set; }

        public bool AddUser(IUser user)
        {
            if (ValidateUserName(user.UserName))        // TODO: Check if return pattern behaves as espected. Check if the bool is intended to be used in this manner.
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

        private bool ValidateUserName(string userName)
        {
            foreach (IUser user in Users)
            {
                if (user.UserName == userName)
                {
                    return false;
                }
            }

            return true;
        }

        public void RemoveUser(IUser user)
        {
            Users.Remove(user);         // TODO: Check if a foreach is needed here.
        }

        public bool SignInUser(string userName, string password)
        {
            foreach (IUser user in Users)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    SignedInUser = user;

                    return true;
                }
            }

            return false;
        }

        public bool UpdateUserName(IUser user, string userName)     // TODO: Implement logic (in calling class or window XAML) to restrict usernames with 3 or less characters prior to method call/btnSave (e.g. if (lvText.Count < 3) MessageBox.Show("Warning message...")).
        {
            if (ValidateUserName(userName))
            {
                user.UserName = userName;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdatePassword(IUser user, string password)
        {
            user.Password = password;
        }

        public void UpdateLocation(IUser user, Countries location)
        {
            user.Location = location;
        }

        public void AddGandalf()
        {
            User user = new("Gandalf", "password", Countries.Sweden);

            TravelManager travelManager = new();

            // Create Gandalf's vacation
            List<IPackingListItem> packingList = new();
            TravelDocument travelDocument = new("Passport", true);
            OtherItem otherItem = new("Magic wand", 1);
            packingList.Add(travelDocument);
            packingList.Add(otherItem);
            DateTime startDate = new DateTime(2022, 12, 4);
            DateTime endDate = new DateTime(2022, 12, 5);
            Vacation vacation = new(true, "Kingston", Countries.Jamaica, 1, packingList, startDate, endDate);

            // Create Gandalf's trip
            List<IPackingListItem> packingList2 = new();
            TravelDocument travelDocument2 = new("Passport", false);
            OtherItem otherItem2 = new("Toothbrush", 1);
            OtherItem otherItem2b = new("Toothpaste", 1);
            packingList2.Add(travelDocument2);
            packingList2.Add(otherItem2);
            packingList2.Add(otherItem2b);
            DateTime startDate2 = new DateTime(2022, 12, 19);
            DateTime endDate2 = new DateTime(2022, 12, 30);
            Trip trip = new(TripTypes.Work, "Madrid", Countries.Spain, 1, packingList2, startDate2, endDate2);

            user.Travels.Add(vacation);
            user.Travels.Add(trip);
            Users.Add(user);

            travelManager.Travels.Add(vacation);
            travelManager.Travels.Add(trip);
        }

        public void AddAdmin()
        {
            Admin admin = new("admin", "password", Countries.Sweden);

            Users.Add(admin);
        }
    }
}
