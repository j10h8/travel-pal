using System.Windows;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        // Constructor to initialize InfoWindow object. Calls method InitializeComponent
        public InfoWindow()
        {
            InitializeComponent();
        }

        // ******************** EVENTS *********************
        private void btnCloseInfoWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
