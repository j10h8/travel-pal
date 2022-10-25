using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public class TravelManager
    {
        public List<Travel> Travels { get; set; } = new();

        public void AddTravel(Travel travel)
        {
            this.Travels.Add(travel);
        }

        public void RemoveTravel(Travel travel)
        {
            this.Travels.Remove(travel);
        }
    }
}
