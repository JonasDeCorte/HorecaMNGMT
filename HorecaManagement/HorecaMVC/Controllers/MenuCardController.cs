using Horeca.Shared.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Dishes;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.MVC.Models.Menus;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Controllers
{
    public class MenuCardController : Controller
    {
        private readonly IMenuCardService menuCardService;
        private readonly IMenuService menuService;
        private readonly IDishService dishService;
        private readonly IRestaurantService restaurantService;

        public MenuCardController(IMenuCardService menuCardService, IMenuService menuService, IDishService dishService, IRestaurantService restaurantService)
        {
            this.menuCardService = menuCardService;
            this.menuService = menuService;
            this.dishService = dishService;
            this.restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MenuCardDto> menuCards = await menuCardService.GetMenuCards();

            if (menuCards == null)
            {
                return View(nameof(NotFound));
            }

            MenuCardListViewModel listModel = new();

            foreach (var item in menuCards)
            {
                MenuCardViewModel model = MenuCardMapper.MapMenuCardModel(item);

                listModel.MenuCards.Add(model);
            }

            return View(listModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            MenuCard menuCard = await menuCardService.GetMenuCardDetailById(id);

            if (menuCard == null)
            {
                return View(nameof(NotFound));
            }

            MenuCardDetailViewModel model = MenuCardMapper.MapMenuCardDetailModel(menuCard);

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new MenuCardViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuCardViewModel menuCard)
        {
            if (ModelState.IsValid)
            {
                MutateMenuCardDto result = MenuCardMapper.MapMutateMenuCard(menuCard, new MenuCardDto());

                var response = await menuCardService.AddMenuCard(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(menuCard);
            }
        }

        public IActionResult CreateDish(int id)
        {
            var model = new MenuCardDishViewModel()
            {
                MenuCardId = id,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(int id, MenuCardDishViewModel dish)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuCardDto result = MenuCardMapper.MapMutateMenuCardDish(id, restaurantService.GetCurrentRestaurantId(), dish);

                var response = await menuCardService.AddMenuCardDish(id, result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id });
            }
            else
            {
                return View(dish);
            }
        }

        public async Task<IActionResult> AddExistingDish(int id)
        {
            MenuCardsByIdDto menuListsDto = await menuCardService.GetListsByMenuCardId(id);
            IEnumerable<DishDto> dishes = await dishService.GetDishes();
            if (dishes == null || menuListsDto == null)
            {
                return View(nameof(NotFound));
            }

            ExistingMenuCardDishesViewModel model = new() { MenuCardId = id };
            model.Dishes = MenuCardMapper.MapRemainingDishesList(menuListsDto, dishes);
            if (!model.Dishes.Any())
            {
                ModelState.AddModelError("DishId", "No dishes to be added");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExistingDish(int id, ExistingMenuCardDishesViewModel model)
        {   
            if (ModelState.IsValid)
            {
                MenuCardDishViewModel dishModel = MenuCardMapper.MapMutateMenuCardDishModel(id, await dishService.GetDishById(model.DishId));
                MutateDishMenuCardDto result = MenuCardMapper.MapMutateMenuCardDish(id, restaurantService.GetCurrentRestaurantId(), dishModel);
                var response = await menuCardService.AddMenuCardDish(id, result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }
            }
            return RedirectToAction(nameof(Detail), new { id });
        }

        public IActionResult CreateMenu(int id)
        {
            var model = new MenuCardMenuViewModel()
            {
                MenuCardId = id,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu(int id, MenuCardMenuViewModel menu)
        {
            if (ModelState.IsValid)
            {
                MutateMenuMenuCardDto result = MenuCardMapper.MapMutateMenuCardMenu(id, restaurantService.GetCurrentRestaurantId(), menu);

                var response = await menuCardService.AddMenuCardMenu(id, result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id });
            }
            else
            {
                return View(menu);
            }
        }

        public async Task<IActionResult> AddExistingMenu(int id)
        {
            MenuCardsByIdDto menuListsDto = await menuCardService.GetListsByMenuCardId(id);
            IEnumerable<MenuDto> menus = await menuService.GetMenus();
            if (menus == null || menuListsDto == null)
            {
                return View(nameof(NotFound));
            }

            ExistingMenuCardMenusViewModel model = new() { MenuCardId = id };
            model.Menus = MenuCardMapper.MapRemainingMenusList(menuListsDto, menus);
            if (!model.Menus.Any())
            {
                ModelState.AddModelError("MenuId", "No menus to be added");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExistingMenu(int id, ExistingMenuCardMenusViewModel model)
        {
            if (ModelState.IsValid)
            {
                MenuCardMenuViewModel menuModel = MenuCardMapper.MapMutateMenuCardMenuModel(id, await menuService.GetMenuById(model.MenuId));
                MutateMenuMenuCardDto result = MenuCardMapper.MapMutateMenuCardMenu(id, restaurantService.GetCurrentRestaurantId(), menuModel);
                var response = await menuCardService.AddMenuCardMenu(id, result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }
            }
            return RedirectToAction(nameof(Detail), new { id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            MenuCardDto menuCard = await menuCardService.GetMenuCardById(id);
            MenuCardViewModel model = MenuCardMapper.MapMenuCardModel(menuCard);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuCardViewModel menuCard)
        {
            if (ModelState.IsValid)
            {
                MutateMenuCardDto result = MenuCardMapper.MapMutateMenuCard(menuCard,
                    await menuCardService.GetMenuCardById(menuCard.Id));

                var response = await menuCardService.UpdateMenuCard(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = menuCard.Id });
            }
            else
            {
                return View(menuCard);
            }
        }

        [Route("/MenuCard/EditDish/{MenuCardId}/{DishId}")]
        public async Task<IActionResult> EditDish(int menuCardId, int dishId)
        {
            DishDto dish = await dishService.GetDishById(dishId);
            MenuCardDishViewModel model = MenuCardMapper.MapMutateMenuCardDishModel(menuCardId, dish);

            return View(model);
        }

        [Route("/MenuCard/EditDish/{MenuCardId}/{DishId}")]
        [HttpPost]
        public async Task<IActionResult> EditDish(MenuCardDishViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateDishMenuCardDto result = MenuCardMapper.MapMutateMenuCardDish(model.MenuCardId, restaurantService.GetCurrentRestaurantId(), model);

                var response = await menuCardService.UpdateMenuCardDish(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = model.MenuCardId });
            }
            else
            {
                return View(model);
            }
        }

        [Route("/MenuCard/EditMenu/{MenuCardId}/{MenuId}")]
        public async Task<IActionResult> EditMenu(int menuCardId, int menuId)
        {
            MenuDto menu = await menuService.GetMenuById(menuId);
            MenuCardMenuViewModel model = MenuCardMapper.MapMutateMenuCardMenuModel(menuCardId, menu);

            return View(model);
        }

        [Route("/MenuCard/EditMenu/{MenuCardId}/{MenuId}")]
        [HttpPost]
        public async Task<IActionResult> EditMenu(MenuCardMenuViewModel model)
        {
            if (ModelState.IsValid)
            {
                MutateMenuMenuCardDto result = MenuCardMapper.MapMutateMenuCardMenu(model.MenuCardId, restaurantService.GetCurrentRestaurantId(), model);

                var response = await menuCardService.UpdateMenuCardMenu(result);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Detail), new { id = model.MenuCardId });
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await menuCardService.DeleteMenuCard(id);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("/MenuCard/DeleteDish/{menuCardId}/{id}")]
        public async Task<IActionResult> DeleteDish(int menuCardId, int id)
        {
            DeleteDishMenuCardDto dish = new();
            dish.MenuCardId = menuCardId;
            dish.DishId = id;

            var response = await menuCardService.DeleteMenuCardDish(dish);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = menuCardId });
        }

        [Route("/MenuCard/DeleteMenu/{menuCardId}/{id}")]
        public async Task<IActionResult> DeleteMenu(int menuCardId, int id)
        {
            DeleteMenuMenuCardDto menu = new();
            menu.MenuCardId = menuCardId;
            menu.MenuId = id;

            var response = await menuCardService.DeleteMenuCardMenu(menu);
            if (response == null)
            {
                return View(nameof(NotFound));
            }

            return RedirectToAction(nameof(Detail), new { id = menuCardId });
        }
    }
}