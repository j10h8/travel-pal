using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class TravelDocument : IPackingListItem
    {
        public string Name { get; set; }
        public bool Required { get; set; }

        public TravelDocument(string name, bool required)
        {
            Name = name;
            Required = required;
        }



        // ******************** METHODS ********************
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
