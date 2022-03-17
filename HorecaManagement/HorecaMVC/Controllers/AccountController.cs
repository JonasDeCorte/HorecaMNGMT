using Horeca.MVC.Models.Accounts;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserDto> users = await accountService.GetUsers();
            if (users == null)
            {
                return View("NotFound");
            }
            UserListViewModel listModel = new UserListViewModel();
            foreach(var user in users)
            {
                UserRolesViewModel model = AccountMapper.MapUserModel(user);
                listModel.Users.Add(model);
            }

            return View(listModel);
        }

        public async Task<IActionResult> Detail(string username)
        {
            UserDto user = await accountService.GetUserByName(username);
            if (user == null)
            {
                return View("NotFound");
            }
            UserRolesViewModel model = AccountMapper.MapUserModel(user);

            return View(model);
        }

        public IActionResult Login()
        {
            var model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginUserDto user = AccountMapper.MapLoginUser(model);

                accountService.LoginUser(user);

                return RedirectToAction("Index", new { area = "Home" });
            } else
            {
                return View(model);
            }
        }

        public IActionResult Register()
        {
            var model = new RegisterUserViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterUserDto user = AccountMapper.MapRegisterUser(model);

                accountService.RegisterUser(user);

                return RedirectToAction("Index", new { area = "Home" } );
            }
            else
            {
                return View(model);
            }
        }
    }
}
