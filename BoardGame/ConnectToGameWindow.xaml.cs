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
using System.Windows.Shapes;

namespace BoardGame
{
    /// <summary>
    /// Interaction logic for ConnectToGameWindow.xaml
    /// </summary>
    public partial class ConnectToGameWindow : Window
    {
        public ConnectToGameWindow()
        {
            InitializeComponent();
        }
        private void LBL_Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LudoWindow ludo = new LudoWindow();
            ludo.ShowDialog();
        }
    }
}
