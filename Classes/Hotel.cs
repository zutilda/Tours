namespace Wpfproject
{
    public partial class Hotel
    {
        public string AllInform
        {
            get
            {
                try
                {
                    return "Название: " + Name + "; количество звезд: " + CountOfStars + "; страна: " + Country.Name + "; описание: " + Description;

                }
                catch (System.Exception)
                {
                    return "Не удалось загрузить информацию";
                    throw;
                }                     }
        }
    }
}
