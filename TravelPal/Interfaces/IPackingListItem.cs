namespace TravelPal.Interfaces
{
    public interface IPackingListItem
    {
        public string Name { get; set; }

        public string GetInfo();
    }
}
