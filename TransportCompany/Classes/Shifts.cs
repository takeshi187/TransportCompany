namespace TransportCompany.Classes
{
    public class Shifts
    {
        public long ShiftId { get; set; }
        public long EmployeeId { get; set; }
        public long RoadTrainId { get; set; }
        public long SupplyId { get; set; }
        public long RouteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Hours { get; set; }
        public int Distance { get; set; }
    }
}
