using Data.ViewModel.ChatRoom;
using Data.ViewModel.HomeController;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        private IChatRoomService _chatRoomService;
        private IUserService _userService;

        public HomeController(IUserService userService, IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var session = HttpContext.Session;
            var id = session.GetString("USER");
            if (id != null)
            {
                ViewBag.error = TempData["error"];
                ViewBag.success = TempData["success"];
                var user = await _userService.GetUserByAccId(Guid.Parse(id));
                HomeControllerModel hcm = new HomeControllerModel();
                hcm.UserViewModel = user;
                return View(hcm);
            }
            return RedirectToAction("Login", controllerName: "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatRoom(HomeControllerModel homeControllerModel)
        {
            var session = HttpContext.Session;
            var id = session.GetString("USER");
            if (id != null)
            {
                var created = await _chatRoomService.CreateChatRoom(id, homeControllerModel.ChatRoomCreateModel);
                if (created != null)
                {
                    ViewBag.success = "created";
                    return View(homeControllerModel);
                }
            }
            TempData["error"] = "can't create";
            return RedirectToAction("Index", controllerName: "Home");
        }

    }
}
