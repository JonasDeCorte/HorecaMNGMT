using Horeca.MVC.Models.Tables;

namespace Horeca.MVC.Models.Floorplans
{
    public class FloorplanCanvasViewModel
    {
        public string version { get; set; }
        public List<CanvasTableViewModel> objects { get; set; } = new List<CanvasTableViewModel>();
    }
}
