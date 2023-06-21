using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using Data.ViewModel.ChatRoom;
using Data.ViewModel.Image;
using Data.ViewModel.ImageSet;
using Data.ViewModel.Message;
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
			CreateMap<Message, MessageViewModel>();
			CreateMap<Image, ImageViewModel>();
			CreateMap<ImageSet, ImageSetViewModel>();
		}
	}
}
