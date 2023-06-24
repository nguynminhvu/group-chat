using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Service.RegisService
{
	public interface IRegisService
	{
		public Task<bool> SaveConnectionIdString(string accId, string connectionId);
		public Task<string> GetConnectionIdAsync(string accId);
	}
	public class RegisService:IRegisService
	{
		private readonly IDistributedCache _cache;
		public RegisService(IDistributedCache distributedCache)
		{
			_cache = distributedCache;
		}
		public async Task<bool> SaveConnectionIdString(string accId,string connectionId )
		{
			var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddDays(1)).SetSlidingExpiration(TimeSpan.FromDays(2));
			var toByte = System.Text.Encoding.UTF8.GetBytes(connectionId);
			await _cache.SetAsync(accId, toByte, options);
			return true;
		}

		public async Task<string> GetConnectionIdAsync(string accId)
		{
			var rs = await _cache.GetAsync(accId);
			if (rs != null)
			{
				return Encoding.UTF8.GetString(rs);
			}
			return null!;
		}
	}
}
