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
        public string GetInfo()
        {
            if (AllInclusive)
            {
                return $"Destination: {Destination} | Country: {Country} | Trip duration: {TravelDays} | All inclusive";
            }
            else
            {
                return $"Destination: {Destination} | Country: {Country} | Trip duration: {TravelDays} | Not all inclusive";
            }
        }
    }
}
