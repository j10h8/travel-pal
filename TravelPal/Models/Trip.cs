using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Trip : Travel
    {
        public TripTypes Type { get; set; }

        // Constructor to initialize Trip. Sets Trip Type and inherited Travel properties. 
        public Trip(TripTypes type, string destination, Countries country, int travellers, List<IPackingListItem> packingList, string creatorUserName, DateTime startDate, DateTime endDate) : base(destination, country, travellers, packingList, creatorUserName, startDate, endDate)
        {
            Type = type;
        }

        // Returns one of two strings (depending on Travel TravelDays) with information about Country and TravelDays. 
        public string GetInfo()
        {
            if (base.TravelDays < 2)
            {
                return $"{base.Country}, {base.TravelDays} day";
            }
            else
            {
                return $"{base.Country}, {base.TravelDays} days";
            }
        }
    }
}
