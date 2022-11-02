using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Travel
    {
        public string Destination { get; set; }
        public Countries Country { get; set; }
        public int Travellers { get; set; }
        public List<IPackingListItem> PackingList { get; set; } = new();
        public string CreatorUserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }

        public Travel(string destination, Countries country, int travellers, List<IPackingListItem> packingList, string creatorUserName, DateTime startDate, DateTime endDate)
        {
            Destination = destination;
            Country = country;
            Travellers = travellers;
            PackingList = packingList;
            CreatorUserName = creatorUserName;
            StartDate = startDate;
            EndDate = endDate;
            TravelDays = CalculateTravelDays(startDate, endDate);
        }



        // ******************** METHODS ********************
        public virtual string GetInfo()
        {
            if (TravelDays < 2)
            {
                return $"{Country.ToString().Replace('_', ' ')}, {TravelDays} day";
            }
            else
            {
                return $"{Country.ToString().Replace('_', ' ')}, {TravelDays} days";
            }
        }

        /// <summary>
        /// Method calculating the number of days between two dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>The calculated number as int</returns>
        private int CalculateTravelDays(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            int travelDays = timeSpan.Days;
            return travelDays;
        }
    }
}
