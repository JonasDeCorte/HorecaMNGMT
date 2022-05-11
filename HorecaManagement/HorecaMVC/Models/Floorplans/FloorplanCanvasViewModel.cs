using Horeca.MVC.Models.Tables;

namespace Horeca.MVC.Models.Floorplans
{
    public class FloorplanCanvasViewModel
    {
        public string Version { get; set; }
        public List<CanvasTableViewModel> Objects { get; set; } = new List<CanvasTableViewModel>();
    }
}
