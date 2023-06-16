using Data.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.User
{
	public class UserControllerViewModel
	{
		public UserCreateModel UserCreateModel { get; set; }
		public AccountCreateModel AccountCreateModel { get; set; }	
		public UserErrorCreateModel UserErrorCreateModel { get; set; }
	}
}
