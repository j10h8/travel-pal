using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Trip : Travel
    {
        public TripTypes Type { get; set; }

        public Trip(TripTypes type, string destination, Countries country, int travellers, List<IPackingListItem> packingList, string creatorsUserName, DateTime startDate, DateTime endDate) : base(destination, country, travellers, packingList, creatorsUserName, startDate, endDate)
        {
            Type = type;
        }



        // ******************** METHODS ********************
        public string GetInfo()
        {
            return Type.ToString();
        }
    }
}
