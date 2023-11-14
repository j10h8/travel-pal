using TravelPal.Enums;

namespace TravelPal.Interfaces
{
    public interface IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }

        // Method that classes that inherit from IUser have to have 
        public string GetInfo();
    }
}
