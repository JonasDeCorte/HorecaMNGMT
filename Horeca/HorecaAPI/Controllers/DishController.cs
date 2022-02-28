using HorecaShared.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;

        public DishController(IDishService dishService)
        {
            this.dishService = dishService;
        }

        [HttpGet]
        public Task<DishResponse.GetIndex> GetIndexAsync([FromQuery] DishRequest.GetIndex request)
        {
            return dishService.GetIndexAsync(request);
        }

        [HttpGet("{DishId}")]
        public Task<DishResponse.GetDetail> GetDetailAsync([FromRoute] DishRequest.GetDetail request)
        {
            return dishService.GetDetailAsync(request);
        }

        [HttpDelete("{DishId}")]
        public Task DeleteAsync([FromRoute] DishRequest.Delete request)
        {
            return dishService.DeleteAsync(request);
        }

        [HttpPost]
        public Task<DishResponse.Create> CreateAsync([FromBody] DishRequest.Create request)
        {
            return dishService.CreateAsync(request);
        }

        [HttpPut]
        public Task<DishResponse.Edit> EditAsync([FromBody] DishRequest.Edit request)
        {
            return dishService.EditAsync(request);
        }
    }
}