using Horeca.MVC.Helpers.Mappers;
using Horeca.MVC.Models.Ingredients;
using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Units;
using Microsoft.AspNetCore.Mvc;

namespace Horeca.MVC.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService unitService;

        public UnitController(IUnitService unitService)
        {
            this.unitService = unitService;
        }

        public async Task<IActionResult> Create()
        {
            UnitViewModel model = new UnitViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitViewModel unit)
        {
            if (ModelState.IsValid)
            {
                MutateUnitDto dto = UnitMapper.MapMutateUnitDto(unit);
                var response = await unitService.AddUnit(dto);
                if (response == null)
                {
                    return View(nameof(NotFound));
                }

                return RedirectToAction(nameof(Index), "Ingredient");
            }
            else
            {
                return View(unit);
            }
        }
    }
}
