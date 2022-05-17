namespace Horeca.MVC.Models.Tables
{
    public class EditTableViewModel
    {
        public int Id { get; set; }

        public int FloorplanId { get; set; }

        public int? Pax { get; set; }

        public string? Seats { get; set; }

        public string Name { get; set; }
    }
}