using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel.User
{
	public class UserViewModel
	{
		public string Id { get; set; } = null!;

		public string Fullname { get; set; } = null!;

		public DateTime Birthday { get; set; }

		public string ImageSetId { get; set; } = null!;

		public string AccId { get; set; } = null!;

	}
}
