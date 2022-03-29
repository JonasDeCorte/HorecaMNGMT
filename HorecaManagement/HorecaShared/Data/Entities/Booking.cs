using Horeca.Shared.Data.Entities.Account;

namespace Horeca.Shared.Data.Entities
{
    public class Booking : BaseEntity
    {
        private ApplicationUser? _user;
        public string UserId { get; set; }

        public ApplicationUser User
        {
            set => _user = value;
            get => _user
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(User));
        }

        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string BookingNo { get; set; }

        public string BookingStatus { get; set; }

        public string FullName { get; set; }

        public string PhoneNo { get; set; }
    }
}