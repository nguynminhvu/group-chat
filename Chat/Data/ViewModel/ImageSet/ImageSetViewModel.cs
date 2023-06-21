using Data.ViewModel.Image;

namespace Data.ViewModel.ImageSet
{
	public class ImageSetViewModel
	{
		public string Id { get; set; } = null!;

		public bool IsActive { get; set; }

		public virtual ICollection<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
	}
}
