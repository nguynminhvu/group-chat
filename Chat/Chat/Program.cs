
using Chat.Application;
using Chat.SignalR_Hubs;

namespace Chat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			builder.Services.AddDependency();
			builder.Services.AddSignalR();
			builder.Services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = "localhost:6379";
			});
			var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();
			app.UseRouting();
			app.MapHub<Hubs>("chatHub");
			app.UseAuthorization();
			app.UseSession();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Auth}/{action=Login}/{id?}");

			app.Run();
		}
	}
}