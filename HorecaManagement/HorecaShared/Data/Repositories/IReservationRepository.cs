using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Reservation GetReservationIncludingDependencies(int id);
    }
}