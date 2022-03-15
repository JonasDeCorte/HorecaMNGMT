using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.Shared.Dtos.Menus
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string Name { get; set; }
    }

    public class MutateMenuDto : MenuDto
    {
    }

    public class MutateDishMenuDto
    {
        public int Id { get; set; }
        public MutateDishDto Dish { get; set; }
    }

    public class MenuDishesByIdDto
    {
        public int Id { get; set; }

        public List<DishDto>? Dishes { get; set; }
    }

    public class DeleteDishMenuDto
    {
        public int DishId { get; set; }
        public int MenuId { get; set; }
    }
}