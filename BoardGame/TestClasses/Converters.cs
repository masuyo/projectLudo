using SharedLudoLibrary.ClientClasses;
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
    class LockedConverter : IValueConverter
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
    class StartLudoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string locker = value.ToString();
            if (String.IsNullOrEmpty(locker))
            {
                return Brushes.Transparent;
            }
            else if (!String.IsNullOrEmpty(locker))
            {
                return Brushes.DodgerBlue;
            }
            return Binding.DoNothing;
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class PlayerBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PlayerColor color = (PlayerColor)value;
            switch (color)
            {
                case PlayerColor.RED:
                    return Brushes.Red;
                case PlayerColor.GREEN:
                    return Brushes.Green;
                case PlayerColor.BLUE:
                    return Brushes.Blue;
                case PlayerColor.YELLOW:
                    return Brushes.Yellow;
                default:
                    return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
