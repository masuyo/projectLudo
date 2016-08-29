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
    /// Interaction logic for LudoWindow.xaml
    /// </summary>
    public partial class LudoWindow : Window
    {
        ViewModel VM;
        public LudoWindow()
        {
            InitializeComponent();
            VM = ViewModel.GetVM;
            this.DataContext = VM;

            VM.ChatMsgs.Add(new ChatMsg("slfmsflnlkwnfleknflekwneklnefklenwklfnkln", "Tom"));
            VM.ChatMsgs.Add(new ChatMsg("msg2", "Ben"));
            VM.ChatMsgs.Add(new ChatMsg("msg3", "Alice"));
            VM.ChatMsgs.Add(new ChatMsg("msg4", "David"));
        }

        private void LBL_Send_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("a");
            MessageBox.Show(VM.ChatMsg);
            Console.WriteLine("b");

            VM.ChatMsgs.Add(new ChatMsg("msgLBLdlmwfnlwnfklwn,mc mfkekjwémwlenrén ee", "Lalalallalaa"));
        }

        private void TXB_Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VM.ChatMsgs.Add(new ChatMsg(VM.ChatMsg, "sentby"));
            }
        }
    }
}
