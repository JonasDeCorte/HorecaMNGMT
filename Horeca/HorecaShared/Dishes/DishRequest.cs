namespace HorecaShared.Dishes
{
    public static class DishRequest
    {
        public class GetIndex
        {
            public string? SearchTerm { get; set; }
            public bool OnlyActiveDishes { get; set; }
            public int Page { get; set; }
            public int Amount { get; set; } = 25;
        }

        public class GetDetail
        {
            public int DishId { get; set; }
        }

        public class Delete
        {
            public int DishId { get; set; }
        }

        public class Create
        {
            public DishDto.Mutate Dish { get; set; }
        }

        public class Edit
        {
            public int DishId { get; set; }
            public DishDto.Mutate Dish { get; set; }
        }
    }
}
