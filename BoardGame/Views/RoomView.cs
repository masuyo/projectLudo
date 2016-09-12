using SharedLudoLibrary.Interfaces;
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

        ObservableCollection<IRoom> roomList;
        ObservableCollection<IRoom> searchRoomList;
        string searchKeyWord;
        IRoom selectedRoom;
        string selectedRoomName;
        string selectedRoomPassword;
        ObservableCollection<IUser> usersInRoom;
        string start;
                
        public ObservableCollection<IRoom> SearchRoomList
        {
            get { return searchRoomList; }

            set { SetProperty(ref searchRoomList, value); }
        }
        public ObservableCollection<IUser> UsersInRoom
        {
            get { return usersInRoom; }

            set { SetProperty(ref usersInRoom, value); }
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
        public string SelectedRoomName
        {
            get { return selectedRoomName; }

            set { SetProperty(ref selectedRoomName, value); }
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
        public string Start
        {
            get { return start; }
            set { SetProperty(ref start, value); }
        }
        static RoomView VM;
        private RoomView()
        {
            roomList = new ObservableCollection<IRoom>();
            searchRoomList = new ObservableCollection<IRoom>();
            usersInRoom = new ObservableCollection<IUser>();
            selectedRoom = default(IRoom);
            selectedRoomPassword = String.Empty;
            searchKeyWord = "Search room by name...";
            start = String.Empty;
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
