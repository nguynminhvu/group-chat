using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using Repository;
using Service.Implement;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
	public class AccountService : IAccountService
	{
		private IMapper _mapper;
		private IUnitOfWork _uow;

		public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_uow = unitOfWork;
		}

		public async Task<AccountViewModel> Login(AccountLoginModel alm)
		{
			var account = await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Email.Equals(alm.Email) && x.Password.Equals(x.Password) && x.IsActive);
			if (account != null)
			{
				return _mapper.Map<AccountViewModel>(account);
			}
			return null!;
		}

		public async Task<AccountViewModel> RegisterAccount(AccountCreateModel acm)
		{
			var account = await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Email.Equals(acm));
			if (account == null)
			{
				Account acc = new Account
				{
					Id = Guid.NewGuid().ToString(),
					Email = acm.Email,
					Password = acm.Password,
					IsActive = true
				};
				await _uow.AccountRepository.AddAsync(acc);
				return await _uow.SaveChanges() > 0 ? _mapper.Map<AccountViewModel>(acc) : null!;
			}
			return null!;
		}
	}
}
