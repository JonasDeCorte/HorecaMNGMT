using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository

    {
        private readonly DatabaseContext context;

        public ReservationRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public Reservation GetReservationIncludingDependencies(int id)
        {
            return context.Reservations.Include(x => x.Table).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}