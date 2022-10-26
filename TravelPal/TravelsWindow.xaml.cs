using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        TravelManager _travelManager;
        UserManager _userManager;

        public TravelsWindow(UserManager userManager, TravelManager travelManager)
        {
            _travelManager = travelManager;
            _userManager = userManager;

            InitializeComponent();
        }

        // ******************** EVENTS *********************

        // ******************** METHODS ********************
    }
}
