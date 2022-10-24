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
    }
}
