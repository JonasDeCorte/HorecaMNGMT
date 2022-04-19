using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.MenuCards
{
    public class MenuCardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Name { get; set; }
    }
}
