namespace TransportCompany.Classes
{
    public class Employees
    {
        public int EmployeeId { get; private set; }
        public int PostId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? MiddleName { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public int Phone { get; private set; }
        public string Email { get; private set; }
    }
}
