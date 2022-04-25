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

        private Schedule? _schedule;

        public int ScheduleId { get; set; }

        public Schedule Schedule
        {
            set => _schedule = value;
            get => _schedule
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Schedule));
        }

        public int Pax { get; set; }

        public DateTime BookingDate { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string BookingNo { get; set; }

        public string BookingStatus { get; set; }

        public string FullName { get; set; }

        public string PhoneNo { get; set; }
    }
}