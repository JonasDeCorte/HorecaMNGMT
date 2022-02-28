using Ardalis.GuardClauses;
using HorecaDomain.Common;

namespace Domain.Kitchen
{
    public class Menu : Entity
    {
        public string Name { get; set; }

        private readonly List<Dish> _dishes = new();
        public IReadOnlyList<Dish> Dishes => _dishes;

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Menu()
        {
        }

        public Menu(string name)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        }

        public void AddDish(Dish dish)
        {
            if (_dishes.Contains(dish))
            {
                throw new ArgumentException($"{nameof(dish)} is already added to {Name}");
            }

            _dishes.Add(dish);
        }

        public void RemoveDish(Dish dish)
        {
            if (!_dishes.Contains(dish))

                throw new ArgumentException($"{nameof(dish)} is not in {Name}");

            _dishes.Remove(dish);
        }
    }
}