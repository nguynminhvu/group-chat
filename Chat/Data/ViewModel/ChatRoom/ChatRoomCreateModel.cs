using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.ChatRoom
{
    public class ChatRoomCreateModel
    {
        public string Name { get; set; } = null!;

        public string? Secret { get; set; }

        public bool IsPrivate { get; set; }

    }
}
