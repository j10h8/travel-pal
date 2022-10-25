using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        private TravelManager _travelManager;
        private UserManager _userManager;

        public TravelsWindow(TravelManager travelManager, UserManager userManager)
        {
            _travelManager = travelManager;
            _userManager = userManager;

            InitializeComponent();
        }
    }
}
