﻿using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class IngredientViewModel
    {
        public int IngredientId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string IngredientType { get; set; }
        [Display(Name = "Base amount")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        public int BaseAmount { get; set; }
        [Display(Name = "Unit name")]
        [Required(ErrorMessage = "Unit name is required.")]
        public UnitViewModel Unit { get; set; }
    }
}
