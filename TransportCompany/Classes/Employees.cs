namespace TransportCompany.Classes
{
    public class Employees
    {
        public long EmployeeId { get; set; }
        public long StatusId { get; set; }
        public long PostId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
