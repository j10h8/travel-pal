using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Managers
{
    public class UserManager
    {
        public List<IUser> Users { get; set; } = new();
        public IUser SignedInUser { get; set; }
        public IUser TemporaryUser { get; set; }

        // Adds user if user name is not already registered in Users list 
        public bool AddUser(IUser user)
        {
            if (ValidateUserName(user.UserName))
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

        // Checks if user name is already registered in Users list 
        public bool ValidateUserNameUpdate(string newUserName)
        {
            foreach (IUser user in Users)
            {
                if (user.UserName == newUserName)
                {
                    return false;
                }
            }

            return true;
        }

        // Checks if user name is already registered in Users list 
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

        // Removes user from Users list 
        public void RemoveUser(IUser user)
        {
            Users.Remove(user);
        }

        // Returns true if provided user name and associated password exist in Users list. Otherwise false. 
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

        // Updates user name if user name is not already registered in Users list
        public bool UpdateUserName(IUser user, string userName)
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

        // Updates user password 
        public void UpdatePassword(IUser user, string password)
        {
            user.Password = password;
        }

        // Updates user location 
        public void UpdateLocation(IUser user, Countries location)
        {
            user.Location = location;
        }
    }
}
