namespace Horeca.Shared.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public int AmountOfPeople { get; set; }

        public DateTime DateOfReservation { get; set; }
        public DateTime TimeOfReservation { get; set; }

        public Table Table { get; set; }
    }
}