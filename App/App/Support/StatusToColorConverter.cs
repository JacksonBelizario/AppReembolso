using System;
using System.Globalization;
using Xamarin.Forms;
using static App.Web.Models.Enums;

namespace App.Support
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = "#2FC2EF"; // amarelo
            if (value is Status && value != null)
            {
                Status status = (Status)value;

                switch (status)
                {
                    case Status.Pendente:
                        color = "#2FC2EF"; // azul
                        break;
                    case Status.Aprovada:
                        color = "#6DBF20"; // verde
                        break;
                    case Status.Rejeitada:
                        color = "#FF4423"; // vermelho
                        break;
                    default:
                        color = "#2FC2EF"; // amarelo
                        break;
                }

            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
