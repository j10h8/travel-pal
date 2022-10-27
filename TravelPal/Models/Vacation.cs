using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        public Vacation(bool allInclusive, string destination, Countries country, int travellers, List<IPackingListItem> packingList, string creatorsUserName, DateTime startDate, DateTime endDate) : base(destination, country, travellers, packingList, creatorsUserName, startDate, endDate)
        {
            AllInclusive = allInclusive;
        }



        // ******************** METHODS ********************
        public string GetInfo()     // Change this to return bool instead? 
        {
            if (AllInclusive)
            {
                return "All inclusive!";
            }
            else
            {
                return "Not all inclusive";
            }
        }
    }
}
