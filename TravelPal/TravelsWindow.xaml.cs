using System.Windows;
using TravelPal.Interfaces;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        public TravelManager TravelManager { get; }
        public UserManager UserManager { get; }
        public IUser SignedInUser { get; }

        public TravelsWindow(TravelManager travelManager, UserManager userManager, IUser signedInUser)
        {
            TravelManager = travelManager;
            UserManager = userManager;
            SignedInUser = signedInUser;

            InitializeComponent();
        }
    }
}
