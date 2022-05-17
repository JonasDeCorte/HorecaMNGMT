namespace Horeca.MVC.Models.Tables
{
    public class GetCanvasTableViewModel
    {
        public int id { get; set; }
        public int floorplanId { get; set; }
        public int? scheduleId { get; set; }
        public int? pax { get; set; }
        public string? seats { get; set; }
        public string name { get; set; }
        public string src { get; set; }
        public string type { get; set; }
        public string originX { get; set; }
        public string originY { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public double scaleX { get; set; }
        public double scaleY { get; set; }
    }
}
