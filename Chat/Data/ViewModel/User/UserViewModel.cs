using Data.Entities;
using Data.ViewModel.Account;

namespace Data.ViewModel.User
{
	public class UserViewModel
	{
		public string Id { get; set; } = null!;

		public string Fullname { get; set; } = null!;

		public DateTime Birthday { get; set; }

		public string ImageSetId { get; set; } = null!;

		public string AccId { get; set; } = null!;

		public virtual AccountViewModel Acc { get; set; } = null!;

	}
}
