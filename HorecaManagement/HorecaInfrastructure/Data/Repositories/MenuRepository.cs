using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
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

        public Menu GetMenuIncludingDependencies(int id)
        {
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);
            return context.Menus.Include(x => x.Dishes).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}