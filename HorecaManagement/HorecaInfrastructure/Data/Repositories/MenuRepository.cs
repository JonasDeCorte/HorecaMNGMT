using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Dtos.Menus;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly DatabaseContext context;

        public MenuRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public MenuDetailDto GetMenuDtoDetailIncludingDependencies(int id)
        {
            return context.Menus.Include(x => x.Dishes).Select(x => new MenuDetailDto()
            {
                Category = x.Category,
                Description = x.Description,
                Name = x.Name,
                Id = x.Id,
                Dishes = x.Dishes
            }).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public Menu GetMenuIncludingDependencies(int id)
        {
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);
            return context.Menus.Include(x => x.Dishes).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}