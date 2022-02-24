using Domain.Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaShared.Ingredients
{
    public static class IngredientRequest
    {
        public class GetIndex
        {
            public string? SearchTerm { get; set; }

            public bool OnlyActiveIngredients { get; set; }

            public int Page { get; set; }
            public int Amount { get; set; } = 25;
        }

        public class GetDetail
        {
            public int IngredientId { get; set; }
        }

        public class Delete
        {
            public int IngredientId { get; set; }
        }

        public class Create
        {
            public IngredientDto.Mutate Ingredient { get; set; }
        }

        public class Edit
        {
            public int IngredientId { get; set; }
            public IngredientDto.Mutate Ingredient { get; set; }
        }
    }
}