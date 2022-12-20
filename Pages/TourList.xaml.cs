using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wpfproject.Classes;

namespace Wpfproject.Pages
{
    /// <summary>
    /// Логика взаимодействия для TourList.xaml
    /// </summary>
    public partial class TourList : Page
    {
        public TourList()
        {
            InitializeComponent();
            ListTour.ItemsSource = BaseClass.BD.Tour.ToList();
            List<Type> typesList = new List<Type>
            {
                new Type()
                {
                    Id = 0,
                    Name = "Все типы",
                    Description = null
                }
            };

            foreach (Type item in BaseClass.BD.Type)
            {
                typesList.Add(item);
            }

            CbType.ItemsSource = typesList;
            CbType.SelectedValuePath = "Id";
            CbType.DisplayMemberPath = "Name";
            CbType.SelectedIndex = 0;
            CbSort.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            _ = MainFrame.MFrame.Navigate(new PageMain());
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void TBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void chBActual_Click(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            List<Tour> listTour = new List<Tour>();

            if ((int)CbType.SelectedValue != 0)
            {
                List<TypeOfTour> typesList = BaseClass.BD.TypeOfTour.Where(x => x.TypeId == (int)CbType.SelectedValue).ToList();
                foreach (TypeOfTour item in typesList)
                {
                    listTour.Add(BaseClass.BD.Tour.FirstOrDefault(x => x.Id == item.TourId));
                }
            }
            else
            {
                listTour = BaseClass.BD.Tour.ToList();
            }

            if (TBoxFind.Text.Length > 0)
            {
                List<Tour> tempList = new List<Tour>();

                for (int i = 0; i < listTour.Count; i++)
                {
                    bool already = true;

                    if (listTour[i].Name.ToLower().Contains(TBoxFind.Text.ToLower()))
                    {
                        tempList.Add(listTour[i]);
                        already = false;
                    }

                    if (listTour[i].Description != null && listTour[i].Description.ToLower().Contains(TBoxFind.Text.ToLower()) && already)
                    {
                        tempList.Add(listTour[i]);
                    }
                }

                listTour = tempList;
            }

            if ((bool)chBActual.IsChecked)
            {
                listTour = listTour.Where(x => x.IsActual).ToList();
            }

            switch (CbSort.SelectedIndex)
            {
                case 1:
                    listTour.Sort((x, y) => x.Price.CompareTo(y.Price));
                    break;
                case 2:
                    listTour.Sort((x, y) => x.Price.CompareTo(y.Price));
                    listTour.Reverse();
                    break;
            }

            ListTour.ItemsSource = listTour;

            decimal cost = 0;
            foreach (Tour item in listTour)
            {
                cost += item.Price * item.TicketCount;
            }


            tbCost.Text = cost.ToString("N0", new CultureInfo("en-us")) + " руб.";
        }

    }
}
