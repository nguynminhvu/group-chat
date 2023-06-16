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
		public string FullnameError { get; set; } = null!;

		public DateTime BirthdayError { get; set; }

		public string ImageUrlError { get; set; } = null!;
		public IFormFile IFormFileError { get; set; }
		public string AccId { get; set; } = null!;
	}
}
