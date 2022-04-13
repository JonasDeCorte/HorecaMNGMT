namespace Horeca.Shared.Dtos.Tables
{
    public class TableDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int BookingDetailId { get; set; }

        public int Pax { get; set; }
    }

    public class MutateTableDto
    {
        public int ScheduleId { get; set; }

        public int Pax { get; set; }
    }
}