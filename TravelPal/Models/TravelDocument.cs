using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class TravelDocument : IPackingListItem
    {
        public string Name { get; set; }
        public bool Required { get; set; }

        // Constructor to initialize TravelDocument. Sets TravelDocument Name and Required. 
        public TravelDocument(string name, bool required)
        {
            Name = name;
            Required = required;
        }

        // Returns one of two strings (depending on TravelDocument bool Required) with information about TravelDocument Name 
        public string GetInfo()
        {
            if (Required)
            {
                return $"{Name} (required)";
            }
            else
            {
                return $"{Name} (not required)";
            }
        }
    }
}
