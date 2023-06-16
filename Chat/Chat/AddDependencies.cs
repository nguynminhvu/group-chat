using Data.Entities;
using Data.MappingConfig;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Implement;
using Service.Interface;

namespace Chat
{
	public static class AddDependencies
	{
		public static void AddDependency(this IServiceCollection services)
		{
			services.AddDbContext<ChatContext>(x => x.UseSqlServer("Data Source=NGYNMINHVU\\SQLEXPRESS;Initial Catalog=Chat;Integrated Security=True;Trust Server Certificate=True"));
			services.AddAutoMapper(typeof(ProfileConfig));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IUserService, UserService>();
			services.AddSession();
		}
	}
}
