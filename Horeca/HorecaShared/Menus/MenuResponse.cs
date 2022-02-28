namespace HorecaShared.Menus
{
    public static class MenuResponse
    {
        public class GetIndex
        {
            public List<MenuDto.Index> Menus { get; set; } = new();
            public int TotalAmount { get; set; }
        }

        public class GetDetail
        {
            public MenuDto.Detail Menu { get; set; }
        }

        public class Delete
        {
        }

        public class Create
        {
            public int MenuId { get; set; }
        }

        public class Edit
        {
            public int MenuId { get; set; }
        }
    }
}
