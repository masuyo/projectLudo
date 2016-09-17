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
        ViewModel VM;
        public MainWindow()
        {
            InitializeComponent();
            VM = new ViewModel();
            this.DataContext = VM;
            this.Loaded += MainWindow_Loaded;
            this.MouseDown += MainWindow_MouseDown;
        }
        bool stop = false;
        DispatcherTimer dt;
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt.Stop(); stop = !stop;
            Console.WriteLine(VM.Dice);
            if (VM.Dice == 1)
            {
                rotateX.Angle = 270; rotateY.Angle = 90; rotateZ.Angle = 0;//
            }
            else if (VM.Dice == 2)
            {
                rotateX.Angle = 90; rotateY.Angle = 0; rotateZ.Angle = 90;//
            }
            else if (VM.Dice == 3)
            {
                rotateX.Angle = 180; rotateY.Angle = 0; rotateZ.Angle = 90;//
            }
            else if (VM.Dice == 4)
            {
                rotateX.Angle = 270; rotateY.Angle = 270; rotateZ.Angle = 90;//
            }
            else if (VM.Dice == 5)
            {
                rotateX.Angle = 270; rotateY.Angle = 0; rotateZ.Angle = 90;//
            }
            else
            {
                rotateX.Angle = 0; rotateY.Angle = 0; rotateZ.Angle = 0;//
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.2);
            dt.Tick += Dt_Tick;
            //Thread.Sleep(TimeSpan.FromMilliseconds(0.5));
            //viewport3D1.InvalidateVisual();

        }
        int i = 0;
        private void Dt_Tick(object sender, EventArgs e)
        {
            rotateX.Angle = new Random().Next(360);
            rotateY.Angle = new Random().Next(360);
            //if (i % 3 == 0)
            //{
            //    rotateX.Angle += 90;
            //}
            //if (i % 3 == 1)
            //{
            //    rotateY.Angle += 90;
            //}
            //if (i % 3 == 2)
            //{
            //    rotateZ.Angle += 90;
            //}
            //i++;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (stop)
                {
                    dt.Stop();
                }
                else
                {
                    dt.Start();
                }
                stop = !stop;
            }
        }
    }
}