using Domain.Kitchen;
using FluentValidation;

namespace HorecaShared.Ingredients
{
    public static class IngredientDto
    {
        public class Index
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class Detail : Index
        {
            public int Amount { get; set; }

            public IngredientType IngredientType { get; set; }

            public IReadOnlyList<Dish> Dishes { get; set; } = new List<Dish>();

            public bool IsEnabled { get; set; }
        }

        public class Mutate
        {
            public string Name { get; set; }
            public int Amount { get; set; }

            public IngredientType IngredientType { get; set; }

            public class Validator : AbstractValidator<Mutate>
            {
                public Validator()
                {
                    RuleFor(x => x.Name).NotEmpty().Length(1, 50);
                    RuleFor(x => x.Amount).InclusiveBetween(1, 999);
                    RuleFor(x => x.IngredientType).NotNull();
                }
            }
        }
    }
}