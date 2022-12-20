using System.Windows;
using System.Windows.Controls;
using Wpfproject.Classes;

namespace Wpfproject.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void btnTour_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new TourList());
        }

        private void btnHotel_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new TableHotel());
        }
    }
}
