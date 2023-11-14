using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class User : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }
        public List<Travel> Travels { get; set; } = new();

        // Constructor to initialize User. Sets User UserName, Password, and Location. 
        public User(string userName, string password, Countries location)
        {
            UserName = userName;
            Password = password;
            Location = location;
        }

        // Returns User Travels list 
        public List<Travel> GetUserTravelList()
        {
            return Travels;
        }

        // Returns string with information about User UserName, Location, and Password. 
        public string GetInfo()
        {
            return $"{UserName} | Location: {Location.ToString().Replace('_', ' ')} | Password: {Password}";
        }
    }
}
