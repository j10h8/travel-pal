using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        // Constructor to initialize Vacation. Sets Vacation AllInclusive and inherited Travel properties. 
        public Vacation(bool allInclusive, string destination, Countries country, int travellers, List<IPackingListItem> packingList, string creatorUserName, DateTime startDate, DateTime endDate) : base(destination, country, travellers, packingList, creatorUserName, startDate, endDate)
        {
            AllInclusive = allInclusive;
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
