using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class OtherItem : IPackingListItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        // Constructor to initialize OtherItem. Sets OtherItem Name and Quantity. 
        public OtherItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        // Returns string with information about OtherItem name and quantity 
        public string GetInfo()
        {
            return $"{Name} x {Quantity}";
        }
    }
}
