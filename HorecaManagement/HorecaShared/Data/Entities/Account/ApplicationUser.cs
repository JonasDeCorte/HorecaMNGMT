using Microsoft.AspNetCore.Identity;

namespace Horeca.Shared.Data.Entities.Account
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsEnabled { get; set; }

        public bool IsOwner { get; set; }
        public string ExternalId { get; set; }

        public List<UserPermission> Permissions { get; private set; } = new();

        public List<RestaurantUser> Restaurants { get; set; } = new();
    }
}