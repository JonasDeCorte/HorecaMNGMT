using Horeca.MVC.Controllers.Filters;
using Horeca.MVC.Models.Accounts;
using Horeca.MVC.Models.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.UserPermissions;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;
        private IPermissionService permissionService;

        public AccountController(IAccountService accountService, IPermissionService permissionService)
        {
            this.accountService = accountService;
            this.permissionService = permissionService;
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

        public async Task<IActionResult> AddPermissions(string username)
        {
            UserDto user = await accountService.GetUserByName(username);
            if (user == null)
            {
                return View("NotFound");
            }
            UserPermissionsViewModel userModel = AccountMapper.MapUserPermissionsModel(user);
            var permissions = await permissionService.GetPermissions();
            ViewData["Permissions"] = AccountMapper.MapPermissionList(userModel, permissions);

            MutatePermissionsViewModel editModel = new MutatePermissionsViewModel
            {
                Username = userModel.Username
            };

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPermissions(MutatePermissionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateUserPermissionsDto dto = AccountMapper.MapMutatePermissionsDto(model);

                var response = await accountService.AddPermissions(dto);
                if (response == null)
                {
                    return View("OperationFailed");
                }

                return RedirectToAction("Detail", new { username = model.Username });
            } else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> RemovePermissions(string username)
        {
            UserDto user = await accountService.GetUserByName(username);
            if (user == null)
            {
                return View("NotFound");
            }
            UserPermissionsViewModel userModel = AccountMapper.MapUserPermissionsModel(user);
            var permissions = await permissionService.GetPermissions();
            ViewData["Permissions"] = AccountMapper.MapPermissionList(userModel, permissions);

            MutatePermissionsViewModel editModel = new MutatePermissionsViewModel
            {
                Username = userModel.Username
            };

            return View(editModel);
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePermissions(MutatePermissionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            } else
            {
                return View(model);
            }

        }
    }
}
