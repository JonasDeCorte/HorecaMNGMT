using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Units;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly DatabaseContext context;

        public IngredientRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<IngredientDto> GetAllIncludingUnit()
        {
            return context.Ingredients.Include(x => x.Unit).Select(ingredient => new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                BaseAmount = ingredient.BaseAmount,
                IngredientType = ingredient.IngredientType,
                Unit = new UnitDto()
                {
                    Id = ingredient.Unit.Id,
                    Name = ingredient.Unit.Name
                },
            }).ToList();
        }

        public IngredientDto GetIncludingUnit(int id)
        {
            return context.Ingredients.Include(x => x.Unit).Select(ingredient => new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                BaseAmount = ingredient.BaseAmount,
                IngredientType = ingredient.IngredientType,
                Unit = new UnitDto()
                {
                    Id = ingredient.Unit.Id,
                    Name = ingredient.Unit.Name
                },
            }).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public Ingredient GetIngredientIncludingUnit(int id)
        {
            return context.Ingredients.Include(x => x.Unit).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}