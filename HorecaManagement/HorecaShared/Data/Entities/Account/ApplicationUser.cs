using Microsoft.AspNetCore.Identity;

namespace Horeca.Shared.Data.Entities.Account
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsEnabled { get; set; }
        public int Id { get; init; }
    }
}