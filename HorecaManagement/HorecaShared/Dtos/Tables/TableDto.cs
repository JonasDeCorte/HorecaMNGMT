using Horeca.Shared.Dtos.Reservation;

namespace Horeca.Shared.Dtos.Tables
{
    public class TableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AmountOfPeople { get; set; }

        public bool HasReservation { get; set; }

        public ReservationDto? Reservation { get; set; }
    }

    public class CreateTableDto
    {
        public string Name { get; set; }

        public int AmountOfPeople { get; set; }
    }

    public class EditTableDto : TableDto
    {
    }
}