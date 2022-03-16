namespace Horeca.MVC.Models.Dishes
{
    public class MutateDishViewModel
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string DishType { get; set; }
        public string Description { get; set; }
    }
}
