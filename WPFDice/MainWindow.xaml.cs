using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFDice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.MouseDown += MainWindow_MouseDown;
        }
        bool stop = false;
        DispatcherTimer dt;
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (stop)
            {
                rotateX.Angle = 0;
                rotateY.Angle = 100;
                rotateZ.Angle = 0;
                dt.Stop();
            }
            else
            {
                dt.Start();
            }
            stop = !stop;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.2);
            dt.Tick += Dt_Tick;
            //Thread.Sleep(TimeSpan.FromMilliseconds(0.5));
            //viewport3D1.InvalidateVisual();

        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            rotateX.Angle = new Random().Next(360);
            rotateY.Angle = new Random().Next(360);
        }
    }
}
