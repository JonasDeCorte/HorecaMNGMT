using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public int TotalCapacity { get; set; }

        public List<ApplicationUser> Employees { get; set; } = new();

        public List<MenuCard> MenuCards { get; set; } = new();

        public List<Floorplan> Floorplans { get; set; } = new();
    }
}