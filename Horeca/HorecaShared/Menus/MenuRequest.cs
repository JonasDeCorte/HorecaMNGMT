namespace HorecaShared.Menus
{
    public static class MenuRequest
    {
        public class GetIndex
        {
            public string? SearchTerm { get; set; }
            public bool OnlyActiveMenus { get; set; }
            public int Page { get; set; }
            public int Amount { get; set; } = 25;
        }

        public class GetDetail
        {
            public int MenuId { get; set; }
        }

        public class Delete
        {
            public int MenuId { get; set; }
        }

        public class Create
        {
            public MenuDto.Mutate Menu { get; set; }
        }

        public class Edit
        {
            public int MenuId { get; set; }
            public MenuDto.Mutate Menu { get; set; }
        }
    }
}
