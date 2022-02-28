using Domain.Kitchen;
using HorecaMVC.Data;
using HorecaPersistence;
using HorecaShared.Menus;
using Microsoft.EntityFrameworkCore;

namespace HorecaServices.Menus
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext context;

        public MenuService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<MenuResponse.Create> CreateAsync(MenuRequest.Create request)
        {
            MenuResponse.Create response = new();

            var menu = new Menu(request.Menu.Name);

            context.Menus.Add(menu);
            await context.SaveChangesAsync();

            response.MenuId = menu.Id;
            return response;
        }

        public async Task DeleteAsync(MenuRequest.Delete request)
        {
            context.Menus.RemoveIf(p => p.Id == request.MenuId);
            await context.SaveChangesAsync();
        }

        public async Task<MenuResponse.Edit> EditAsync(MenuRequest.Edit request)
        {
            MenuResponse.Edit response = new();

            var menu = await context.Menus.SingleAsync(x => x.Id == request.MenuId);
            var model = request.Menu;

            MapModelChanges(menu, model);

            await context.SaveChangesAsync();
            response.MenuId = menu.Id;

            return response;
        }

        private void MapModelChanges(Menu menu, MenuDto.Mutate model)
        {
            menu.Name = model.Name;
        }

        public async Task<MenuResponse.GetDetail> GetDetailAsync(MenuRequest.GetDetail request)
        {
            MenuResponse.GetDetail response = new();

            response.Menu = await context.Menus.Include(x => x.Dishes).Select(x =>
            new MenuDto.Detail
            {
                Id = x.Id,
                Name = x.Name,
            }).SingleAsync(x => x.Id == request.MenuId);

            return response;
        }

        public async Task<MenuResponse.GetIndex> GetIndexAsync(MenuRequest.GetIndex request)
        {
            MenuResponse.GetIndex response = new();

            var query = context.Menus.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(request.SearchTerm));
            }
            if (request.OnlyActiveMenus)
            {
                query = query.Where(x => x.IsEnabled);
            }

            response.TotalAmount = query.Count();

            query = query.OrderBy(x => x.Name);
            query = query.Skip(request.Amount * request.Page);
            query = query.Take(request.Amount);

            response.Menus = await query.Select(x => new MenuDto.Index
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return response;
        }
    }
}
