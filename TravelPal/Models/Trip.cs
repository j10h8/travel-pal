using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Trip : Travel
    {
        public TripTypes Type { get; set; }

        public Trip(TripTypes type, string destination, Countries country, int travellers, List<IPackingListItem> packingList, DateTime startDate, DateTime endDate, int travelDays) : base(destination, country, travellers, packingList, startDate, endDate, travelDays)
        {
            Type = type;
        }

        public string GetInfo()
        {
            return Type.ToString();
        }
    }
}
