using Data.ViewModel.Account;
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

		[HttpPost]
		public async Task<IActionResult> SignUp(AccountCreateModel acm)
		{
			string[] permittedExtensions = { ".png", ".jpg", ".jpeg" };
			if (ModelState.IsValid)
			{
				if (acm.Password != acm.PasswordConfirm)
				{
					ModelState.AddModelError("PasswordConfirm", "Two password not equal");
					return View(acm);
				}
				if (await _accountService.CheckEmailExist(acm.Email) is false)
				{
					ModelState.AddModelError("Email", "Email already exist");
					return View(acm);
				}
				using (var memoryStream = new MemoryStream())
				{
					await acm.IFormFile!.CopyToAsync(memoryStream);
					//check file extension and signature
					// Check the content length in case the file's only
					// content was a BOM and the content is actually
					// empty after removing the BOM.
					if (memoryStream.Length == 0)
					{
						ModelState.AddModelError("IFormFile", "Image is required");
						return View(acm);
					}
					if (!FileHelper.IsValidFileExtensionAndSignature(acm.IFormFile.FileName, memoryStream, permittedExtensions))
					{
						ModelState.AddModelError("IFormFile", "Lỗi!! Chỉ được upload file hình như png, jpg, jpeg");
						return View(acm);
					}
				}

				//check file size
				if (acm.IFormFile.Length > 2097152)
				{
					ModelState.AddModelError("IFormFile", "Lỗi!! Chỉ được upload file nhỏ hơn 2MB");
					return View(acm);
				}
				string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/UserAvatars");

				//create folder if not exist
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				//change file name

				var ext = Path.GetExtension(acm.IFormFile!.FileName).ToLowerInvariant();
				var fileName = Path.GetRandomFileName();
				fileName = Path.ChangeExtension(fileName, ext);
				string fileNameWithPath = Path.Combine(path, fileName);

				using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
				{
					acm.IFormFile.CopyTo(stream);
				}
				string imageUrl = "Images/UserAvatars/" + fileName;

				acm.ImageUrl = imageUrl;
				var accountCreated = await _accountService.RegisterAccount(acm);
				if (accountCreated != null)
				{
					acm.AccId = accountCreated.Id;
					var userCreated = await _userService.CreateUserAsync(acm);
					if (userCreated)
					{
						TempData["success"] = "success";
						return RedirectToAction(nameof(Login));
					}
				}
			}
			ViewBag.error = "fail to create account";
			return View(acm);
		}


		public async Task<IActionResult> SignUp()
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

		public IActionResult Login()
		{
			ViewBag.success = TempData["success"];
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(AccountLoginModel alm)
		{
			if (ModelState.IsValid)
			{
				var rs = await _accountService.Login(alm);
				if (rs != null)
				{
					var session = HttpContext.Session;
					session.SetString("USER", rs.Id.ToString());
					return RedirectToAction("Index", controllerName: "Home");
				}
				ModelState.AddModelError("Email", "Email or password invalid");
				ModelState.AddModelError("Password", "Email or password invalid");
				return View(alm);
			}
			return View(alm);
		}
	}
}
