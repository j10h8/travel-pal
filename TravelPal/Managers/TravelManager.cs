using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public class TravelManager
    {
        public List<Travel> Travels { get; set; } = new();

        // Adds a Travel to Travels list 
        public void AddTravel(Travel travel)
        {
            Travels.Add(travel);
        }

        // Removes a Travel from Travels list 
        public void RemoveTravel(Travel travel)
        {
            Travels.Remove(travel);
        }
    }
}
