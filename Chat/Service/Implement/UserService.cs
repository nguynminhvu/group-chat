using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using Data.ViewModel.User;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ProfileViewModel> GetUser(Guid userId)
        {
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.Id.Equals(userId.ToString()), x => x.Acc, x => x.ImageSet);
            return _mapper.Map<ProfileViewModel>(user);
        }

        public async Task<ProfileViewModel> GetUserByAccId(Guid accId)
        {
            var acc = await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Id.Equals(accId.ToString()));
            var user = await _uow.UserRepository.FirstOrDefaultAsync(x => x.AccId.Equals(accId.ToString()));
            var image = await _uow.ImageRepository.GetEntitiesPredicate(x => x.ImageSetId.Equals(user.ImageSetId)).FirstOrDefaultAsync();
            return new ProfileViewModel()
            {
                AccId = accId.ToString(),
                Birthday = user.Birthday,
                Email = acc.Email,
                Fullname = user.Fullname,
                ImageUrl = image.ImageUrl
            };
        }
    }

}
