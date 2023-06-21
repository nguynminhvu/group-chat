using Data.ViewModel.ImageSet;

namespace Data.ViewModel.Message
{
	public class MessageViewModel
	{
		public string Id { get; set; } = null!;

		public string Message1 { get; set; } = null!;
		public string ChatRoomId { get; set; } = null!;

		public DateTime CreateDate { get; set; }

		public string Sender { get; set; } = null!;

		public virtual ImageSetViewModel ImageSet { get; set; } = null!;
	}
}
