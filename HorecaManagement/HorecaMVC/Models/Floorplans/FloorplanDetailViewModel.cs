using Horeca.MVC.Models.Tables;

namespace Horeca.MVC.Models.Floorplans
{
    public class FloorplanDetailViewModel : FloorplanViewModel
    {
        public List<TableDetailViewModel> Tables { get; set; } = new List<TableDetailViewModel>();
        public string Json { get; set; }
    }
}
