using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Ingredients;
using Horeca.MVC.Models.Mappers;
using Horeca.Shared.Data.Entities;

namespace HorecaMVC.Models.Mappers
{
    public static class DishMapper
    {
        public static DishViewModel MapModel(Dish dish)
        {
            DishViewModel model = new DishViewModel();

            model.Id = dish.Id;
            model.Name = dish.Name;
            model.Category = dish.Category;
            model.DishType = dish.DishType;
            model.Description = dish.Description;

            return model;
        }
        public static DishDetailViewModel MapDetailModel(Dish dish)
        {
            DishDetailViewModel model = new DishDetailViewModel();

            model.Id = dish.Id;
            model.Name = dish.Name;
            model.Category = dish.Category;
            model.DishType = dish.DishType;
            model.Description = dish.Description;

            foreach (var ingredient in dish.Ingredients)
            {
                IngredientViewModel ingredientModel = IngredientMapper.MapModel(ingredient);
                model.Ingredients.Add(ingredientModel);
            }

            return model;
        }
    }
}
