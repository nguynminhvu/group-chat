using Data.Entities;
using Data.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IAccountService
	{
		public Task<AccountViewModel> Login(AccountLoginModel alm);
		public Task<AccountViewModel> RegisterAccount(AccountCreateModel acm);
	}
}
