using Horeca.MVC.Models.Tables;

namespace Horeca.MVC.Models.Floorplans
{
    public class FloorplanDetailViewModel : FloorplanViewModel
    {
        public List<FloorplanTableViewModel> Tables { get; set; } = new List<FloorplanTableViewModel>();
        public string Json { get; set; }
    }
}
