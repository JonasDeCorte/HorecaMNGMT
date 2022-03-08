using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.Shared.Dtos.MenuCards
{
    public class MenuCardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MutateMenuCardDto : MenuCardDto
    {
    }

    public class MutateMenuMenuCardDto
    {
        public int MenuCardId { get; set; }
        public MutateMenuDto Menu { get; set; }
    }

    public class MutateDishMenuCardDto
    {
        public int MenuCardId { get; set; }
        public MutateDishDto Dish { get; set; }
    }

    public class MenuCardsByIdDto
    {
        public int Id { get; set; }

        public List<MenuDto> Menus { get; set; }

        public List<DishDto> Dishes { get; set; }
    }

    public class DeleteDishMenuCardDto
    {
        public int MenuCardId { get; set; }

        public int DishId { get; set; }
    }

    public class DeleteMenuMenuCardDto
    {
        public int MenuCardId { get; set; }

        public int MenuId { get; set; }
    }
}