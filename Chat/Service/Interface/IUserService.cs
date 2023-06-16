using Data.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IUserService
	{
		public Task<bool> CreateUserAsync(UserCreateModel ucm);
	}
}
