using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Data.Entities
{
    public class RestaurantUser : BaseEntity
    {
        private Restaurant? _restaurant;
        private ApplicationUser? _user;

        public int RestaurantId { get; set; }

        public Restaurant Restaurant
        {
            set => _restaurant = value;
            get => _restaurant
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Restaurant));
        }

        public string UserId { get; set; }

        public ApplicationUser User
        {
            set => _user = value;
            get => _user
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(ApplicationUser));
        }
    }
}