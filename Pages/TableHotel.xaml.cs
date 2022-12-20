using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpfproject.Classes;

namespace Wpfproject.Pages
{
    /// <summary>
    /// Логика взаимодействия для TableHotel.xaml
    /// </summary>
    public partial class TableHotel : Page
    {
        private readonly Pagination Pagin = new Pagination();
        private List<Hotel> HotelList = new List<Hotel>();
        public TableHotel()
        {
            InitializeComponent();
            DataContext = Pagin;
            HotelList = BaseClass.BD.Hotel.ToList();
            ListHotel.ItemsSource = HotelList;

            tboxPageCount.Text = "10";

        }

        private void EditingCurrentPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)
            {
                case "first":
                    Pagin.CurrentPage = 1;
                    break;
                case "last":
                    Pagin.CurrentPage = HotelList.Count;
                    break;
                case "back":
                    Pagin.CurrentPage--;
                    break;
                case "next":
                    Pagin.CurrentPage++;
                    break;
                default:
                    Pagin.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }

            ListHotel.ItemsSource = HotelList.Skip((Pagin.CurrentPage * Pagin.CountPage) - Pagin.CountPage).Take(Pagin.CountPage).ToList();
        }


        private void tboxPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetPagination();
        }

        private void SetPagination()
        {
            try
            {
                if (Convert.ToInt32(tboxPageCount.Text) > 0)
                {
                    Pagin.CountPage = Convert.ToInt32(tboxPageCount.Text);
                }
            }
            catch
            {
                Pagin.CountPage = HotelList.Count;
            }

            Pagin.CountList = HotelList.Count;
            ListHotel.ItemsSource = HotelList.Skip(0).Take(Pagin.CountPage).ToList();



        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new PageMain());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditHotel addHotel = new AddOrEditHotel();
            _ = addHotel.ShowDialog();
            HotelList = BaseClass.BD.Hotel.ToList();
            SetPagination();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);
            Hotel hotel = BaseClass.BD.Hotel.FirstOrDefault(x => x.Id == id);
            foreach (Tour item1 in BaseClass.BD.Tour)
            {
                if (hotel.Tour == item1 && item1.IsActual == true)
                {
                    _ = MessageBox.Show("Тур активен и отель пытаются удалить");
                    return;
                }
            }
            string nameHotel = hotel.Name;
            foreach (HotelImage item in BaseClass.BD.HotelImage.Where(x => x.HotelId == id))
            {
                _ = BaseClass.BD.HotelImage.Remove(item);
            }
            _ = BaseClass.BD.Hotel.Remove(hotel);
            _ = BaseClass.BD.SaveChanges();
            _ = MessageBox.Show("Был удален отель: " + nameHotel);
            HotelList = BaseClass.BD.Hotel.ToList();
            ListHotel.ItemsSource = HotelList;
        }



    }
}
