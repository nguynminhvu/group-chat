using Data.Entities;
using Data.ViewModel.ChatRoom;
using Data.ViewModel.HomeController;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Chat.Controllers
{
	public class HomeController : Controller
	{
		private IAccountService _accountService;
		private IChatRoomService _chatRoomService;
		private IUserService _userService;
		private static string URL = "http://localhost:5246/Home/JoinRoom";
		public HomeController(IUserService userService, IChatRoomService chatRoomService, IAccountService accountService)
		{
			_accountService = accountService;
			_chatRoomService = chatRoomService;
			_userService = userService;
		}
		private async Task<Account> CheckTokenController()
		{
			try
			{
				var session = HttpContext.Session;
				var token = session.GetString("Token");
				if (token != null)
				{
					return await _accountService.CheckToken(token);
				}
				return null!;
			}
			catch
			{
				return null!;
			}
		}

		public async Task<IActionResult> Index(string roomId)
		{
			var account = await CheckTokenController();
			if (account != null)
			{
				HomeControllerModel hcm = new HomeControllerModel();
				ChatRoomCurrentViewModel chatRoomCurrentViewModel = new ChatRoomCurrentViewModel();
				var user = await _userService.GetUserByAccId(Guid.Parse(account.Id));
				var chatRooms = await _chatRoomService.GetChatRooms(user.AccId);

				if (roomId == null && chatRooms.Count > 0)
				{
					roomId = chatRooms.FirstOrDefault()!.Id;
				}
				chatRoomCurrentViewModel = await _chatRoomService.GetChatRoom(roomId!);
				hcm.ChatRoomCurrentViewModel = chatRoomCurrentViewModel;
				hcm.ProfileViewModel = user;
				hcm.ChatRooms = chatRooms;

				ViewBag.error = TempData["error"];
				ViewBag.success = TempData["success"];
				return View(hcm);
			}
			return RedirectToAction("Login", controllerName: "Auth");
		}

		public async Task<IActionResult> LoadChats(string roomId)
		{
			var account = await CheckTokenController();
			if (account != null)
			{
				return RedirectToAction(nameof(Index), controllerName: "Home", new { roomId = roomId });
			}
			return RedirectToAction("Login", controllerName: "Auth");
		}

		[HttpPost]
		public async Task<IActionResult> CreateChatRoom(HomeControllerModel homeControllerModel)
		{
			var account = await CheckTokenController();
			if (account != null)
			{
				if (homeControllerModel.ChatRoomCreateModel.IsPrivate && string.IsNullOrEmpty(homeControllerModel.ChatRoomCreateModel.Secret))
				{
					TempData["error"] = "Secret key required";
					return RedirectToAction("Index", controllerName: "Home");
				}
				var created = await _chatRoomService.CreateChatRoom(account.Id, homeControllerModel.ChatRoomCreateModel);
				if (created != null)
				{
					TempData["success"] = "created";
					return RedirectToAction("Index", controllerName: "Home", new { roomId = created.Id });
				}
			}
			TempData["error"] = "can't create";
			return RedirectToAction("Index", controllerName: "Home");
		}

		[HttpPost]
		public async Task<IActionResult> CreateMessage(HomeControllerModel homeControllerModel)
		{
			var account = await CheckTokenController();
			if (account != null)
			{
				if (await _chatRoomService.CreateMessage(homeControllerModel.SendMessageViewModel) != null)
				{
					return RedirectToAction("Index", controllerName: "Home", homeControllerModel.SendMessageViewModel.RoomId);
				}
				TempData["error"] = "can't create chat";
				return RedirectToAction("Index", controllerName: "Home");
			}
			return RedirectToAction("Login", controllerName: "Auth");
		}

		[HttpPost]
		public async Task<IActionResult> JoinRoom(HomeControllerModel homeControllerModel)
		{
			var account = await CheckTokenController();
			if (account != null)
			{
				if (await _chatRoomService.CheckSecretKey(homeControllerModel.JoinRoomViewModel.RoomId, homeControllerModel.JoinRoomViewModel.SecretKey) is false)
				{
					TempData["error"] = "Secret key is required";
					return RedirectToAction("Index", controllerName: "Home");
				}
				var created = await _chatRoomService.JoinRoom(homeControllerModel.JoinRoomViewModel);
				if (created != null)
				{
					TempData["success"] = "Join success, have fun";
					return RedirectToAction(nameof(Index), controllerName: "Home", new { roomId = created.Id });
				}
				TempData["error"] = "can't join this room";
				return RedirectToAction(nameof(Index), "Home");
			}
			return RedirectToAction("Login", controllerName: "Auth");
		}

		public async Task<IActionResult> JoinRoom(string roomId)
		{
			var account = await CheckTokenController();
			if (account != null)
			{
				var created = await _chatRoomService.JoinRoomQR(roomId, account.Id);
				if (created != null)
				{
					TempData["success"] = "Join success, have fun";
					return RedirectToAction(nameof(Index), controllerName: "Home", new { roomId = created.Id });
				}
				TempData["error"] = "can't join this room";
				return RedirectToAction(nameof(Index), "Home");
			}
			return RedirectToAction("Login", controllerName: "Auth");
		}
	}
}
