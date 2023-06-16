using Data.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Utilities.FileHelper;

namespace Chat.Controllers
{
	public class AuthController : Controller
	{
		private IUserService _userService;
		private IAccountService _accountService;

		public AuthController(IAccountService accountService, IUserService userService)
		{
			_userService = userService;
			_accountService = accountService;
		}

		[HttpPost("SignUpUser")]
		public async Task<IActionResult> SignUpUser(UserControllerViewModel ucvm)
		{
			string[] permittedExtensions = { ".png", ".jpg", ".jpeg" };
			if (ModelState.IsValid || !ModelState.IsValid)
			{
				using (var memoryStream = new MemoryStream())
				{
					await ucvm.UserCreateModel.IFormFile.CopyToAsync(memoryStream);
					//check file extension and signature
					// Check the content length in case the file's only
					// content was a BOM and the content is actually
					// empty after removing the BOM.
					if (memoryStream.Length == 0)
					{
						return RedirectToAction(nameof(SignUp), new { error = "Lỗi!! file không chứa thông tin nào" });
					}
					if (!FileHelper.IsValidFileExtensionAndSignature(ucvm.UserCreateModel.IFormFile.FileName, memoryStream, permittedExtensions))
					{
						return RedirectToAction("SignUp", new { error = "Lỗi!! Chỉ được upload file hình như png, jpg, jpeg" });
					}
				}

				//check file size
				if (ucvm.UserCreateModel.IFormFile.Length > 2097152)
				{
					return RedirectToAction("SignUp", new { error = "Lỗi!! Chỉ được upload file nhỏ hơn 2MB" });
				}
				string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatars");

				//create folder if not exist
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				//change file name

				var ext = Path.GetExtension(ucvm.UserCreateModel.IFormFile.FileName).ToLowerInvariant();
				var fileName = Path.GetRandomFileName();
				fileName = Path.ChangeExtension(fileName, ext);
				string fileNameWithPath = Path.Combine(path, fileName);

				using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
				{
					ucvm.UserCreateModel.IFormFile.CopyTo(stream);
				}
				string imageUrl = "Images/UserAvatars/" + fileName;

				ucvm.UserCreateModel.ImageUrl = imageUrl;
				var accountCreated = await _accountService.RegisterAccount(ucvm.AccountCreateModel);
				if (accountCreated != null)
				{
					ucvm.UserCreateModel.AccId = accountCreated.Id;
					var userCreated = await _userService.CreateUserAsync(ucvm.UserCreateModel);
					if (userCreated)
					{
						return RedirectToAction(nameof(Index), new { success = "success" });
					}
				}
			}
			return RedirectToAction(nameof(SignUp), new { error = "fail to create account" });
		}


		public async Task<IActionResult> SignUp(string error, string success)
		{
			return View();
		}

		public async Task<IActionResult> Index(string error, string succes)
		{
			var session = HttpContext.Session;
			var token = session.GetString("USER");
			if (token == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction(nameof(Index), "Home");
			}
		}
	}
}
