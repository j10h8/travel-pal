namespace TravelPal.Interfaces
{
    public interface IPackingListItem
    {
        public string Name { get; set; }


        // Method that base classes that inherit from IPackingListItem have to have 
        public string GetInfo();
    }
}
