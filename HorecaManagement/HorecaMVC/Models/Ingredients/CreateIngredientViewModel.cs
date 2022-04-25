﻿using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class CreateIngredientViewModel
    {
        public int IngredientId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }

        [Display(Name = "Ingredient Type")]
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string IngredientType { get; set; }

        [Display(Name = "Base amount")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public int BaseAmount { get; set; }

        public int UnitId { get; set; }

        public List<UnitViewModel> Units { get; set; } = new List<UnitViewModel>();
    }
}
