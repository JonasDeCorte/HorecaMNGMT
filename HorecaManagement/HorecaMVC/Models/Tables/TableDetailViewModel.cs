namespace Horeca.MVC.Models.Tables
{
    public class TableDetailViewModel : TableViewModel
    {
        public string Type { get; set; }

        public string OriginX { get; set; }

        public string OriginY { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public double ScaleX { get; set; }

        public double ScaleY { get; set; }
    }
}
