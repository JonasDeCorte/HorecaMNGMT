using HorecaShared.Menus;
using Microsoft.AspNetCore.Mvc;

namespace HorecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        [HttpGet]
        public Task<MenuResponse.GetIndex> GetIndexAsync([FromQuery] MenuRequest.GetIndex request)
        {
            return menuService.GetIndexAsync(request);
        }

        [HttpGet("{MenuId}")]
        public Task<MenuResponse.GetDetail> GetDetailAsync([FromRoute] MenuRequest.GetDetail request)
        {
            return menuService.GetDetailAsync(request);
        }

        [HttpDelete("{MenuId}")]
        public Task DeleteAsync([FromRoute] MenuRequest.Delete request)
        {
            return menuService.DeleteAsync(request);
        }

        [HttpPost]
        public Task<MenuResponse.Create> CreateAsync([FromBody] MenuRequest.Create request)
        {
            return menuService.CreateAsync(request);
        }

        [HttpPut]
        public Task<MenuResponse.Edit> EditAsync([FromBody] MenuRequest.Edit request)
        {
            return menuService.EditAsync(request);
        }
    }
}
