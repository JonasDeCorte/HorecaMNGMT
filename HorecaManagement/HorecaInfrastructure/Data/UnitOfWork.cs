﻿using Horeca.Infrastructure.Data.Repositories;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Repositories;

namespace Horeca.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IIngredientRepository Ingredients => new IngredientRepository(_context);

        public IUnitRepository Units => new UnitRepository(_context);

        public IDishRepository Dishes => new DishRepository(_context);

        public IMenuRepository Menus => new MenuRepository(_context);

        public IMenuCardRepository MenuCards => new MenuCardRepository(_context);

        public async Task CommitAsync()
        {
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
            await _context.SaveChangesAsync();
        }
    }
}