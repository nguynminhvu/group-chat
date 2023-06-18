using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.User
{
	public class UserErrorCreateModel
	{
		public string EmailError { get; set; } = null!;

		public string PasswordError { get; set; } = null!;

		public string PasswordConfirmError { get; set; } = null!;

		public string FullnameError { get; set; } = null!;

		public DateTime BirthdayError { get; set; }

		public IFormFile IFormFileError { get; set; }
	}
}
