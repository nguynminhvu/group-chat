using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using Data.ViewModel.ChatRoom;
using Data.ViewModel.User;

namespace Data.MappingConfig
{
    public class ProfileConfig : Profile
    {
        public ProfileConfig()
        {
            CreateMap<Account, AccountViewModel>();
            CreateMap<User, ProfileViewModel>();
            CreateMap<ChatRoom, ChatRoomViewModel>();
        }
    }
}
