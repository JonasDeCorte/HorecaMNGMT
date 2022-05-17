namespace Horeca.MVC.Models.Tables
{
    public class CanvasTableViewModel
    {
        public string type { get; set; }
        public string version { get; set; }
        public string originX { get; set; }
        public string originY { get; set; }
        public double left { get; set; }
        public double top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string fill { get; set; }
        public object stroke { get; set; }
        public int strokeWidth { get; set; }
        public object strokeDashArray { get; set; }
        public string strokeLineCap { get; set; }
        public int strokeDashOffset { get; set; }
        public string strokeLineJoin { get; set; }
        public bool strokeUniform { get; set; }
        public int strokeMiterLimit { get; set; }
        public int scaleX { get; set; }
        public int scaleY { get; set; }
        public int angle { get; set; }
        public bool flipX { get; set; }
        public bool flipY { get; set; }
        public int opacity { get; set; }
        public object shadow { get; set; }
        public bool visible { get; set; }
        public string backgroundColor { get; set; }
        public string fillRule { get; set; }
        public string paintFirst { get; set; }
        public string globalCompositeOperation { get; set; }
        public int skewX { get; set; }
        public int skewY { get; set; }
        public int cropX { get; set; }
        public int cropY { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Seats { get; set; }
        public string src { get; set; }
        public object crossOrigin { get; set; }
        public List<object> filters { get; set; }
    }
}
