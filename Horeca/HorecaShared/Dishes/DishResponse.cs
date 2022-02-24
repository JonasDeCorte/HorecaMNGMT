namespace HorecaShared.Dishes
{
    public static class DishResponse
    {
        public class GetIndex
        {
            public List<DishDto.Index> Dishes { get; set; } = new();
            public int TotalAmount { get; set; }
        }

        public class GetDetail
        {
            public DishDto.Detail Dish { get; set; }
        }

        public class Delete
        {
        }

        public class Create
        {
            public int DishId { get; set; }
        }

        public class Edit
        {
            public int DishId { get; set; }
        }
    }
}
