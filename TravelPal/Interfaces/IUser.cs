using TravelPal.Enums;

namespace TravelPal.Interfaces
{
    public interface IUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }


        // Method that base classes that inherit from IPackingListItem have to have 
        public string GetInfo();
    }
}
