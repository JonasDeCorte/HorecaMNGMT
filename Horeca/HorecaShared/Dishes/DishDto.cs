using Domain.Kitchen;
using FluentValidation;

namespace HorecaShared.Dishes
{
    public static class DishDto
    {
        public class Index
        {
            public int Id { get; set; }

            public string Name { get; set; }
            public string Category { get; set; }
        }

        public class Detail : Index
        {
            public string Description { get; set; }
            public DishType DishType { get; set; }
            public bool IsEnabled { get; set; }
        }

        public class Mutate
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public DishType DishType { get; set; }
            public string Category { get; set; }

            public class Validator : AbstractValidator<Mutate>
            {
                public Validator()
                {
                    RuleFor(x => x.Name).NotEmpty().Length(1, 50);
                    RuleFor(x => x.Description).NotEmpty().Length(1, 500);
                    RuleFor(x => x.Category).NotEmpty().Length(1, 50);
                    RuleFor(x => x.DishType).NotNull();
                }
            }
        }
    }
}
