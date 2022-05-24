using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AerodromDomaci.Konverteri
{
    internal class RaketeKonverter : IMultiValueConverter
    {
        // Konvertuje broj raketa u tekstualni oblik
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int? brojRaketa = (int?)values[0];
            if(brojRaketa is null)
            {
                return "/";
            } else
            {
                return brojRaketa.ToString();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
