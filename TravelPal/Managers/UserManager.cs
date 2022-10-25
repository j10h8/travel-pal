using System.Collections.Generic;
using TravelPal.Interfaces;

namespace TravelPal.Managers
{
    public class UserManager
    {
        public List<IUser> Users { get; set; }
        public IUser SignedInUser { get; set; }

        public bool AddUser(IUser user)
        {
            foreach (IUser listItem in Users)           // TODO: Check if return pattern behaves as espected. Check if the bool is intended to be used in this manner. 
            {
                if (listItem.UserName == user.UserName)
                {
                    return false;
                }
            }

            Users.Add(user);
            return true;
        }

        public void RemoveUser(IUser user)
        {
            Users.Remove(user);         // TODO: Check if a foreach is needed here.
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

        public bool SignInUser(string userName, string password)
        {
            foreach (IUser user in Users)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
