

using Microsoft.AspNetCore.Http;

namespace Data.ErrorModel
{
	public class SignUpErrorModel
	{
		public string? FullnameError { get; set; } = null!;

		public string? ImageUrlError { get; set; } = null!;

		public string? EmailError { get; set; } = null!;

		public string? PasswordError { get; set; } = null!;

		public DateTime? BirthdayError { get; set; }

		public string? PasswordConfirmError { get; set; } = null!;
	}
}
