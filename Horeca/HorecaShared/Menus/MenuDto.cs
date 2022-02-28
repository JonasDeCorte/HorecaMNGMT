using Domain.Kitchen;
using FluentValidation;

namespace HorecaShared.Menus
{
    public static class MenuDto
    {
        public class Index
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Detail : Index
        {
            public IReadOnlyList<Dish> Dishes { get; set; }
        }

        public class Mutate
        {
            public string Name { get; set; }

            public class Validator : AbstractValidator<Mutate>
            {
                public Validator()
                {
                    RuleFor(x => x.Name).NotEmpty().Length(1, 50);
                }
            }
        }
    }
}
