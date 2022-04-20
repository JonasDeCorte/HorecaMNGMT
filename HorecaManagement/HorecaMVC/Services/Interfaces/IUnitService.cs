using Horeca.Shared.Dtos.Units;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IUnitService
    {
        public Task<IEnumerable<UnitDto>> GetUnits();
        public Task<UnitDto> GetUnitById(int id);
        public Task<HttpResponseMessage> AddUnit(MutateUnitDto unitDto);
        public Task<HttpResponseMessage> UpdateUnit(MutateUnitDto unitDto);
        public Task<HttpResponseMessage> DeleteUnit(int id);
    }
}
