using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class TravelDocument : IPackingListItem
    {
        public string Name { get; set; }
        public bool Required { get; set; }


        // Constructor to initialize TravelDocument object. Sets TravelDocument Name and Required. 
        public TravelDocument(string name, bool required)
        {
            Name = name;
            Required = required;
        }


        // ******************** METHODS ********************

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
