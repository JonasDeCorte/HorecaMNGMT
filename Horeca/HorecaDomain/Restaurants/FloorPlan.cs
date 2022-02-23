namespace Domain.Restaurants
{
    public class FloorPlan
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Table> Tables { get; set; } = new List<Table>();

        public FloorPlan(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }
    }
}
