using Data.ViewModel.ChatRoom;
using Data.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.HomeController
{
    public class HomeControllerModel
    {
        public ProfileViewModel UserViewModel { get; set; }
        public ChatRoomCreateModel ChatRoomCreateModel { get; set; }

    }
}
