using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BoardGame.TestClasses
{
    class TestLockedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string locker = value.ToString();
            if (String.IsNullOrEmpty(locker))
            {
                return Brushes.White;
            }
            else if (!String.IsNullOrEmpty(locker))
            {
                return Brushes.DodgerBlue;
            }
            return Binding.DoNothing;
            /*
             public static ImageBrush GetBG
                {
                    get
                    {
                        ImageBrush imgb = new ImageBrush();
                        imgb.ImageSource = new BitmapImage(new Uri(@"Images\l2.png", UriKind.Relative));
                        return imgb;
                    }
                }             
             */

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
