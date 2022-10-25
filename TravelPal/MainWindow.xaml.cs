using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager userManager = new();

        public MainWindow()
        {
            InitializeComponent();
            userManager.AddGandalf();
        }
    }
}
