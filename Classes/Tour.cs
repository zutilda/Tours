using System.Globalization;
using System.Windows.Media;

namespace Wpfproject
{
    public partial class Tour
    {
        public string TicetsCount => "Количество билетов: " + TicketCount;

        public string Costes => Price.ToString("N0", new CultureInfo("en-us")) + " руб.";

        public string Actuality => IsActual ? "Актуален" : "Неактуален";
        public Color ActualityColor => IsActual ? Color.FromRgb(55, 255, 55) : Color.FromRgb(255, 55, 55);
    }
}
