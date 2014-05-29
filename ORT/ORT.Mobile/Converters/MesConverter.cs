using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ORT.Mobile.Converters
{
    public class MesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // value is the data from the source object.
            DateTime thisdate = DateTime.Parse(value.ToString(), null, DateTimeStyles.RoundtripKind);
            
            int monthnum = thisdate.Month;
            
            string month;
            switch (monthnum)
            {
                case 1:
                    month = "Enero";
                    break;
                case 2:
                    month = "Febrero";
                    break;
                case 3:
                    month = "Marzo";
                    break;
                case 4:
                    month = "Abril";
                    break;
                case 5:
                    month = "Mayo";
                    break;
                case 6:
                    month = "Junio";
                    break;
                case 7:
                    month = "Julio";
                    break;
                case 8:
                    month = "Agosto";
                    break;
                case 9:
                    month = "Septiembre";
                    break;
                case 10:
                    month = "Octubre";
                    break;
                case 11:
                    month = "Noviembre";
                    break;
                case 12:
                    month = "Diciembre";
                    break;
                default:
                    month = "";
                    break;
            }

            // Return the value to pass to the target.
            return month;
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
