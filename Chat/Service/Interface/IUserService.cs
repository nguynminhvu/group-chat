using Data.ViewModel.Account;
using Data.ViewModel.User;

namespace Service.Interface
{
    public interface IUserService
    {
        public Task<bool> CreateUserAsync(AccountCreateModel acm);
        public Task<ProfileViewModel> GetUser(Guid userId);
        public Task<ProfileViewModel> GetUserByAccId(Guid accId);
    }
}
