using HorecaShared.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        [HttpGet]
        public Task<IngredientResponse.GetIndex> GetIndexAsync([FromQuery] IngredientRequest.GetIndex request)
        {
            return ingredientService.GetIndexAsync(request);
        }

        [HttpGet("{IngredientId}")]
        public Task<IngredientResponse.GetDetail> GetDetailAsync([FromRoute] IngredientRequest.GetDetail request)
        {
            return ingredientService.GetDetailAsync(request);
        }

        [HttpDelete("{IngredientId}")]
        public Task DeleteAsync([FromRoute] IngredientRequest.Delete request)
        {
            return ingredientService.DeleteAsync(request);
        }

        [HttpPost]
        public Task<IngredientResponse.Create> CreateAsync([FromBody] IngredientRequest.Create request)
        {
            return ingredientService.CreateAsync(request);
        }

        [HttpPut]
        public Task<IngredientResponse.Edit> EditAsync([FromBody] IngredientRequest.Edit request)
        {
            return ingredientService.EditAsync(request);
        }
    }
}