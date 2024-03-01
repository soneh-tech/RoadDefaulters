
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RoadDefaulters.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccount account;

		public AccountController(IAccount account)
		{
			this.account = account;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(AppUserDto user)
		{
			var result = await account.LoginUser(user);
			if (result is true)
				return RedirectToAction("index", "home");
			return View(user);
		}

		[HttpGet]
		public async Task<IActionResult> Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(AppUserDto user)
		{
			var result = await account.RegisterUser(user);
			if (result is true)
				return RedirectToAction("index");
			return View(user);
		}
	}
}
