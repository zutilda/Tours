using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Wpfproject.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditHotel.xaml
    /// </summary>
    public partial class AddOrEditHotel : Window
    {
        private Hotel HOTEL;
        private readonly bool FlagCreate;

        public AddOrEditHotel()
        {
            InitializeComponent();

            cbCountry.ItemsSource = BaseClass.BD.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";

            FlagCreate = true;
        }

        public AddOrEditHotel(Hotel hotel)
        {
            InitializeComponent();

            btnAdd.Content = "Изменить";

            cbCountry.ItemsSource = BaseClass.BD.Country.ToList();
            cbCountry.SelectedValuePath = "Code";
            cbCountry.DisplayMemberPath = "Name";

            HOTEL = hotel;
            FlagCreate = false;

            tboxName.Text = hotel.Name;
            tboxStars.Text = hotel.CountOfStars.ToString();
            cbCountry.SelectedValue = hotel.CountryCode;
            tboxDescription.Text = hotel.Description;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                if (FlagCreate)
                {
                    HOTEL = new Hotel();
                }

                HOTEL.Name = tboxName.Text;
                HOTEL.CountOfStars = Convert.ToInt32(tboxStars.Text);
                HOTEL.CountryCode = cbCountry.SelectedValue.ToString();
                HOTEL.Description = tboxDescription.Text;

                if (FlagCreate)
                {
                    _ = BaseClass.BD.Hotel.Add(HOTEL);
                }
                _ = BaseClass.BD.SaveChanges();

                _ = FlagCreate ? MessageBox.Show("Отель успешно добавлен") : MessageBox.Show("Отель успешно изменён");

                Close();
            }
        }

        private bool CheckData()
        {
            if (tboxName.Text.Length == 0)
            {
                _ = MessageBox.Show("Поле \"Название\" не должно быть пустым");
                return false;
            }
            else if (!Regex.IsMatch(tboxStars.Text, @"^[0-5]$"))
            {
                _ = MessageBox.Show("Количество звёзд может быть от 0 до 5!");
                return false;
            }
            else if (cbCountry.SelectedIndex == -1)
            {
                _ = MessageBox.Show("Выберите страну");
                return false;
            }
            else if (tboxDescription.Text.Length == 0)
            {
                _ = MessageBox.Show("Поле \"Описание\" не должно быть пустым");
                return false;
            }

            return true;
        }
    }
}