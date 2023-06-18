using Data.ViewModel.Account;


namespace Service.Interface
{
	public interface IAccountService
	{
		public Task<AccountViewModel> Login(AccountLoginModel alm);
		public Task<AccountViewModel> RegisterAccount(AccountCreateModel acm);
		public Task<bool> CheckEmailExist(string email);
	}
}
