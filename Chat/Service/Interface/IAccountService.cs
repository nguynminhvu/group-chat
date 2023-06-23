using Data.Entities;
using Data.ViewModel.Account;


namespace Service.Interface
{
	public interface IAccountService
	{
		public Task<Account> CheckToken(string token);
		public Task<string> Login(AccountLoginModel alm);
		public Task<AccountViewModel> RegisterAccount(AccountCreateModel acm);
		public Task<bool> CheckEmailExist(string email);
		public Task<Account> GetAccount(string id);
	}
}
