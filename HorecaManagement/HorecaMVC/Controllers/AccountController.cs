using Horeca.MVC.Controllers.Filters;
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

        [TypeFilter(typeof(TokenFilter))]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BaseUserDto> users = await accountService.GetUsers();
            if (users == null)
            {
                return View("NotFound");
            }
            UserListViewModel listModel = new UserListViewModel();
            foreach(var user in users)
            {
                UserViewModel model = AccountMapper.MapUserModel(user);
                listModel.Users.Add(model);
            }

            return View(listModel);
        }

        [TypeFilter(typeof(TokenFilter))]
        public async Task<IActionResult> Detail(string username)
        {
            UserDto user = await accountService.GetUserByName(username);
            if (user == null)
            {
                return View("NotFound");
            }
            UserPermissionsViewModel model = AccountMapper.MapUserPermissionsModel(user);

            return View(model);
        }

        public IActionResult Login()
        {
            var model = new LoginUserViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginUserDto user = AccountMapper.MapLoginUser(model);

                var response = await accountService.LoginUser(user);
                if (response == null)
                {
                    return View("Login");
                }
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
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterUserDto user = AccountMapper.MapRegisterUser(model);

                var response = await accountService.RegisterUser(user);
                if (response == null)
                {
                    return View("Register");
                }

                return RedirectToAction("Index", new { area = "Home" } );
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> EditPermissions(string username)
        {
            UserDto dto = await accountService.GetUserByName(username);
            UserPermissionsViewModel model = AccountMapper.MapUserPermissionsModel(dto);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPermissions(UserPermissionsViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
