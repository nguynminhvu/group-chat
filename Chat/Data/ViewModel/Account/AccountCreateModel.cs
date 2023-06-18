using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.Account
{
	public class AccountCreateModel
	{
		[Required(ErrorMessage = "{0} is required")]
		public string Fullname { get; set; } = null!;


		[Required(ErrorMessage = "{0} is required")]
		public DateTime Birthday { get; set; }

		public string? ImageUrl { get; set; } = null!;

		[Required(ErrorMessage = "{0} is required")]
		public IFormFile IFormFile { get; set; }

		public string? AccId { get; set; } = null!;


		[Required(ErrorMessage = "{0} is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; } = null!;


		[Required(ErrorMessage = "{0} is required")]
		[StringLength(15, ErrorMessage = "{0} length >4", MinimumLength = 4)]
		public string Password { get; set; } = null!;


		[Required(ErrorMessage = "{0} is required")]
		[StringLength(15, ErrorMessage = "{0} length >4", MinimumLength = 4)]
		public string PasswordConfirm { get; set; } = null!;
	}
}
