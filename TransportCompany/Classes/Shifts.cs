namespace TransportCompany.Classes
{
    public class Shifts
    {
        public int ShiftId { get; private set; }
        public int EmployeeId { get; private set; }
        public int RoadTrainId { get; private set; }
        public int SupplyId { get; private set; }
        public int RouteId { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public int Hours { get; private set; }
    }
}
