using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using Data.ViewModel.User;
using Repository;
using Service.Interface;

namespace Service.Implement
{
	public class UserService : IUserService
	{
		private IMapper _mapper;
		private IUnitOfWork _uow;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_uow = unitOfWork;
		}
		public async Task<bool> CreateUserAsync(AccountCreateModel acm)
		{
			ImageSet imageSet = new ImageSet
			{
				Id = Guid.NewGuid().ToString(),
				IsActive = true
			};
			await _uow.ImageSetRepository.AddAsync(imageSet);
			Image image = new Image
			{
				Id = Guid.NewGuid().ToString(),
				ImageSetId = imageSet.Id,
				ImageUrl = acm.ImageUrl!,
				IsActive = true
			};
			await _uow.ImageRepository.AddAsync(image);
			User user = new User
			{
				Id = Guid.NewGuid().ToString(),
				Fullname = acm.Fullname!,
				Birthday = acm.Birthday!,
				AccId = acm.AccId!,
				ImageSetId = imageSet.Id,
			};
			await _uow.UserRepository.AddAsync(user);
			return await _uow.SaveChanges() > 0 ? true : false;
		}

		public async Task<ProfileViewModel> GetUserByAccId(Guid accId)
		{
			var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.AccId.Equals(accId.ToString()), x => x.ImageSet, x => x.Acc);
			var image = await _uow.ImageRepository.FirstOrDefaultAsync(x => x.ImageSetId.Equals(user.ImageSetId));
			return new ProfileViewModel()
			{
				AccId = accId.ToString(),
				Birthday = user.Birthday,
				Email = user.Acc.Email,
				Fullname = user.Fullname,
				ImageUrl = image.ImageUrl
			};
		}
	}

}
