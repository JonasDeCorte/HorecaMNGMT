using Horeca.MVC.Models.Accounts;
using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Accounts;
using Horeca.Shared.Dtos.UserPermissions;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IPermissionService permissionService;

        public AccountController(IAccountService accountService, IPermissionService permissionService)
        {
            this.accountService = accountService;
            this.permissionService = permissionService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BaseUserDto> users = await accountService.GetUsers();
            if (users == null)
            {
                return View(nameof(NotFound));
            }
            UserListViewModel listModel = AccountMapper.MapUserListModel(users);

            return View(listModel);
        }

        public async Task<IActionResult> Detail(string username)
        {
            UserDto user = await accountService.GetUserByName(username);
            if (user == null)
            {
                return View(nameof(NotFound));
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
                    return View(nameof(Login));
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            var response = await accountService.LogoutUser();
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Index), "Home");
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
                    return View(nameof(Register));
                }

                return RedirectToAction(nameof(Index), new { area = "Home" });
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
                return View(nameof(NotFound));
            }
            var permissions = await permissionService.GetPermissions();
            MutatePermissionsViewModel editModel = AccountMapper.MapAddPermissionsModel(
                AccountMapper.MapUserPermissionsModel(user), permissions);

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
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { username = model.Username });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> RemovePermissions(string username)
        {
            UserDto user = await accountService.GetUserByName(username);
            if (user == null)
            {
                return View(nameof(NotFound));
            }
            MutatePermissionsViewModel editModel = AccountMapper.MapRemovePermissionsModel(
                AccountMapper.MapUserPermissionsModel(user));

            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemovePermissions(MutatePermissionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateUserPermissionsDto dto = AccountMapper.MapMutatePermissionsDto(model);

                var response = await accountService.RemovePermissions(dto);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { username = model.Username });
            }
            else
            {
                return View(model);
            }
        }

        [Route("/Account/DeleteUser/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var response = await accountService.DeleteUser(username);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}