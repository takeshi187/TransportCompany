using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportCompany.Classes
{
    public class Shifts
    {
        public int ShiftId { get; private set; }
        public int EmployeeId { get; private set; }
        public int TransportId { get; private set; }
        public int TrailerId { get; private set; }
        public int SupplyId { get; private set; }
        public int RouteId { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public int Hours { get; private set; }
    }
}
