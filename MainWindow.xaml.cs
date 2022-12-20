using System.Windows;
using Wpfproject;
using Wpfproject.Classes;
using Wpfproject.Pages;

namespace WpfAppWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.MFrame = fMain;
            _ = MainFrame.MFrame.Navigate(new PageMain());
            BaseClass.BD = new Entities();
        }
    }
}
