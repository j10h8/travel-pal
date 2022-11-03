using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Admin : IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }

        public Admin(string userName, string password, Countries location)
        {
            UserName = userName;
            Password = password;
            Location = location;
        }

        public string GetInfo()
        {
            return $"{UserName} | Location: {Location.ToString()} | Password: {Password}";
        }
    }
}
