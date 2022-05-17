namespace Horeca.Shared.Data.Entities
{
    public class Floorplan : BaseEntity
    {
        public string Name { get; set; }
        public Restaurant? Restaurant { get; set; }
        public int? RestaurantId { get; set; }
        public List<Table> Tables { get; set; }
    }
}