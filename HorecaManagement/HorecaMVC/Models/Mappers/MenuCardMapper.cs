using Horeca.MVC.Models.MenuCards;
using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Models.Mappers
{
    public static class MenuCardMapper
    {
        public static MenuCardViewModel MapModel(MenuCard menuCard)
        {
            MenuCardViewModel model = new MenuCardViewModel();

            model.Id = menuCard.Id;
            model.Name = menuCard.Name;

            return model;
        }

    }
}
