using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Shared.Utils
{
    public class Constants
    {
        public static class BookingStatus
        {
            public const string PENDING = "Pending";
            public const string EXPIRED = "Expired";
            public const string COMPLETE = "Completed";
        }

        /// <summary>
        /// Working Days for Operating Hour
        /// </summary>
        public enum WorkingDays : int
        {
            Daily = 1,
            Weekend = 2,
        }

        /// <summary>
        /// Schedule Status
        /// </summary>
        public enum ScheduleStatus : int
        {
            Available = 1,
            Full = 2,
            Expired = 3,
            Unavailable = 4,
        }
    }
}