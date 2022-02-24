namespace HorecaShared.Ingredients
{
    public static class IngredientResponse
    {
        public class GetIndex
        {
            public List<IngredientDto.Index> Ingredients { get; set; } = new();
            public int TotalAmount { get; set; }
        }

        public class GetDetail
        {
            public IngredientDto.Detail Ingredient { get; set; }
        }

        public class Delete
        {
        }

        public class Create
        {
            public int IngredientId { get; set; }
        }

        public class Edit
        {
            public int IngredientId { get; set; }
        }
    }
}