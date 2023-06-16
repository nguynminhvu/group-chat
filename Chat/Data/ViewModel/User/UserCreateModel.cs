using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.User
{
	public class UserCreateModel
	{
		public string Fullname { get; set; } = null!;

		public DateTime Birthday { get; set; }

		public string ImageUrl { get; set; } = null!;
		public IFormFile IFormFile { get; set; }
		public string AccId { get; set; } = null!;
	}
}
