using HorecaDomain.Common;

namespace Domain.Restaurants
{
    public class FloorPlan : Entity
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Table> Tables { get; set; } = new List<Table>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private FloorPlan()
        {
        }

        public FloorPlan(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }
    }
}