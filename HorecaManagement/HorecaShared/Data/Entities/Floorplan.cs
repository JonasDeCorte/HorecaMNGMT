namespace Horeca.Shared.Data.Entities
{
    public class Floorplan : BaseEntity
    {
        public string Type { get; set; }
        public string Version { get; set; }
        public string OriginX { get; set; }
        public string OriginY { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string? Fill { get; set; }
        public int StrokeWidth { get; set; }
        public string? StrokeLineCap { get; set; }
        public int StrokeDashOffset { get; set; }
        public string StrokeLineJoin { get; set; }
        public bool StrokeUniform { get; set; }
        public int StrokeMiterLimit { get; set; }
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public int Angle { get; set; }
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        public int Opacity { get; set; }
        public bool Visible { get; set; }
        public string BackgroundColor { get; set; }
        public string FillRule { get; set; }
        public string PaintFirst { get; set; }
        public string GlobalCompositeOperation { get; set; }
        public int SkewX { get; set; }
        public int SkewY { get; set; }
        public int CropX { get; set; }
        public int CropY { get; set; }
        public string Name { get; set; }
        public string Seats { get; set; }
        public string Src { get; set; }
    }
}