namespace Horeca.Shared.Dtos.Reservation
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int AmountOfPeople { get; set; }

        public DateTime DateOfReservation { get; set; }
        public DateTime TimeOfReservation { get; set; }

        public int TableId { get; set; }
    }

    public class MutateReservationDto : ReservationDto
    {
    }
}