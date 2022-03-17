using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Dishes
{
    public class MenuCardDishViewModel
    {
        public int MenuCardId { get; set; }
        public int DishId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Category { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string DishType { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "{0} may not contain more than 500 characters!")]
        public string Description { get; set; }
    }
}
