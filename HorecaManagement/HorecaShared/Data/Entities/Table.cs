using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Shared.Data.Entities
{
    public class Table : BaseEntity
    {
        public string Name { get; set; }

        public int AmountOfPeople { get; set; }

        public bool HasReservation { get; set; } = false;

        public Reservation? Reservation { get; set; }
        public int? ReservationId { get; set; }
    }
}