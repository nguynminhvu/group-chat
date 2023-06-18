using Data.Entities;
using Data.ViewModel.Account;
using Data.ViewModel.User;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
	public class UserService : IUserService
	{
		private IUnitOfWork _uow;

		public UserService(IUnitOfWork unitOfWork)
		{
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
	}

}
