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
			CreateMap<ChatRoom, ChatRoomViewModel>().ForMember(x => x.Messages, x => x.MapFrom(x => x.Messages.OrderByDescending(x => x.CreateDate).FirstOrDefault()));
			CreateMap<ChatRoom, ChatRoomCurrentViewModel>().ForMember(x => x.Messages, x => x.MapFrom(x => x.Messages.OrderBy(x => x.CreateDate)));
			CreateMap<Message, MessageViewModel>();
			CreateMap<Image, ImageViewModel>();
			CreateMap<ImageSet, ImageSetViewModel>();
		}
	}
}
