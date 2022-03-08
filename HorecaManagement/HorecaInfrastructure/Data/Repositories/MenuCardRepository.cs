using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class MenuCardRepository : Repository<MenuCard>, IMenuCardRepository
    {
        private readonly DatabaseContext context;

        public MenuCardRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public MenuCard GetMenuCardIncludingDependencies(int id)
        {
            return context.MenuCards.Include(x => x.Menus).Include(x => x.Dishes).Where(x => x.Id.Equals(id)).SingleOrDefault();
        }
    }
}