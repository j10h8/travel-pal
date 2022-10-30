using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        public Vacation(bool allInclusive, string destination, Countries country, int travellers, List<IPackingListItem> packingList, string creatorUserName, DateTime startDate, DateTime endDate) : base(destination, country, travellers, packingList, creatorUserName, startDate, endDate)
        {
            AllInclusive = allInclusive;
        }



        // ******************** METHODS ********************
        public bool GetInfo()     // OK to have this return bool instead of string? 
        {
            if (AllInclusive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
