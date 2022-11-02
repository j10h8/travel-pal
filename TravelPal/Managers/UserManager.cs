using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Managers
{
    public class UserManager
    {
        public List<IUser> Users { get; set; } = new();
        public IUser SignedInUser { get; set; }



        // ******************** METHODS ********************
        public bool AddUser(IUser user)
        {
            if (ValidateUserName(user.UserName))        // TODO: Check if return pattern behaves as espected. Check if the bool is intended to be used in this manner.
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

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
            Users.Remove(user);
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

        public void UpdatePassword(IUser user, string password)
        {
            user.Password = password;
        }

        public void UpdateLocation(IUser user, Countries location)
        {
            user.Location = location;
        }
    }
}
