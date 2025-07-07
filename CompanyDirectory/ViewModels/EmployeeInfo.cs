namespace CompanyDirectory.ViewModels
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public bool HasDrivingLicense { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}