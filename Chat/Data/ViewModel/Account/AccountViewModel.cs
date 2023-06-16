using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.Account
{
	public class AccountViewModel
	{
		public string Id { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

		public bool IsActive { get; set; }

	}
}
