using Data.Entities;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Implement
{
	public class ImageSetRepository : Repository<ImageSet>, IImageSetRepository
	{
		public ImageSetRepository(ChatContext chatContext) : base(chatContext)
		{
		}
	}
}
