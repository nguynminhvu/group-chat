using Data.ViewModel.Account;

namespace Service.Interface
{
	public interface IUserService
	{
		public Task<bool> CreateUserAsync(AccountCreateModel acm);
	}
}
