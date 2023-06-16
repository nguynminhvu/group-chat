using Data.Entities;
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
	public class UserService:IUserService
	{
		private IUnitOfWork _uow;

		public UserService(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}
		public async Task<bool> CreateUserAsync(UserCreateModel ucm)
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
				ImageUrl = ucm.ImageUrl,
				IsActive = true
			};
			await _uow.ImageRepository.AddAsync(image);
			User user = new User
			{
				Id = Guid.NewGuid().ToString(),
				Fullname = ucm.Fullname,
				Birthday = ucm.Birthday,
				AccId = ucm.AccId,
				ImageSetId = imageSet.Id,
			};
			await _uow.UserRepository.AddAsync(user);
			return await _uow.SaveChanges() > 0 ? true : false;
		}
	}

}
