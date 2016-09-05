using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BoardGame.Views
{
    class RoomView : Bindable
    {
        private string userName;

        ObservableCollection<IRoom> roomList;
        ObservableCollection<IRoom> searchRoomList;
        string searchKeyWord;
        IRoom selectedRoom;
        string selectedRoomPassword;

        public string UserName
        {
            get { return userName; }

            set { SetProperty(ref userName, value); }
        }

        public ObservableCollection<IRoom> SearchRoomList
        {
            get { return searchRoomList; }

            set { SetProperty(ref searchRoomList, value); }
        }

        public ObservableCollection<IRoom> RoomList
        {
            get { return roomList; }

            set { SetProperty(ref roomList, value); }
        }
        public IRoom SelectedRoom
        {
            get { return selectedRoom; }

            set { SetProperty(ref selectedRoom, value); }
        }
        public string SelectedRoomPassword
        {
            get { return selectedRoomPassword; }

            set { SetProperty(ref selectedRoomPassword, value); }
        }
        public string SearchKeyWord
        {
            get { return searchKeyWord; }

            set { SetProperty(ref searchKeyWord, value); }
        }
        static RoomView VM;
        private RoomView()
        {
            roomList = new ObservableCollection<IRoom>();
            searchRoomList = new ObservableCollection<IRoom>();
            selectedRoom = default(IRoom);
            selectedRoomPassword = String.Empty;
            SearchKeyWord = "Search room by name...";
        }
        public static RoomView GetVM
        {
            get
            {
                if (VM == null)
                {
                    VM = new RoomView();
                }
                return VM;
            }
        }

    }
}
