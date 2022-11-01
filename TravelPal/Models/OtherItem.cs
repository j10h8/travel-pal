using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class OtherItem : IPackingListItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public OtherItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }



        // ******************** METHODS ********************
        public string GetInfo()     // TODO: Possibly change what is returned. 
        {
            return $"{Name} x {Quantity}";
        }
    }
}
