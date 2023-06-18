using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.Account
{
	public class AccountLoginModel
	{
		[Required(ErrorMessage = "{0} is required")]
		[EmailAddress(ErrorMessage = "Email invalid")]
		public string Email { get; set; } = null!;


		[Required(ErrorMessage = "{0} is required")]
		[StringLength(20, ErrorMessage = "{0} length >4", MinimumLength = 4)]
		public string Password { get; set; } = null!;
	}
}
