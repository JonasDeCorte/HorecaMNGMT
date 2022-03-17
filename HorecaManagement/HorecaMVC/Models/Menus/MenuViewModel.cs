﻿using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Menus
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Category { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "{0} may not contain more than 500 characters!")]
        public string Description { get; set; }
    }
}
