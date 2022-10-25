using System.Windows;
using TravelPal.Managers;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserManager _userManager;

        public RegisterWindow(UserManager userManager)
        {
            _userManager = userManager;

            InitializeComponent();
        }
    }
}
